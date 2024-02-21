using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class CheckNewGamePanel : MonoBehaviour
    {
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
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
            gameObject.SetActive(false);
        }

        public void OnYesButton()
        {
            FileHandler.Reset(MapCompletion.m_FileName);
            FileHandler.Reset(Upgrades.filename);
            //SceneManager.LoadScene(1);
            m_MenuPanelAnimator.Play("MenuToMapSceneSwitch");
            m_MapLevelControler.gameObject.SetActive(true);
            m_Levels.gameObject.SetActive(true);
            m_Upgrades.gameObject.SetActive(true);
        }

        public void OnNoButton()
        {
            MainMenuPanel.Instance.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}