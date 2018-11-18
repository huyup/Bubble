using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBubbleCollider : MonoBehaviour
{
    GameObject Bubble;
    // Use this for initialization
    void Start()
    {
        Bubble = transform.parent.Find("Bubble").gameObject;

    }
    // Update is called once per frame
    void Update()
    {
        if (Bubble != null)
        {
            transform.position = transform.parent.Find("Bubble").position;
            transform.localScale = transform.parent.Find("Bubble").localScale;
        }
    }
}
