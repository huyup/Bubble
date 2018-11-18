using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusController : EnemyController
{
    enum OctopusState
    {
        StandBy, //待機
        Attacking,  // 攻撃
        Died,       // 死亡
    };

    OctopusSearch searchController;
    OctopusStatus status;
    OctopusAnimator animator;
    EnemyMove moveController;

    // 初期位置を保存しておく変数
    Vector3 initPosition;
    Quaternion initQuaternion;

    OctopusState nowState;
    OctopusState nextState;

    // 複数のアイテムを入れれるように配列にしましょう。
    public GameObject[] dropItemPrefab;
    bool attacked = false;
    bool isDiving = false;

    //弾
    public GameObject bullet;
    List<GameObject> bullets = new List<GameObject>();
    Vector3 shootStartPos;
    private void Start()
    {
        status = transform.root.GetComponent<OctopusStatus>();
        animator = GetComponent<OctopusAnimator>();
        moveController = GetComponent<EnemyMove>();
        searchController = transform.Find("SearchAreaTrigger").GetComponent<OctopusSearch>();

        // 初期位置を保持
        initPosition = transform.position;

        initQuaternion = transform.rotation;

        //重力制御を禁止する
        moveController.BanGravity();

        nowState = OctopusState.StandBy;
        nextState = OctopusState.StandBy;

        //Shoot
        shootStartPos = transform.Find("ShootStartObj").position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (nowState)
        {
            case OctopusState.StandBy:
                StandBy();
                break;
            case OctopusState.Attacking:
                Attacking();
                break;
        }

        if (nowState != nextState)
        {
            nowState = nextState;
            switch (nowState)
            {
                case OctopusState.StandBy:
                    PrepareForStandBy();
                    break;
                case OctopusState.Attacking:
                    PrepareForAttack();
                    break;
                case OctopusState.Died:
                    Died();
                    break;
            }
        }
        animator.SetMoveAnimatorParameter();
    }

    // ステートを変更する.
    void ChangeState(OctopusState _nextState)
    {
        nextState = _nextState;
    }
    //待機準備
    void PrepareForStandBy()
    {
        ResetStateFlag();
        transform.rotation = initQuaternion;
    }
    void StandBy()
    {
        if (attackTarget != null)
        {
            ChangeState(OctopusState.Attacking);
        }
    }
    // 攻撃準備
    void PrepareForAttack()
    {
        Debug.Log("PrepareForAttack");
        ResetStateFlag();
        status.attacking = true;

        // 敵の方向に振り向かせる.
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        moveController.SetDirectionXZ(targetDirection);

        // 移動を止める.
        moveController.StopMove();
    }
    // 攻撃中
    void Attacking()
    {
        if (attackTarget == null || isInsideBubble)
        {
            ChangeState(OctopusState.StandBy);
            StopCoroutine(DiveAndAttackCoroutine());
            return;
        }
        //Shootの場所を更新
        shootStartPos = transform.Find("ShootStartObj").position;
        if (!attacked)
        {        
            // 敵の方向に振り向かせる.
            StartCoroutine(DiveAndAttackCoroutine());
        }
        //FIXME:敵のhpが0以下の場合、再設定へ
        if (attackTarget.GetComponent<PlayerStatus>().nowHp <= 0)
            attackTarget = null;


    }
    IEnumerator DiveAndAttackCoroutine()
    {
        if (attacked||attackTarget==null||isInsideBubble)
            yield break;

        attacked = true;
        //status.FloatingTotalTimeカウント分上昇
        for (int i = 0; i < status.FloatingTotalTime; i++)
        {
            if (isInsideBubble)
                break;
            transform.position += new Vector3(0, status.FloatingSpeed, 0) * Time.deltaTime;
            yield return null;
        }

        //弾1を発射させる
        GameObject bulletInstance = Instantiate(bullet) as GameObject;
        SetBullets(bulletInstance);
        yield return new WaitForSeconds(status.AttackInterval);

        //弾2を発射させる
        GameObject bulletInstance2 = Instantiate(bullet) as GameObject;
        SetBullets(bulletInstance2);
        yield return new WaitForSeconds(status.AttackInterval);

        //弾3を発射させる
        GameObject bulletInstance3 = Instantiate(bullet) as GameObject;
        SetBullets(bulletInstance3);

        yield return new WaitForSeconds(status.AttackBreakTime);

        ////status.FloatingTotalTimeカウント分落下
        for (int i = 0; i < status.FloatingTotalTime; i++)
        {
            if (isInsideBubble)
                break;
            transform.position -= new Vector3(0, status.FloatingSpeed, 0) * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(status.FloatingInterval);
        attacked = false;
    }
    void SetBullets(GameObject bulletInstance)
    {
        if (attackTarget == null)
            return;

        // 敵の方向に振り向かせる.
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        moveController.SetDirectionXZ(targetDirection);

        bullets.Add(bulletInstance);

        bullets[bullets.Count - 1].transform.position = shootStartPos;

        Vector3 direction= (attackTarget.position - transform.position).normalized;
        bullets[bullets.Count - 1].transform.LookAt(direction);
        bullets[bullets.Count - 1].GetComponent<Rigidbody>().AddForce(direction*status.BulletSpeed,ForceMode.VelocityChange);
        bullets.Remove(bulletInstance);

    }
    void dropItem()
    {
        if (dropItemPrefab.Length == 0) { return; }
        GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }
    void Died()
    {
        status.died = true;
        dropItem();
        Destroy(gameObject);
    }

    /// <summary>
    /// ステートが始まる前にステータスﾌﾗｸﾞを初期化する.
    /// FIXME:名前がおかしい
    /// </summary>
    void ResetStateFlag()
    {
        status.attacking = false;
        status.died = false;
    }
}
