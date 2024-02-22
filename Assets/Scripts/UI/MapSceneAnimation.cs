using SpaceShip;
using System.Collections;
using UnityEngine;

public class MapSceneAnimation : MonoBehaviour
{
    [SerializeField] private string m_StartMapAnimationName;
    [SerializeField] private string m_OnUpgradeAnimationName;
    [SerializeField] private string m_OnExitUpgradeAnimationName;
    public string LoadLevelAnimationName = "FadeOutLevelAnimation";

    private Animator m_AnimatedMenuCanvas;

    private void Start()
    {
        m_AnimatedMenuCanvas = GetComponent<Animator>();
        m_AnimatedMenuCanvas.Play(m_StartMapAnimationName);
    }
    public void OnButtonUpgrade()
    {
        m_AnimatedMenuCanvas.Play(m_OnUpgradeAnimationName);
    }

    public void OnExitUpgrade()
    {
        m_AnimatedMenuCanvas.Play(m_OnExitUpgradeAnimationName);
    }
    public void AnimationOnLoad(Episode episode) // берет у LevelMap инфу об эпизоде и запускает корутину, в котоую передат эпизод
    {
        this.StartCoroutine(AnimateThenLoadLevel(episode)); 
    }
    /// <summary>
    /// Запускает анимацию и ждет, когда она в своем конце изменит состояние метки конца анимации (m_MarkEndAnimation).
    /// Тогда вызывает загрузку эпизода/
    /// </summary>
    /// <param name="episode"></param>
    /// <returns></returns>
    IEnumerator AnimateThenLoadLevel(Episode episode)
    {
        m_AnimatedMenuCanvas.Play(LoadLevelAnimationName);
        while (m_MarkEndAnimation == false)
        {
            yield return null;
        }
        LevelSequenceController.Instance.StartEpisode(episode);
    }
    private bool m_MarkEndAnimation; // Метка конца анимации
    public void MarkOfEndAnimation(bool isEnd) => m_MarkEndAnimation = isEnd; 
}
