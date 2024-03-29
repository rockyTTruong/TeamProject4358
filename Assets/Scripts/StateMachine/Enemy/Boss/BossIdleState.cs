using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    public BossIdleState(BossStateMachine sm) : base(sm) { }

    private int idleHash = Animator.StringToHash("Idle");
    private float crossFixedDuration = 0.3f;
    private float timer;

    private float idleTime = 2f;
    public override void Enter()
    {
        if (sm.below50Percent) idleTime = 1f;
        timer = 0f;
        PlayAnimation(idleHash, crossFixedDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        if (sm.targetManager.GetCurrentTarget() == null) return;
        Move(Vector3.zero);
        FaceTarget();

        timer += Time.deltaTime;
        if (timer >= idleTime)
        {
            if (sm.below50Percent)
            {
                float r = Random.Range(0f, 100f);
                if (r < 25)
                {
                    sm.SwitchState(new BossMoveToCenterState(sm));
                    return;
                }
            }

            if (sm.targetManager.GetDistanceToTarget() > 12f)
            {
                float r = Random.Range(0f, 100f);
                if (r < 70)
                {
                    sm.SwitchState(new BossCastingState(sm));
                    return;
                }
                else
                {
                    sm.SwitchState(new BossChasingState(sm));
                    return;
                }
            }
            else if (sm.targetManager.GetDistanceToTarget() > 8f)
            {
                float r = Random.Range(0f, 100f);
                if (r < 20)
                {
                    sm.SwitchState(new BossCastingState(sm));
                    return;
                }
                else if (r < 50)
                {
                    sm.SwitchState(new BossApproachingState(sm));
                    return;
                }
                else
                {
                    sm.SwitchState(new BossChasingState(sm));
                    return;
                }

            }
            else 
            {
                float r = Random.Range(0f, 100f);
                if (r < 50)
                {
                    sm.SwitchState(new BossApproachingState(sm));
                    return;
                }
                else
                {
                    sm.SwitchState(new BossBackStepState(sm));
                    return;
                }
            }
        }
    }
}
