using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO:狼専用のアニメーター
/// </summary>
public class WargAnimator : EnemyAnimator {
    public override void SetMoveAnimatorParameter()
    {
        //移動パラメータ
        Vector3 posXZ = transform.position - new Vector3(0, transform.position.y, 0);
        Vector3 deltaPosition = prePositionXZ - posXZ;

        if (deltaPosition.magnitude > 0.01)
            animator.SetBool("Moving", true);
        else
            animator.SetBool("Moving", false);


        prePositionXZ = transform.position - new Vector3(0, transform.position.y, 0);
    }

}
