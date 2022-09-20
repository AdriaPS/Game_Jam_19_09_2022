using System;
using Codetox.Events;
using Codetox.FSM;

namespace Player
{
    [Serializable]
    public class MoveState : State<PlayerStateMachine>
    {
        public MovementController movementController;
        public JumpController jumpController;
        public Event<bool> onJumpEvent;
        public Event<bool> onGroundedEvent;

        public MoveState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter(State<PlayerStateMachine> previousState)
        {
            onJumpEvent.AddListener(jumpController.OnJump);
            onGroundedEvent.AddListener(jumpController.OnGrounded);
        }

        public override void Update(float deltaTime)
        {
        }

        public override void FixedUpdate(float deltaTime)
        {
            movementController.Move();
            jumpController.ApplyGravity(deltaTime);
        }

        public override void Exit(State<PlayerStateMachine> nextState)
        {
            onJumpEvent.RemoveListener(jumpController.OnJump);
            onGroundedEvent.RemoveListener(jumpController.OnGrounded);
        }
    }
}