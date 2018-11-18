//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BubbleController : MonoBehaviour
//{
//    Rigidbody rb;
//    BubbleRotation bubbleRotation;
//    GameObject movingCenter;

//    [SerializeField]
//    float upForceToTheBubble = 100;

//    //Fixeme：置くところを修正
//    [SerializeField]
//    float maxFloatLerance = 0.05f;
    
//    bool isArrived = false;
//    bool canSetHitObjsToChild = true;
//    bool canSetMoveParameter = true;
//    // Use this for initialization
//    void Start()
//    {
//        bubbleRotation = GetComponent<BubbleRotation>();
//        rb = GetComponent<Rigidbody>();
//        movingCenter = transform.Find("Sphere").gameObject;
//    }
//    public void MergeFunc(Vector3 _centerPos, List<GameObject> hitObjs)
//    {
//        //isArrived = CheckIsArrived(_centerPos);

//        //if (isArrived)
//        //{
//        //    if (canSetHitObjsToChild)
//        //    {
//        //        SetAllHitObjsToChildObj(hitObjs);
//        //        canSetHitObjsToChild = false;
//        //    }
//        //    bubbleRotation.enabled = false;

//        //    rb.AddForce(Vector3.up * upForceToTheBubble * Time.deltaTime, ForceMode.VelocityChange);
//        //}
//        //else
//        //{
//        //    //中心点に移動
//        //    MoveToObjCenter(_centerPos);
//        //}
//    }
//    //private void MoveToObjCenter(Vector3 _centerPos)
//    //{
//    //    if (canSetMoveParameter)
//    //    {
//    //        rb.velocity = Vector3.zero;
//    //        canSetMoveParameter = false;
//    //    }
//    //    Vector3 direction;
//    //    direction = _centerPos - movingCenter.transform.position;
//    //    direction.Normalize();
//    //    movingCenter.GetComponent<Rigidbody>().MovePosition(movingCenter.transform.position + direction * Time.deltaTime);
//    //}
//    //private bool CheckIsArrived(Vector3 _centerPos)
//    //{
//    //    if (Mathf.Abs(movingCenter.transform.position.z - _centerPos.z) < maxFloatLerance)
//    //        return true;
//    //    else
//    //        return false;
//    //}
//    private void SetAllHitObjsToChildObj(List<GameObject> hitObjs)
//    {
//        foreach (GameObject child in hitObjs)
//        {
//            child.transform.parent = transform;
//        }
//    }
//}
