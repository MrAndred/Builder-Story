using UnityEngine.SceneManagement;

namespace BuilderStory
{
    public static class BuilderStoryUtil
    {
        private const int UnplayableScenesCount = 1;

        public static int GetLevelNumber()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex - UnplayableScenesCount;
            int humanFormat = sceneIndex + 1;

            return humanFormat;
        }

        public static int CalculateLevelIndex(int levelNumber)
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;

            if (levelNumber >= sceneCount)
            {
                return levelNumber % SceneManager.sceneCountInBuildSettings + UnplayableScenesCount;
            }

            return levelNumber;
        }
    }
}
