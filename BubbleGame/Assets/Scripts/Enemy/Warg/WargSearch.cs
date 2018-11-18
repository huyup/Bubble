using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargSearch : EnemySearch {
    WargsStatus property;

    WargController wargController;

    private class Targets
    {
        public GameObject target;
        public float distance;
        public bool visible;
    }

    Targets[] targets = new Targets[WorldSetting.MaxPlayerNum];

    bool isSetFinished = false;
    // Use this for initialization
    void Start () {
        wargController = transform.parent.GetComponent<WargController>();
        property = transform.root.GetComponent<WargsStatus>();

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = new Targets();
            targets[i].target = GameObject.Find("Player" + (i + 1).ToString());
            targets[i].distance = 0;
            targets[i].visible = false;
        }
    }

    /// <summary>
    /// 視野を模倣する索敵メソッド
    /// FIXME:簡潔化すべき
    /// </summary>
    public void SearchByEye()
    {
        if (targets.Length == 0)
        {
            Debug.Log("プレイヤー名を設定してください");
            return;
        }
        if (!isSetFinished)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                float angle = 0;
                angle = CalculateAngleFromEyeToTarget(targets[i].target.transform);
                targets[i].distance = Vector3.Distance(transform.parent.position-new Vector3(0,transform.parent.position.y),
    targets[i].target.transform.position - new Vector3(0, targets[i].target.transform.position.y));
                if (angle <= property.MaxViewAngle && targets[i].distance <= property.EyeDistance)
                {
                    targets[i].visible = true;
                }
            }

            float nearDistance = 999;
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].visible)
                {
                    if (targets[i].distance < nearDistance)
                        nearDistance = targets[i].distance;
                }
            }

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].visible && targets[i].distance - nearDistance == 0)
                {
                    wargController.SetAttackTarget(targets[i].target.transform);
                    SetFinished();
                    break;
                }
            }
        }
    }

    private float CalculateAngleFromEyeToTarget(Transform _targetTransform)
    {
        //前に向く視線ベクトル
        Vector3 eyeDirection = transform.forward;

        //ターゲットを指すベクトル
        Vector3 targetDirection = _targetTransform.position - transform.position;

        float angleFromEyeToTarget = Vector3.Angle(targetDirection, eyeDirection);

        return angleFromEyeToTarget;
    }
    void OnTriggerStay(Collider other)
    {
        // Playerタグをターゲットにする
        if (other.tag == "Player" && !isSetFinished)
        {
            wargController.SetAttackTarget(other.transform);
            isSetFinished = true;
        }
    }
    void SetFinished()
    {
        isSetFinished = true;
    }

    #region Debug用
    /// <summary>
    /// TODO:ここにデバッグ用のrayを表示する
    /// </summary>
    private void DrawRayAsEyeArea()
    {
    }
    #endregion
}
