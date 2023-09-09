using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerState
{
    public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private int freelookHash =  Animator.StringToHash("FreeLookBlendTree");
    private int blendSpeedHash = Animator.StringToHash("FreeLookBlendSpeed");
    private float crossFixedDuration = 0.1f;
    private float blendValue;
    private Vector3 movement;

    public override void Enter()
    {
        InputReader.Instance.EnableFreelookInputReader();
        InputReader.Instance.SouthButtonPressEvent += Jump;
        InputReader.Instance.EastButtonPressEvent += Dodge;
        InputReader.Instance.WestButtonPressEvent += NormalAttack;
        InputReader.Instance.RightStickPressEvent += LockOnMode;
        InputReader.Instance.DpadLeftButtonPressEvent += LockOnPreviousTarget;
        InputReader.Instance.DpadRightButtonPressEvent += LockOnNextTarget;
        playerStateMachine.animator.CrossFadeInFixedTime(freelookHash, crossFixedDuration);
    }

    public override void Exit()
    {
        InputReader.Instance.SouthButtonPressEvent -= Jump;
        InputReader.Instance.EastButtonPressEvent -= Dodge;
        InputReader.Instance.WestButtonPressEvent -= NormalAttack;
        InputReader.Instance.RightStickPressEvent -= LockOnMode;
        InputReader.Instance.DpadLeftButtonPressEvent -= LockOnPreviousTarget;
        InputReader.Instance.DpadRightButtonPressEvent -= LockOnNextTarget;
    }

    public override void Tick()
    {
        UpdateAnimator();
        HandleCameraMovement();
        HandlePlayerMovement();
        if (!playerStateMachine.controller.isGrounded) playerStateMachine.SwitchState(new PlayerFallingState(playerStateMachine));
        if (InputReader.Instance.isPressingWestButton) NormalAttack();
        if (InputReader.Instance.isPressingSouthButton) Jump();
    }

    private void HandlePlayerMovement()
    {
        movement = CalculateMovement();
        if (InputReader.Instance.leftStickValue == Vector2.zero)
        {
            Move(Vector3.zero);
        }
        else
        {
            Move(movement * playerStateMachine.movementSpeed);
            ChangeDirection(movement);
        }
    }

    private void UpdateAnimator()
    {
        if (InputReader.Instance.leftStickValue == Vector2.zero)
        {
            playerStateMachine.animator.SetFloat(blendSpeedHash, 0f, 0.1f, Time.deltaTime);
            return;
        }
        else blendValue = Mathf.Max(Mathf.Abs(InputReader.Instance.leftStickValue.x), Mathf.Abs(InputReader.Instance.leftStickValue.y));

        if (blendValue > 0.7f) blendValue = 1f;
        else blendValue = 0.5f;

        playerStateMachine.animator.SetFloat(blendSpeedHash, blendValue, 0.1f, Time.deltaTime);
    }

    private void NormalAttack()
    {
        playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, playerStateMachine.comboManager.normalCombo.attack[0]));
    }

    private void Jump()
    {
        playerStateMachine.SwitchState(new PlayerJumpState(playerStateMachine));
    }

    private void Dodge()
    {
        if (CalculateMovement() == Vector3.zero) return;
        playerStateMachine.SwitchState(new PlayerDodgingState(playerStateMachine));
    }
    
    private void LockOnMode()
    {
        if (playerStateMachine.playerTargetManager.GetCurrentTarget() == null) playerStateMachine.playerTargetManager.LockOnTarget();
        else playerStateMachine.playerTargetManager.DisableLockOn();
    }

    private void LockOnNextTarget()
    {
        if (playerStateMachine.playerTargetManager.GetCurrentTarget() == null) return;
        playerStateMachine.playerTargetManager.NextTarget();
    }

    private void LockOnPreviousTarget()
    {
        if (playerStateMachine.playerTargetManager.GetCurrentTarget() == null) return;
        playerStateMachine.playerTargetManager.PreviouTarget();
    }
}
