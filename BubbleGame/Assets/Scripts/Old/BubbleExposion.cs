using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleExposion : MonoBehaviour
{
    //BubbleProperty burbleProperty;
    //GameObject burbleSet;
    //GameObject burble;

    //[SerializeField]
    //float banTime;
    //float banTimeCount;

    //bool canStartTheBanCount = false;
    //bool canBeDestoriedByOther = false;


    //// Use this for initialization
    //void Start()
    //{
    //    burble = transform.parent.gameObject;
    //    burbleProperty = burble.GetComponent<BubbleProperty>();

    //    burbleSet = transform.parent.transform.parent.gameObject;
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    CheckCanBeDestoried();
    //}

    //private void CheckCanBeDestoried()
    //{
    //    if (burbleProperty.IsLeftFromPlayer)
    //    {
    //        banTimeCount = banTime;
    //        canStartTheBanCount = true;
    //        burbleProperty.IsLeftFromPlayer = false;
    //    }

    //    if (canStartTheBanCount)
    //    {
    //        if (banTimeCount > 0)
    //        {
    //            banTimeCount--;
    //            Debug.Log(banTimeCount);
    //        }
    //        else
    //        {
    //            canBeDestoriedByOther = true;
    //            canStartTheBanCount = false;
    //        }
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player" && canBeDestoriedByOther)
    //    {
    //        burbleSet.GetComponent<BubbleSetController>().DestroyBubble();
    //    }
    //}



}
