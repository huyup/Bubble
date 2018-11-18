using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ステートの種類.

public class WargController : EnemyController
{
    enum WargState
    {
        Searching,    // 探索
        Chasing,    // 追跡
        Attacking,  // 攻撃
        Died,       // 死亡
    };
    WargSearch searchController;
    WargsStatus status;
    WargAnimator animator;
    EnemyMove moveController;
    
    // 残り待機時間
    float waitTime;

    // 初期位置を保存しておく変数
    Vector3 initPosition;

    // 複数のアイテムを入れれるように配列にしましょう。
    public GameObject[] dropItemPrefab;

    bool attacked = false;

    WargState nowState;
    WargState nextState;

    // Use this for initialization
    void Start()
    {
        status = transform.root.GetComponent<WargsStatus>();
        animator = GetComponent<WargAnimator>();
        moveController = GetComponent<EnemyMove>();
        searchController = transform.Find("SearchAreaTrigger").GetComponent<WargSearch>();

        // 初期位置を保持
        initPosition = transform.position;
        // 待機時間
        waitTime = status.MinWaitTime;

        nowState = WargState.Searching;
        nextState = WargState.Searching;
    }

    // Update is called once per frame
    void Update()
    {
        switch (nowState)
        {
            case WargState.Searching:
                Searching();
                break;
            case WargState.Chasing:
                Chasing();
                break;
            case WargState.Attacking:
                Attacking();
                break;
        }

        if (nowState != nextState)
        {
            nowState = nextState;
            switch (nowState)
            {
                case WargState.Searching:
                    PrepareForSearch();
                    break;
                case WargState.Chasing:
                    PrepareForChase();
                    break;
                case WargState.Attacking:
                    PrepareForAttack();
                    break;
                case WargState.Died:
                    Died();
                    break;
            }
        }
        animator.SetMoveAnimatorParameter();
    }


    // ステートを変更する.
    void ChangeState(WargState _nextState)
    {
        nextState = _nextState;
    }
    //徘徊準備
    void PrepareForSearch()
    {
        ResetStateFlag();
    }
    void Searching()
    {
        // ターゲットを発見したら追跡
        if (attackTarget)
        {
            ChangeState(WargState.Chasing);
            //waitTime = 0;
        }
        // 待機時間がまだあったら
        if (waitTime > 0.0f)
        {
            // 待機時間を減らす
            waitTime -= Time.deltaTime;
            // 待機時間が無くなったら
            if (waitTime <= 0.0f)
            {
                // 範囲内の何処か
                Vector2 randomValue = Random.insideUnitCircle * status.WalkRange;
                // 移動先の設定
                Vector3 destinationPosition = initPosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                //　目的地の指定.
                moveController.SetDestination(destinationPosition);
            }
        }
        else
        {
            searchController.SearchByEye();

            // 目的地へ到着
            if (moveController.CheckIsArrived())
            {
                // 待機状態へ
                waitTime = Random.Range(status.MinWaitTime, status.MaxWaitTime);
            }

        }
    }


    // 追跡準備
    void PrepareForChase()
    {
        ResetStateFlag();
    }
    // 追跡中
    void Chasing()
    {

        // 移動先をプレイヤーに設定
        moveController.SetDestination(attackTarget.position);

        // FIXME:攻撃範囲内に近づいたら攻撃、攻撃範囲の値がマジックナンバーなってる
        if (Vector3.Distance(attackTarget.position, transform.position) <= 2.5f)
        {
            ChangeState(WargState.Attacking);
        }
    }

    // 攻撃準備
    void PrepareForAttack()
    {
        ResetStateFlag();
        status.attacking = true;

        // 敵の方向に振り向かせる.
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        moveController.SetDirectionXZ(targetDirection);

        // 移動を止める.
        moveController.StopMove();
    }
    IEnumerator TurnOnSphereTriggerCoroutine(Transform explosionCollider)
    {
        yield return new WaitForSeconds(0.5f);
        explosionCollider.GetComponent<SphereCollider>().enabled = true;
    }
    // 攻撃中
    void Attacking()
    {
        //TODO:ここに攻撃の処理を入れる
        //Debug.Log("Attack");
        if (!attacked)
        {
            GameObject explosion = transform.Find("ExplosionEffect").gameObject;
            explosion.transform.position = transform.position;

            GameObject bodyMesh = transform.Find("RETMESH2").gameObject;
            bodyMesh.SetActive(false);

            ParticleSystem[] particles = explosion.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in particles)
            {
                if (!particle.isPlaying)
                    particle.Play();
            }
            StartCoroutine(TurnOnSphereTriggerCoroutine(explosion.transform.Find("ExploisionTrigger")));

            attacked = true;
        }
        Destroy(this.gameObject,1.5f);
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

    void Damage(AttackArea.AttackInfo attackInfo)
    {
        status.nowHP -= attackInfo.attackPower;
        if (status.nowHP <= 0)
        {
            status.nowHP = 0;
            // 体力０なので死亡
            ChangeState(WargState.Died);
        }
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
