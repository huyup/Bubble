using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public enum AttackButtonState
{
    NoInput,
    ButtonDown,
    ButtonKeep,
    ButtonUp,
}

public class PlayerInputManager : MonoBehaviour
{
    AttackButtonState attackButtonState;

    [SerializeField]
    private float maxControllerLerance = 0.02f;

    PlayerController playerController;
    PlayerStatus property;
    PlayerBubbleShooting bubbleShooting;

    int playerNum;

    /// <summary>
    /// 左スティックの入力量
    /// </summary>
    float leftXAsisInput;
    float leftYAsisInput;

    /// <summary>
    /// 右スティックの入力量
    /// </summary>
    float rightXAsisInput;
    float rightYAsisInput;

    bool isOverMoveableSize = false;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        property = GetComponent<PlayerStatus>();
        bubbleShooting = GetComponent<PlayerBubbleShooting>();
        playerNum = property.Num;

        attackButtonState = AttackButtonState.NoInput;
    }

    private void Update()
    {
        //左スティック
        leftXAsisInput = GamePad.GetAxis(GamePad.Axis.LeftStick, (GamePad.Index)playerNum).x;
        leftYAsisInput = GamePad.GetAxis(GamePad.Axis.LeftStick, (GamePad.Index)playerNum).y;

        //右スティック
        rightXAsisInput = GamePad.GetAxis(GamePad.Axis.RightStick, (GamePad.Index)playerNum).x;
        //y軸を反転させる
        rightYAsisInput = -GamePad.GetAxis(GamePad.Axis.RightStick, (GamePad.Index)playerNum).y;

        //攻撃ボタン
        if (GamePad.GetButtonDown(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            bubbleShooting.CreateTheBubbleSet();
            attackButtonState = AttackButtonState.ButtonDown;
            bubbleShooting.SetAttackAnimation(attackButtonState);
        }
        if (GamePad.GetButton(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            //長押しの時かつ泡が一定の大きさ以上の時は、移動を禁止させる
            isOverMoveableSize = bubbleShooting.CheckIsBubbleOverMoveableSize();
            bubbleShooting.ChangeTheBubbleScale();
            attackButtonState = AttackButtonState.ButtonKeep;

            bubbleShooting.SetAttackAnimation(attackButtonState);
        }
        if (GamePad.GetButtonUp(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            bubbleShooting.TurnOnThePushFlag();
            //ボタンを離したとき、移動を回復させる
            isOverMoveableSize = false;

            attackButtonState = AttackButtonState.ButtonUp;
            bubbleShooting.SetAttackAnimation(attackButtonState);
        }

        if (GamePad.GetButtonDown(GamePad.Button.X, (GamePad.Index)playerNum))
        {
            playerController.Jump();
        }
        Debug.Log("isOverMoveableSize" + isOverMoveableSize);
    }

    private void FixedUpdate()
    {
        if (!isOverMoveableSize)
        {
            playerController.MoveByRigidBody(leftXAsisInput, leftYAsisInput, maxControllerLerance);
        }
        playerController.SetMoveAnimation();
        playerController.RotatePlayer(rightXAsisInput, rightYAsisInput, maxControllerLerance);
    }

}
