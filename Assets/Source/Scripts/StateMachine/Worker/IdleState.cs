namespace BuilderStory
{
    public class IdleState : IBehaviour
    {
        private readonly IWorkable _worker;

        public IdleState(IWorkable worker)
        {
            _worker = worker;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            return _worker.IsBusy != true;
        }

        public void Update()
        {
        }
    }
}
