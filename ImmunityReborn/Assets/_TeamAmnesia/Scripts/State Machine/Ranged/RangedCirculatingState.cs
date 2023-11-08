using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCirculatingState : RangedBaseState
{
    private readonly int CirculatingStateName = Animator.StringToHash("Circulating");

    private const float CirculatingSpeed = 1.1f;
    private const float CrossFadeDuration = 0.1f;
    private float duration;
    private bool isClockwise;

    public RangedCirculatingState(RangedStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        isClockwise = RandomBoolean();
        // Circle for 3 seconds every time, @TODO to parameterize this
        duration = 3f;
        stateMachine.Animator.CrossFadeInFixedTime(CirculatingStateName, CrossFadeDuration, -1);
    }

    public override void Tick(float deltaTime)
    {
        FacePlayer(deltaTime);
        MoveAroundPlayer(deltaTime);
        UpdateCirculatingAnimator(deltaTime);
        duration -= deltaTime;
        if (duration <= 0.0f)
        {
            stateMachine.SwitchState(new RangedAdvancingState(stateMachine));
        }
    }

    public override void Exit()
    {
    }

    private void MoveAroundPlayer(float deltaTime)
    {
        if (stateMachine.NavMeshAgent.isOnNavMesh)
        {
            Vector3 direction = isClockwise ? -stateMachine.transform.right : stateMachine.transform.right;

            stateMachine.NavMeshAgent.destination = stateMachine.transform.position + direction * CirculatingSpeed;

            Move(stateMachine.NavMeshAgent.desiredVelocity.normalized * CirculatingSpeed, deltaTime);
        }

        stateMachine.NavMeshAgent.velocity = stateMachine.CharacterController.velocity;
        stateMachine.NavMeshAgent.nextPosition = stateMachine.CharacterController.transform.position;
    }

    private bool RandomBoolean()
    {
        return Random.Range(0.0f, 1.0f) < 0.5f;
    }
}
