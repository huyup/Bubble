using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyCollision : MonoBehaviour {
    EnemyController controller;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            GetComponent<CharacterController>().enabled = true;
            controller.ResetForceFlag();
        }
    }
}
