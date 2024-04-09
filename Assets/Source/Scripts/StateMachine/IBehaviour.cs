namespace BuilderStory.States
{
    public interface IBehaviour
    {
        public void Enter();

        public void Exit();

        public void Update();

        public bool IsReady();
    }
}
