using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerController controller;
    GameObject lastAttackObj;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject != lastAttackObj && other.gameObject.layer == 15/*EnemyHit*/)
        {
            lastAttackObj = other.gameObject;
            controller.Damgae();
        }
    }
}
