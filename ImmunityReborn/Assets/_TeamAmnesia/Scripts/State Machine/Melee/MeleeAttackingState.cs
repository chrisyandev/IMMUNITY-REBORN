using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackingState : MeleeBaseState
{
    private readonly int AnimatorAttackStateName = Animator.StringToHash("Attack2SwordShield");
    private readonly int EmptyDefaultStateName = Animator.StringToHash("Empty Default");

    private const float TransitionDuration = 0.1f;

    public MeleeAttackingState(MeleeStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.WeaponDamager.SetDamage(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AnimatorAttackStateName, TransitionDuration, 1);
    }

    public override void Tick(float deltaTime)
    {
        MoveToPlayer(deltaTime);

        FacePlayer(deltaTime);

        UpdateLocomotionAnimator(deltaTime);

        if (GetPlayingAnimationTimeNormalized(stateMachine.Animator, 1) >= 1.0f)
        {
            stateMachine.SwitchState(new MeleeRetreatingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.NavMeshAgent.ResetPath();
        stateMachine.NavMeshAgent.velocity = Vector3.zero;

        stateMachine.Animator.CrossFade(EmptyDefaultStateName, TransitionDuration, 1); // without this, gets stuck at end of attack animation
    }
}
