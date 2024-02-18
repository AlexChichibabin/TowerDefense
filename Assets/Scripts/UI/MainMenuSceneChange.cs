using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class MainMenuSceneChange : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadMainMap()
        {
            SceneManager.LoadScene(1);
        }
    }
}