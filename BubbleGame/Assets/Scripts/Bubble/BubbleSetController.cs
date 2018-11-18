using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FIXEME:スクリプト修正
/// </summary>
public class BubbleSetController : MonoBehaviour
{
    GameObject bubble;
    GameObject bubbleExplosion;

    BubbleProperty bubbleProperty;
    ParticleSystem bubbleExplosionEff;

    BubbleExpolisionEff bubbleExpolision;

    float explosionEffLastTime = 1.8f;
    [SerializeField]
    List<GameObject> insideObjs = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        bubble = transform.Find("Bubble").gameObject;
        bubbleProperty = bubble.GetComponent<BubbleProperty>();

        bubbleExplosion = transform.Find("BubbleExplosion").gameObject;
        bubbleExplosionEff = bubbleExplosion.GetComponent<ParticleSystem>();
        bubbleExpolision = bubbleExplosion.GetComponent<BubbleExpolisionEff>();

    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DestroyMySelf", bubbleProperty.LastTime);
        CheckCanDestroyBubblSetController();
    }
    public void DestoryNow()
    {
        CancelInvoke("DestroyMySelf");
        DestroyBubble();
    }

    private void DestroyMySelf()
    {
        Destroy(bubble);
    }
    public void SaveInsideObj(GameObject insideObj)
    {
        EnemyController enemyController = insideObj.GetComponent<EnemyController>();

        if (enemyController == null)
        {
            Debug.Log("Cant found enemyController");
            return;
        }

        if (!enemyController.isInsideBubble)
        {
            insideObjs.Add(insideObj);
            enemyController.isInsideBubble = true;
        }
    }
    public void DestroyBubble()
    {
        if (this.gameObject == null || bubble == null)
            return;
        bubbleExpolision.SaveEffStartProperty(bubble.transform.localScale.x);
        Destroy(bubble);
        bubbleExplosion.transform.position = bubble.transform.position;
        bubbleExpolision.ChangeEffStartProperty();
        if (!bubbleExplosionEff.isPlaying)
        {
            bubbleExplosionEff.Play();
            Destroy(this.gameObject, explosionEffLastTime);
        }
    }

    private void AddDownForceToInsideObj()
    {
        if (insideObjs.Count == 0)
            return;

        for (int i = 0; i < insideObjs.Count; i++)
        {
            if (!insideObjs[i])
            {
                insideObjs.Remove(insideObjs[i]);
                break;
            }
            if (insideObjs[i].GetComponent<EnemyController>().canAddDownForce)
            {
                insideObjs[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                insideObjs[i].GetComponent<Rigidbody>().AddForce(Physics.gravity, ForceMode.VelocityChange);
                insideObjs[i].GetComponent<EnemyController>().canAddDownForce = false;
                insideObjs.Remove(insideObjs[i]);
            }
        }
    }
    private void CheckCanDestroyBubblSetController()
    {
        if (bubble == null)
        {
            AddDownForceToInsideObj();
            bubbleExpolision.ChangeEffStartProperty();
            if (!bubbleExplosionEff.isPlaying)
            {
                bubbleExplosionEff.Play();
                Destroy(this.gameObject, explosionEffLastTime);
            }
        }
        else
        {
            bubbleExplosion.transform.position = bubble.transform.position;
            //FIXME:xのスケール値だけ判断している
            bubbleExpolision.SaveEffStartProperty(bubble.transform.localScale.x);
        }
    }

}
