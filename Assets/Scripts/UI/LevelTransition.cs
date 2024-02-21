using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public void SceneChange(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
