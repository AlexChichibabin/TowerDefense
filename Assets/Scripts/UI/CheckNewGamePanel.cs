using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class CheckNewGamePanel : MonoBehaviour
    {
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private Animator m_AnimatedMenuCanvas;
        [SerializeField] private string m_NewGameAnimationName;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnYesButton()
        {
            FileHandler.Reset(MapCompletion.m_FileName);
            FileHandler.Reset(Upgrades.filename);
            m_AnimatedMenuCanvas.Play(m_NewGameAnimationName);
        }

        public void OnNoButton()
        {
            MainMenuPanel.Instance.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}