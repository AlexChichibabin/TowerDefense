using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSceneAnimation : MonoBehaviour
{
    [SerializeField] private string m_StartLevelAnimationName;
    [SerializeField] private string m_LevelFadeOutAnimationName;

    private Animator m_Animator;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.Play(m_StartLevelAnimationName);
    }

    public void AnimationOnLoad(int i) // берет инфу о сцене и запускает корутину, в которую передает сцену
    {
        this.StartCoroutine(AnimateAndLoadLevel(i));
    }
    IEnumerator AnimateAndLoadLevel(int scene) // Запускает анимацию и ждет, когда она в своем конце
                                               // изменит состояние Метки конца анимации (m_MarkEndAnimation).
                                               // Тогда вызывает загрузку сцены
    {
        m_Animator.Play(m_LevelFadeOutAnimationName);
        while (m_MarkEndAnimation == false)
        {
            yield return null;
        }
        SceneManager.LoadScene(scene);
    }
    private bool m_MarkEndAnimation;
    public void MarkOfEndAnimation()
    {
        m_MarkEndAnimation = true;
    }
}
