using Codetox.FSM;

namespace Player
{
    public class PlayerStateMachine : StateMachine<PlayerStateMachine>
    {
        public MoveState moveState;

        public override State<PlayerStateMachine> InitialState => moveState;

        protected override void Init()
        {
        }
    }
}