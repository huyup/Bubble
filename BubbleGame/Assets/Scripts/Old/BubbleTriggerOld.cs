//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BubbleTrigger : MonoBehaviour
//{
//    Vector3 objCenterPos;

//    BubbleController bubbleController;
//    bool isHitEnemy;

//    List<GameObject> hitEnemys=new List<GameObject>();
//    // Use this for initialization
//    void Start()
//    {
//        bubbleController = transform.parent.GetComponent<BubbleController>();
//        isHitEnemy = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//    }

//    private void FixedUpdate()
//    {
//        if (isHitEnemy)
//        {
//            bubbleController.MergeFunc(objCenterPos,hitEnemys);
//        }
//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.tag == "Enemy")
//        {
//            Debug.Log("hitEnemy");
//            other.GetComponent<Rigidbody>().useGravity=false;
//            objCenterPos = other.transform.position + other.GetComponent<EnemyProperty>().Offset;
//            hitEnemys.Add(other.gameObject);
//            isHitEnemy = true;
//        }
//    }
//}
