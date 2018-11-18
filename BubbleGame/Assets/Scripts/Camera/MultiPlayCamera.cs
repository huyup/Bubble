using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayCamera : MonoBehaviour
{
    [SerializeField]
    List<GameObject> players = new List<GameObject>();

    [SerializeField]
    private float distanceToForcusPointY = 8.5f;

    [SerializeField]
    private float distanceToForcusPointZ = -10f;
    
    Vector3 centerPos;
    // Update is called once per frame
    void Update()
    {
        Vector3 positionSum=Vector3.zero;

        for(int i=0;i<players.Count;i++)
        {
            positionSum += players[i].transform.position;
        }
        centerPos = positionSum / players.Count;

        transform.position = new Vector3(centerPos.x,
distanceToForcusPointY,
centerPos.z + distanceToForcusPointZ);
    }
    
}
