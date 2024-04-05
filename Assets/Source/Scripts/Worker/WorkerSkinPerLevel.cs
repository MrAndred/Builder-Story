using UnityEngine;

namespace BuilderStory
{
    [CreateAssetMenu(fileName = "WorkerSkins", menuName = "BuilderStory/WorkerSkins")]
    public class WorkerSkinPerLevel : ScriptableObject
    {
        private const int DefaultIndex = 0;
        private const int DecreaseIndex = 1;

        [SerializeField] private Worker[] _workerTemplates;

        public Worker GetSkin(int level)
        {
            if (level < 0 || level > _workerTemplates.Length)
            {
                return _workerTemplates[DefaultIndex];
            }

            int index = (level - DecreaseIndex) % _workerTemplates.Length;

            return _workerTemplates[index];
        }
    }
}
