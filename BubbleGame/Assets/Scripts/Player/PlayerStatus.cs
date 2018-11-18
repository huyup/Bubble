using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum PlayerSelection
    {
        Player1 = 1,
        Player2,
    };

    public PlayerSelection playerNum = PlayerSelection.Player1;

    /// <summary>
    /// プレイヤーの番号
    /// </summary>
    //FIXME:ここにNumのsetの制限を追加する
    public int Num
    {
        get { return (int)playerNum; }
    }



    /// <summary>
    /// 移動時の速度
    /// </summary>
    [SerializeField]
    private float runSpeed = 5;
    public float RunSpeed
    {
        get { return runSpeed; }
        private set
        {
            if (runSpeed > 0 && runSpeed != 0)
                runSpeed = value;
        }
    }

    /// <summary>
    /// 泡を前に押す力
    /// </summary>
    [SerializeField]
    float bubbleFowardPower = 15f;
    public float BubbleFowardPower
    {
        get { return bubbleFowardPower; }
        private set
        {
            if (bubbleFowardPower > 0 && bubbleFowardPower != 0)
                bubbleFowardPower = value;
        }
    }


    /// <summary>
    /// 泡を上に押す力
    /// </summary>
    [SerializeField]
    float bubbleUpPower = 15f;
    public float BubbleUpPower
    {
        get { return bubbleUpPower; }
        private set
        {
            if (bubbleUpPower > 0 && bubbleUpPower != 0)
                bubbleUpPower = value;
        }
    }

    /// <summary>
    /// 泡の拡大スピード
    /// </summary>
    [SerializeField]
    float spaceKeySpeed = 0.15f;
    public float SpaceKeySpeed
    {
        get { return spaceKeySpeed; }
        private set
        {
            if (spaceKeySpeed > 0 && spaceKeySpeed != 0)
                spaceKeySpeed = value;
        }
    }

    /// <summary>
    /// 最大hp
    /// </summary>
    [HideInInspector]
    const int MaxHp = 3;

    [HideInInspector]
    public int nowHp;

    private void Start()
    {
        nowHp = MaxHp;
    }

    /// <summary>
    /// 点滅間隔
    /// </summary>
    [SerializeField]
    float invincibleInterval = 0.1f;
    public float InvincibleInterval
    {
        get { return invincibleInterval; }
        private set
        {
            invincibleInterval = value;
        }
    }

    /// <summary>
    /// 無敵時間
    /// </summary>
    [SerializeField]
    float invincibleTotalTime = 20f;
    public float InvincibleTotalTime
    {
        get { return invincibleTotalTime; }
        private set
        {
            invincibleTotalTime = value;
        }
    }

    /// <summary>
    /// ジャンプパワー
    /// </summary>
    [SerializeField]
    float jumpPower = 20f;
    public float JumpPower
    {
        get { return jumpPower; }
        private set
        {
            jumpPower = value;
        }
    }

    /// <summary>
    /// 重力Factor
    /// </summary>
    [SerializeField]
    float gravityFactor = 0.3f;
    public float GravityFactor
    {
        get { return gravityFactor; }
        private set
        {
            gravityFactor = value;
        }
    }
}
