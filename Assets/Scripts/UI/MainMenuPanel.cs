using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

namespace TowerDefense
{
    public class MainMenuPanel : SingletonBase<MainMenuPanel>
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private GameObject CheckNewGamePanel;
        [SerializeField] private Animator m_MenuPanelAnimator;
        [SerializeField] private GameObject m_Levels;
        private MapCompletion m_MapLevelControler;
        private Upgrades m_Upgrades;
        private void Start()
        {
            m_MapLevelControler = MapCompletion.Instance;
            m_Upgrades = Upgrades.Instance;

            m_MapLevelControler.gameObject.SetActive(false);
            m_Upgrades.gameObject.SetActive(false);
            m_Levels.gameObject.SetActive(false);
            continueButton.interactable = FileHandler.HasFile(MapCompletion.m_FileName);
        }
        public void OnNewGameButton()
        {
            CheckNewGamePanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnContinueButton()
        {
            //SceneManager.LoadScene(1);
            m_MenuPanelAnimator.Play("MenuToMapSceneSwitch");
            m_MapLevelControler.gameObject.SetActive(true);
            m_Levels.gameObject.SetActive(true);
            m_Upgrades.gameObject.SetActive(true);
        }

        public void OnQuitButton()
        {
            Application.Quit();
        }
    }
}