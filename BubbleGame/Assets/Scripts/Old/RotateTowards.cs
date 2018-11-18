using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    float rotateSpeed = 360;
    public GameObject target;
    Quaternion newRotation ;
    float plusTest = 10;
    // Use this for initialization
    void Start()
    {
        newRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = target.transform.position - transform.position;
        //Quaternion characterTargetRotation = Quaternion.LookRotation(direction);

        if (Input.GetKey(KeyCode.Alpha1))
        {
            plusTest += 10;
        }
        newRotation = Quaternion.Euler(transform.rotation.x,
    transform.rotation.y + plusTest,
    transform.rotation.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }
}
