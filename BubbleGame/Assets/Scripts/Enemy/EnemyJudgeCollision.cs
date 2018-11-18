using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJudgeCollision : MonoBehaviour
{
    GameObject calculationController;
    CalculationProperty calculationProperty;

    JudgeBoxBContainBoxA judgeContainFunc;
    JudgeBiggerBoxCollider judgeBiggerFunc;

    EnemyController controller;

    /// <summary>
    /// FIXME:ここをenumにできないか
    /// </summary>
    bool isEnemyBiggerThanBubble = false;
    bool isBubbleContainEnemy = false;

    // Use this for initialization
    void Start()
    {
        calculationController = GameObject.Find("CalculationController");

        calculationProperty = calculationController.GetComponent<CalculationProperty>();

        judgeContainFunc = calculationController.GetComponent<JudgeBoxBContainBoxA>();
        judgeBiggerFunc = calculationController.GetComponent<JudgeBiggerBoxCollider>();

        controller = transform.parent.GetComponent<EnemyController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BubbleCollider")
        {
            //当たった時に、大きさを比較
            isEnemyBiggerThanBubble = judgeBiggerFunc.JudegeWhichBoxIsBigger(this.gameObject, other.gameObject);
            if (isEnemyBiggerThanBubble)
            {
                //泡に破裂命令
                other.transform.parent.GetComponent<BubbleSetController>().DestroyBubble();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BubbleCollider")
        {
            isBubbleContainEnemy = judgeContainFunc.JudgeIsBoxBContainBoxA(this.gameObject, other.gameObject);
            if (isBubbleContainEnemy)
            {
                other.transform.parent.GetComponent<BubbleSetController>().SaveInsideObj(transform.parent.gameObject);
                SetBoxAndBubbleVelocity(calculationProperty.UpForce, this.gameObject, other.gameObject);
            }
            else
            {
                //一定時間後に、泡を破滅させる
            }
        }
    }

    /// <summary>
    /// FIXME:Controllerに置けないか？可変性がない
    /// </summary>
    /// <param name="_upForce"></param>
    /// <param name="_enemy"></param>
    /// <param name="_boxColider"></param>
    private void SetBoxAndBubbleVelocity(float _upForce, GameObject _enemy, GameObject _boxColider)
    {
        Vector3 upForce = Vector3.up * _upForce;
        
        //FIXME:ずっと探すではなく、一回だけにする
        if (_boxColider.transform.parent.Find("Bubble"))
        {
            GameObject bubble = _boxColider.transform.parent.Find("Bubble").gameObject;
            bubble.GetComponent<BubbleController>().SetRigibodyVelocityOnce(upForce);
        }

        controller.SetRigibodyVelocityOnce(upForce);
    }
}
