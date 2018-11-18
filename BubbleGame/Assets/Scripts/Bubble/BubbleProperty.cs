using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleProperty : MonoBehaviour
{
    /// <summary>
    /// 回転する速度
    /// </summary>
    [SerializeField]
    private float rotateSpeed = 0.15f;
    public float RotateSpeed
    {
        get { return rotateSpeed; }
        private set
        {
            rotateSpeed = value;
        }
    }

    /// <summary>
    /// 存続時間
    /// </summary>
    [SerializeField]
    private float lastTime = 3f;
    public float LastTime
    {
        get { return lastTime; }
        private set
        {
            lastTime = value;
        }
    }

    /// <summary>
    /// 最大サイズ
    /// </summary>
    [SerializeField]
    private float maxSize = 0.05f;
    public float MaxSize
    {
        get { return maxSize; }
        private set
        {
            maxSize = value;
        }
    }

    /// <summary>
    ///上昇状態
    /// </summary>
    [SerializeField]
    private bool isFloating =false;
    public bool IsFloating
    {
        get { return isFloating; }
        set
        {
            isFloating = value;
        }
    }
}
