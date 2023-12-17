namespace BuilderStory
{
    public class StateMachine
    {
        private IBehaviour _currentBehaviour;

        public StateMachine(IBehaviour startBehaviour)
        {
            Reset();

            _currentBehaviour = startBehaviour;
            _currentBehaviour.Enter();
        }

        public IBehaviour CurrentState => _currentBehaviour;

        public void Update()
        {
            _currentBehaviour?.Update();
        }

        public void Reset()
        {
            _currentBehaviour?.Exit();

            _currentBehaviour = null;
        }

        public void ChangeState(IBehaviour behaviour)
        {
            Reset();

            _currentBehaviour = behaviour;
            _currentBehaviour.Enter();
        }
    }
}
