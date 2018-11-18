using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour {
    bool canSetVelocity = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRigibodyVelocityOnce(Vector3 _velocity)
    {
        if (canSetVelocity)
        {
            GetComponent<BubbleProperty>().IsFloating = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = _velocity;
            canSetVelocity = false;
        }
    }
}
