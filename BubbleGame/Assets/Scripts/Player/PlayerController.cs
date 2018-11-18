using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public class PlayerController : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// アタッチするコンポーネント
    /// </summary>
    Rigidbody rb;
    Animator animator;
    PlayerStatus status;

    /// <summary>
    /// 移動 
    /// </summary>
    float moveSpeed;

    //入力前の位置
    Vector3 prevPlayerPos;

    //無敵かどうか
    bool isVincible = false;

    //接地しているかどうか
    bool isGround = false;
    #endregion

    #region 初期化
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status = GetComponent<PlayerStatus>();

        moveSpeed = status.RunSpeed;

        prevPlayerPos = transform.position;
    }
    #endregion
    private void Update()
    {
        CheckDied();
    }
    private void FixedUpdate()
    {
        AddGravityPower();
        CheckGroundByRay();
    }
    #region アニメーション用メソッド
    public void SetMoveAnimation()
    {
        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - prevPlayerPos;

        if (direction.magnitude > 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    #endregion

    #region 回転用メソッド
    public void RotatePlayer(float _inputH2, float _inputV2, float _maxControllerLerance)
    {
        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - prevPlayerPos;
        direction = direction - new Vector3(0, direction.y, 0);
        if (direction.magnitude > 0)
        {
            //RotateCharaterByOneAxis 左スティックだけ採用するときはこちらを使う
            RotateCharaterByTwoAxis(direction, _inputH2, _inputV2, _maxControllerLerance);
        }
        else
        {
            //動かないときでも方向転換できるように
            RotateCharacterByAxis2(_inputH2, _inputV2);
        }

        //計算後、入力前の位置を更新する
        prevPlayerPos = transform.position;
    }


    private void RotateCharaterByOneAxis(Vector3 _direction, float _maxControllerLerance)
    {
        if ((_direction.magnitude > _maxControllerLerance))
            RotateCharacterByAxis1(_direction);
    }

    private void RotateCharaterByTwoAxis(Vector3 _direction, float _inputH2, float _inputV2, float _maxControllerLerance)
    {
        if ((_direction.magnitude > _maxControllerLerance) &&
    (_inputH2 == 0 && _inputV2 == 0))
            RotateCharacterByAxis1(_direction);
        else
            RotateCharacterByAxis2(_inputH2, _inputV2);
    }

    private void RotateCharacterByAxis1(Vector3 _direction)
    {
        transform.rotation = Quaternion.LookRotation(new Vector3
(_direction.x, 0, _direction.z));
    }

    private void RotateCharacterByAxis2(float _h, float _v)
    {
        //なぜか右のスティックが上下反転しているため、
        //vの値をマイナスにした
        Vector3 velocity = new Vector3(_h, 0, -_v) * Time.deltaTime;

        if (velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(new Vector3
    (velocity.x, 0, velocity.z));
    }
    #endregion

    #region 移動用メソッド
    public void MoveByRigidBody(float _h, float _v, float _maxControllerLerance)
    {
        Vector3 velocity = new Vector3(_h * moveSpeed, 0, _v * moveSpeed) * Time.deltaTime;

        if (velocity.magnitude > _maxControllerLerance)
            //現在の位置＋入力した数値の場所に移動する
            rb.MovePosition(transform.position + velocity);
    }
    #endregion

    #region 被ダメージ
    public void Damgae()
    {
        if (isVincible || status.nowHp <= 0)
            return;
        if (status.nowHp > 0)
            status.nowHp--;

        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        Renderer renderer;
        renderer = transform.Find("PlayerMesh").GetComponent<Renderer>();
        for (int i = 0; i < status.InvincibleTotalTime; i++)
        {
            renderer.enabled = !renderer.enabled;
            isVincible = true;
            yield return new WaitForSeconds(status.InvincibleInterval);
        }
        isVincible = false;
    }

    public void CheckDied()
    {
        if (status.nowHp <= 0)
            this.gameObject.SetActive(false);
    }
    #endregion

    public void Jump()
    {
        if (isGround)
        {
            Vector3 jumpVelocity = transform.up * Time.fixedDeltaTime * 60 * status.JumpPower;
            rb.velocity = jumpVelocity;
            isGround = false;
        }
    }

    private void CheckGroundByRay()
    {
        RaycastHit hit;
        Vector3 downDirection = (transform.up * -1);
        float distanceToGround = 0.02f;
        if (Physics.Raycast(transform.position, downDirection, out hit, distanceToGround))
        {
            Debug.Log("isGround" + isGround);
            //Debug.Log("hit.distance" + hit.distance);
            //Debug.Log("hit.transform.name" + hit.transform.name);
            if (!isGround)
            {
                if (hit.transform.gameObject.layer == 9/*Ground*/)
                isGround = true;
            }
        }
        Debug.DrawRay(transform.position, downDirection, Color.red);
    }
    private void AddGravityPower()
    {
        rb.velocity += Physics.gravity * status.GravityFactor;
    }
}
