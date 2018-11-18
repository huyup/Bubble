using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargsStatus : EnemyStatus {

    #region 視野探索用
    /// <summary>
    /// 最大視野角
    /// </summary>
    [SerializeField]
    private float maxViewAngle = 30.0f;
    public float MaxViewAngle
    {
        get { return maxViewAngle; }
        private set
        {
            maxViewAngle = value;
        }
    }

    /// <summary>
    /// 見える距離
    /// </summary>
    [SerializeField]
    private float eyeDistance = 5.0f;
    public float EyeDistance
    {
        get { return eyeDistance; }
        private set
        {
            eyeDistance = value;
        }
    }
    #endregion

    #region 待機用
    /// <summary>
    /// 最小待機時間
    /// </summary>
    [SerializeField]
    private float minWaitTime = 2.0f;
    public float MinWaitTime
    {
        get { return minWaitTime; }
        private set
        {
            minWaitTime = value;
        }
    }

    /// <summary>
    /// 最大待機時間
    /// </summary>
    [SerializeField]
    private float maxWaitTime = 4.0f;
    public float MaxWaitTime
    {
        get { return maxWaitTime; }
        private set
        {
            maxWaitTime = value;
        }
    }
    #endregion

    #region  徘徊用
    /// <summary>
    /// 移動範囲
    /// </summary>
    [SerializeField]
    private float walkRange = 5.0f;
    public float WalkRange
    {
        get { return walkRange; }
        private set
        {
            walkRange = value;
        }
    }
    #endregion

    
}
