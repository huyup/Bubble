using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    /// <summary>
    /// 落下できるかどうか
    /// FIXME:回数にしたほうがいいか？
    /// </summary>
    [HideInInspector]
    public bool canAddDownForce = true;

    /// <summary>
    /// 上昇できるかどうか
    /// FIXME:回数にしたほうがいいか？
    /// </summary>
    [HideInInspector]
    public bool canAddUpForce = true;

    /// <summary>
    /// 泡の中にいるかどうか
    /// </summary>
    [HideInInspector]
    public bool isInsideBubble = false;

    /// <summary>
    /// 攻撃目標
    /// </summary>
    protected Transform attackTarget;
    
    public void SetRigibodyVelocityOnce(Vector3 _velocity)
    {
        if (canAddUpForce)
        {
            GetComponent<CharacterController>().enabled = false;
            GetComponent<Rigidbody>().velocity = _velocity * 1.1f;
            canAddUpForce = false;
        }
    }

    public void ResetForceFlag()
    {
        canAddDownForce = true;
        canAddUpForce = true;
        isInsideBubble = false;
    }
    // 攻撃対象を設定する
    public void SetAttackTarget(Transform target)
    {
        attackTarget = target;
    }
}
