using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// XXX:controllerが見つかりません
/// </summary>
public class EnemySearch : MonoBehaviour
{
    EnemyController controller;
    
    // Use this for initialization
    void Start()
    {
        controller = transform.parent.GetComponent<EnemyController>();
    }
}
