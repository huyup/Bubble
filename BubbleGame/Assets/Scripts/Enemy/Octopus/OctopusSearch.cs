using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusSearch : EnemySearch {
    OctopusController octopusController;
    
    bool isSetFinished = false;

    // Use this for initialization
    void Start()
    {
        octopusController = transform.parent.GetComponent<OctopusController>();
    }

    void OnTriggerStay(Collider other)
    {
        // Playerタグをターゲットにする
        if (other.tag == "Player" && !isSetFinished)
        {
            octopusController.SetAttackTarget(other.transform);
            isSetFinished = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Playerタグをターゲットから外す
        if (other.tag == "Player" )
        {
            octopusController.SetAttackTarget(null);
            isSetFinished = false;
        }
    }
}
