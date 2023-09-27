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
        private void Start()
        {
            continueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
        }
        public void OnNewGameButton()
        {
            CheckNewGamePanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnContinueButton()
        {
            SceneManager.LoadScene(1);
        }

        public void OnQuitButton()
        {
            Application.Quit();
        }
    }
}