using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

namespace TowerDefense
{
    public class MainMenuPanel : SingletonBase<MainMenuPanel>
    {
        #region MainMenu
        [SerializeField] private Button m_ContinueButton;
        [SerializeField] private GameObject m_CheckNewGamePanel;
        [SerializeField] private Animator m_AnimatedMenuCanvas;
        [SerializeField] private string m_ContinueAnimationName = "ContinueGameSceneSwitch";
        [SerializeField] private string m_NewGameAnimationName = "NewGameSceneSwitch";
        //[SerializeField] private Button NewYesButton;
        //[SerializeField] private Button NewNoButton;

        private void Start()
        {
            m_ContinueButton.interactable = FileHandler.HasFile(MapCompletion.m_FileName);
            m_CheckNewGamePanel.SetActive(false);
        }
        public void OnNewGameButton()
        {
            m_CheckNewGamePanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnContinueButton()
        {
            m_AnimatedMenuCanvas.Play(m_ContinueAnimationName);
        }

        public void OnQuitButton()
        {
            Application.Quit();
        }
        #endregion

        #region Check
        public void OnYesButton()
        {
            FileHandler.Reset(MapCompletion.m_FileName);
            FileHandler.Reset(Upgrades.filename);
            m_AnimatedMenuCanvas.Play(m_NewGameAnimationName);
        }
        public void OnNoButton()
        {
            gameObject.SetActive(true);
            m_CheckNewGamePanel.SetActive(false);
        }
        #endregion
    }
}