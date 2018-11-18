//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyStopRotate : MonoBehaviour
//{
//    BubbleProperty property;

//    // Use this for initialization
//    void Start()
//    {
//        property = transform.parent.GetComponent<BubbleProperty>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //FIXME:ほかのオブジェに影響を与えないように修正

//        ////ランダム軸から回転する
//        //Vector3 randomAxis = new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
//        Vector3 randomAxis = new Vector3(0, 0, 1);
//        //float speed = property.RotateSpeed * Time.deltaTime;
//        float speed = -1;
//        Quaternion rot = Quaternion.AngleAxis(speed, randomAxis);
//        transform.rotation = rot * transform.rotation;

        
//    }
//}
