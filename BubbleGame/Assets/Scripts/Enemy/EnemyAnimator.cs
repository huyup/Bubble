using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    protected Animator animator;
    protected Vector3 prePositionXZ;

    void Start()
    {
        animator = GetComponent<Animator>();
        prePositionXZ = transform.position - new Vector3(0, transform.position.y, 0);
    }
    /// <summary>
    /// TODO:今　空になっている
    /// </summary>
    public virtual void SetMoveAnimatorParameter()
    {

    }
}
