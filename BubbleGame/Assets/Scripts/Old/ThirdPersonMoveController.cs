using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラー用アセット
public class ThirdPersonMoveController : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// アタッチするコンポーネント
    /// </summary>
    Rigidbody rb;
    Animator animator;

    /// <summary>
    /// 入力 
    /// </summary>
    float inputH;
    float inputV;

    float inputH2;
    float inputV2;

    /// <summary>
    /// 移動 
    /// </summary>
    [SerializeField] private float maxControllerLerance = 0.02f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float backSpeed = 2f;
    float moveSpeed;

    //入力前の位置
    Vector3 prevPlayerPos;
    #endregion

    #region 初期化
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        moveSpeed = walkSpeed;
        prevPlayerPos = transform.position;
    }
    #endregion

    #region 更新
    void Update()
    {
        //入力
        inputH = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
        inputV = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;

        inputH2 = GamePad.GetAxis(GamePad.Axis.RightStick, GamePad.Index.One).x;
        //y軸を反転させる
        inputV2 = -GamePad.GetAxis(GamePad.Axis.RightStick, GamePad.Index.One).y;
    }
    #endregion

    #region Fixed更新
    private void FixedUpdate()
    {
        MoveWithRigidBody(inputH, inputV);

        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - prevPlayerPos;

        bool canSetInputVelocity = false;

        if (direction.magnitude > 0 ||
            (inputH2 != 0 || inputV2 != 0))
        {
            //RotateCharaterByOneAxis 左スティックだけ採用するときはこちらを使う
            RotateCharaterByTwoAxis(direction);

            SetMoveAnimationParameter(ref canSetInputVelocity, direction.magnitude);

            //走るときのスピードを増加させる
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                moveSpeed = runSpeed;

            //バックするときのスピードを低下させる
            if ((inputH2 != 0 || inputV2 != 0) &&
                ((inputH * inputH2 < 0) || (inputV * inputV2 > 0))//別方向のスティックを倒す判定
                )
            {
                moveSpeed = backSpeed;
                animator.SetBool("Backing", true);
            }
            else if (inputH2 <= 0.01f && inputV2 <= 0.01f)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                    moveSpeed = walkSpeed;
                animator.SetBool("Backing", false);
            }
        }
        else
        {
            //動かないときでも方向転換できるように
            RotateCharacterByAxis2(inputH2, inputV2);

            if (inputH <= 0 && inputV <= 0)
            {
                //スピードを元に戻す
                moveSpeed = walkSpeed;
                ResetAnimationParamater(direction.magnitude);
            }
        }
        //計算後、入力前の位置を更新する
        prevPlayerPos = transform.position;

    }
    #endregion

    #region 回転用メソッド
    private void RotateCharaterByOneAxis(Vector3 _direction)
    {
        if ((_direction.magnitude > maxControllerLerance))
            RotateCharacterByAxis1(_direction);
    }

    private void RotateCharaterByTwoAxis(Vector3 _direction)
    {
        if ((_direction.magnitude > maxControllerLerance) &&
    (inputH2 == 0 && inputV2 == 0))
            RotateCharacterByAxis1(_direction);
        else
            RotateCharacterByAxis2(inputH2, inputV2);
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
    private void MoveWithRigidBody(float _h, float _v)
    {
        Vector3 velocity = new Vector3(_h * moveSpeed, 0, _v * moveSpeed) * Time.deltaTime;

        if (velocity.magnitude > maxControllerLerance)
            //現在の位置＋入力した数値の場所に移動する
            rb.MovePosition(transform.position + velocity);
    }

    //private void MoveWithRigidBodyBanRightMove(float _h, float _v)
    //{
    //    if (_h < 0)
    //    {
    //        Vector3 velocity = new Vector3(_h * moveSpeed, 0, _v * moveSpeed) * Time.deltaTime;

    //        if (velocity.magnitude > maxControllerLerance)
    //            //現在の位置＋入力した数値の場所に移動する
    //            rb.MovePosition(transform.position + velocity);
    //    }
    //}
    //private void MoveWithRigidBodyBanLeftMove(float _h, float _v)
    //{
    //    if (_h >= 0)
    //    {
    //        Vector3 velocity = new Vector3(_h * moveSpeed, 0, _v * moveSpeed) * Time.deltaTime;

    //        if (velocity.magnitude > maxControllerLerance)
    //            //現在の位置＋入力した数値の場所に移動する
    //            rb.MovePosition(transform.position + velocity);
    //    }
    //}
    //private void MoveWithRigidBodyBanUpMove(float _h, float _v)
    //{
    //    if (_v < 0)
    //    {
    //        Vector3 velocity = new Vector3(_h * moveSpeed, 0, _v * moveSpeed) * Time.deltaTime;

    //        if (velocity.magnitude > maxControllerLerance)
    //            //現在の位置＋入力した数値の場所に移動する
    //            rb.MovePosition(transform.position + velocity);
    //    }
    //}
    //private void MoveWithRigidBodyBanDownMove(float _h, float _v)
    //{
    //    if (_v >= 0)
    //    {
    //        Vector3 velocity = new Vector3(_h * moveSpeed, 0, _v * moveSpeed) * Time.deltaTime;

    //        if (velocity.magnitude > maxControllerLerance)
    //            //現在の位置＋入力した数値の場所に移動する
    //            rb.MovePosition(transform.position + velocity);
    //    }
    //}
    #endregion

    #region アニメーション用メソッド
    private void SetMoveAnimationParameter(ref bool canSetInputVelocity, float _velocity)
    {
        animator.SetBool("Moving", true);

        canSetInputVelocity = true;

        //InpuVelocityとNowVelocityで移動と走るモーションの切り替えを判断する
        //設定はプレイヤーのアニメーターにある
        if (canSetInputVelocity)
        {


            animator.SetFloat("FirstInputVelocity", _velocity);
            canSetInputVelocity = false;
        }
        animator.SetFloat("NowVelocity", _velocity);
    }

    private void ResetAnimationParamater(float _velocity)
    {
        animator.SetBool("Moving", false);
        animator.SetFloat("FirstInputVelocity", _velocity);
        animator.SetFloat("NowVelocity", _velocity);
    }
    #endregion
}
