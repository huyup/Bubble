using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO:タコ専用のパラメータを追加
/// </summary>
public class OctopusStatus : EnemyStatus {
    #region  浮上用
    /// <summary>
    /// 浮上速度
    /// </summary>
    [SerializeField]
    private float floatingSpeed = 3.0f;
    public float FloatingSpeed
    {
        get { return floatingSpeed; }
        private set
        {
            floatingSpeed = value;
        }
    }

    /// <summary>
    /// 浮上時間
    /// </summary>
    [SerializeField]
    private float floatingTime = 3.0f;
    public float FloatingTime
    {
        get { return floatingTime; }
        private set
        {
            floatingTime = value;
        }
    }

    /// <summary>
    /// 浮上時間
    /// </summary>
    [SerializeField]
    private float floatingTotalTime = 3.0f;
    public float FloatingTotalTime
    {
        get { return floatingTotalTime; }
        private set
        {
            floatingTotalTime = value;
        }
    }

    /// <summary>
    /// 浮上間隔
    /// </summary>
    [SerializeField]
    private float floatingInterval = 1.0f;
    public float FloatingInterval
    {
        get { return floatingInterval; }
        private set
        {
            floatingInterval = value;
        }
    }
    #endregion
    #region 攻撃用
    /// <summary>
    /// 攻撃間隔
    /// </summary>
    [SerializeField]
    private float attackInterval = 3.0f;
    public float AttackInterval
    {
        get { return attackInterval; }
        private set
        {
            attackInterval = value;
        }
    }

    /// <summary>
    /// 弾のスピード
    /// </summary>
    [SerializeField]
    private float bulletSpeed = 8.0f;
    public float BulletSpeed
    {
        get { return bulletSpeed; }
        private set
        {
            bulletSpeed = value;
        }
    }

    /// <summary>
    /// 攻撃の後の待機時間
    /// </summary>
    [SerializeField]
    private float attackBreakTime = 3.0f;
    public float AttackBreakTime
    {
        get { return attackBreakTime; }
        private set
        {
            attackBreakTime = value;
        }
    }
    #endregion
}
