using SpaceShip;
using System;
using System.Collections;
using UnityEngine;

public class MapSceneAnimation : MonoBehaviour
{
    [SerializeField] private string m_StartMapAnimationName = "StartMapLevel";
    [SerializeField] private string m_OnUpgradeAnimationName = "OnUpgradeButton";
    [SerializeField] private string m_OnExitUpgradeAnimationName = "OnExitUpgrade";
    [SerializeField] private string m_MapToMenuAnimationName = "MapToMenuSceneSwitch";
    public string LoadLevelAnimationName = "FadeOutLevelAnimation";

    private Animator m_MenuAnimator;

    private void Start()
    {
        m_MenuAnimator = GetComponent<Animator>();
        m_MenuAnimator.Play(m_StartMapAnimationName);
    }
    public void OnButtonUpgrade()
    {
        m_MenuAnimator.Play(m_OnUpgradeAnimationName);
    }

    public void OnExitUpgrade()
    {
        m_MenuAnimator.Play(m_OnExitUpgradeAnimationName);
    }
    public void OnButtonMainMenu() // Только для кнопки в меню карты
    {
        //SceneManager.LoadScene(0);
        m_MenuAnimator.Play(m_MapToMenuAnimationName);
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
        m_MenuAnimator.Play(LoadLevelAnimationName);
        while (m_MarkEndAnimation == false)
        {
            yield return null;
        }
        LevelSequenceController.Instance.StartEpisode(episode);
    }
    private bool m_MarkEndAnimation; // Метка конца анимации
    public void MarkEndAnimation(int isEnd)
    {
        if (isEnd > 1) isEnd = 1;
            m_MarkEndAnimation = Convert.ToBoolean(isEnd);

    }


}
