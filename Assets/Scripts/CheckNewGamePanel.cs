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

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnYesButton()
        {
            FileHandler.Reset(MapCompletion.filename);
            SceneManager.LoadScene(1);
        }

        public void OnNoButton()
        {
            gameObject.SetActive(false);
            //MainMenuPanel.Instance.gameObject.SetActive(true);
        }
    }
}