using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace TowerDefense
{
    public class MainMenuSceneChange : SingletonBase<MainMenuSceneChange>
    {
        [SerializeField] private Button m_PauseButton;
        [SerializeField] private GameObject m_PausePanel;
        [SerializeField] private GameObject m_MapToMenuAnimationGameObject;

        public event Action<bool> OnGamePaused;

        public void PauseGame()
        {
            Time.timeScale = 0.0f;
            m_PauseButton.interactable = false;
            m_PausePanel.SetActive(true);
            Instance.OnGamePaused?.Invoke(true);
        }
        public void ContinueGame()
        {
            Time.timeScale = 1.0f;
            m_PauseButton.interactable = true;
            m_PausePanel.SetActive(false);
            Instance.OnGamePaused?.Invoke(false);
        }
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1.0f;
        }
        public void BackToMainMenu() // Только для кнопки в меню карты
        {
            SceneManager.LoadScene(0);
        }

        public void LoadMainMap()
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1.0f;
        }

        #region Animated
        public void LoadMainFromMapMenu()
        {
            m_MapToMenuAnimationGameObject.SetActive(true);
        }
        #endregion
    }
}