using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationProperty : MonoBehaviour {
    /// <summary>
    /// 泡に包まれたときに上昇する力
    /// </summary>
    [SerializeField]
    private float upForce = 2f;
    public float UpForce
    {
        get { return upForce; }
        private set
        {
            upForce = value;
        }
    }
}
