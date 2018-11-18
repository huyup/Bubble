using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public class PlayerBubbleShooting : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// アタッチするオブジェ
    /// </summary>
    BubbleProperty bubbleProperty;
    Rigidbody rb;
    PlayerStatus property;
    Animator animator;

    [SerializeField]
    GameObject bubbleSet;
    List<GameObject> bubbles = new List<GameObject>();
    List<GameObject> bubbleSets = new List<GameObject>();

    /// <summary>
    /// 泡の出現参照オブジェ
    /// </summary>
    GameObject bubbleStartObj;
    Vector3 bubbleStartPos;

    /// <summary>
    /// 移動できる泡の最大サイズ
    /// </summary>
    float playerMoveableBubbleSize;

    float spaceKeyForce = 0.0f;
    bool canAddForceToBubble = false;
    bool isBubbleExploded = false;

    bool canSetBubbleStartPos = false;

    #endregion

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        property = GetComponent<PlayerStatus>();
        bubbleStartObj = transform.Find("BubbleStartObj").gameObject;
        animator = GetComponent<Animator>();

        bubbleProperty = bubbleSet.transform.Find("Bubble").GetComponent<BubbleProperty>();
        playerMoveableBubbleSize = bubbleProperty.MaxSize;
    }
    // Update is called once per frame
    void Update()
    {
        bubbleStartPos = bubbleStartObj.transform.position;
    }

    private void FixedUpdate()
    {
        PushTheBubble(ref canAddForceToBubble);
    }

    public void SetAttackAnimation(AttackButtonState _attackButtonState)
    {
        if (_attackButtonState == AttackButtonState.ButtonDown)
        {
            animator.SetBool("Attacking", true);
        }
        if (_attackButtonState == AttackButtonState.ButtonKeep)
        {
            animator.speed = 0.5f;
        }
        if (_attackButtonState == AttackButtonState.ButtonUp)
        {
            animator.speed = 1;
            animator.SetBool("Attacking", false);
        }
    }

    #region 泡関連メソッド
    public void CreateTheBubbleSet()
    {
        rb.velocity = Vector3.zero;

        canAddForceToBubble = false;
        isBubbleExploded = false;
        canSetBubbleStartPos = true;
        spaceKeyForce = 0;

        GameObject bubbleSetInstance = Instantiate(bubbleSet) as GameObject;

        GameObject bubbleInstance = bubbleSetInstance.transform.Find("Bubble").gameObject;

        bubbles.Add(bubbleInstance);
        bubbleSets.Add(bubbleSetInstance);
    }

    public void ChangeTheBubbleScale()
    {
        if (bubbles.Count == 0)
            return ;
        
        if (bubbles[bubbles.Count - 1])
        {
            rb.velocity = Vector3.zero;

            if (spaceKeyForce < bubbleProperty.MaxSize)
            {
                spaceKeyForce += property.SpaceKeySpeed * Time.deltaTime;
                bubbles[bubbles.Count - 1].transform.localScale += new Vector3(spaceKeyForce, spaceKeyForce, spaceKeyForce);
                if (canSetBubbleStartPos)
                {
                    bubbles[bubbles.Count - 1].transform.position = bubbleStartPos;
                    canSetBubbleStartPos = false;
                }
                //少しずつ前に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.forward * Time.deltaTime * 1.1f;
            }
            else
            {
                if (!isBubbleExploded)
                    bubbleSets[bubbleSets.Count - 1].GetComponent<BubbleSetController>().DestroyBubble();
                isBubbleExploded = true;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool CheckIsBubbleOverMoveableSize()
    {
        if (spaceKeyForce > bubbleProperty.MaxSize * 0.01f)
            return true;
        else
            return false;
    }

    public void TurnOnThePushFlag()
    {
        if (!isBubbleExploded)
            canAddForceToBubble = true;
    }
    public void PushTheBubble(ref bool _canAddForceToBubble)
    {
        if (bubbles.Count > 0)
        {
            if (bubbles[bubbles.Count - 1] == null)
                return;
        }
        if (_canAddForceToBubble&& !bubbles[bubbles.Count - 1].GetComponent<BubbleProperty>().IsFloating)
        {
            bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.forward * property.BubbleFowardPower,
                ForceMode.VelocityChange);
            bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.up * property.BubbleUpPower,
                ForceMode.VelocityChange);
            canAddForceToBubble = false;
        }
    }
    #endregion
}


