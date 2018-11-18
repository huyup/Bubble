using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyStatus : MonoBehaviour
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    private float walkSpeed = 6.0f;
    public float WalkSpeed
    {
        get { return walkSpeed; }
        private set
        {
            if (walkSpeed > 0 && walkSpeed != 0)
                walkSpeed = value;
        }
    }

    /// <summary>
    /// 回転速度
    /// </summary>
    [SerializeField]
    private float rotateSpeed = 360.0f;
    public float RotateSpeed
    {
        get { return rotateSpeed; }
        private set
        {
            rotateSpeed = value;
        }
    }

    /// <summary>
    /// 最大Hp
    /// </summary>
    [SerializeField]
    private int maxHp = 100;
    public int MaxHp
    {
        get { return maxHp; }
        private set
        {
            if (maxHp > 0 && maxHp != 0)
                maxHp = value;
        }
    }

    /// <summary>
    /// 上昇速度の補正
    /// </summary>
    [SerializeField]
    private float upFactor = 1.1f;
    public float UpFactor
    {
        get { return upFactor; }
        private set
        {
            upFactor = value;
        }
    }
    //現在のhp
    public int nowHP = 100;

    // 最後に攻撃した対象.
    public GameObject lastAttackTarget = null;

    // プレイヤー名.
    public string characterName = "Player";

    //状態.
    public bool attacking = false;
    public bool died = false;
}
