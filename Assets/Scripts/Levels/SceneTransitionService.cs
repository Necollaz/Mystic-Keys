using UnityEngine.SceneManagement;

namespace Levels
{
    public class SceneTransitionService
    {
        public void ReloadCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        public void LoadSceneByIndex(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}