using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShip
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Scores;
        [SerializeField] private Text m_Time;

        [SerializeField] private Text m_Result;

        [SerializeField] private Text m_ButtonNextText;
        [SerializeField] private Button m_MainMenuButton;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";
            m_ButtonNextText.text = success ? "Next" : "Restart";

            /*m_Kills.text = "Kills : " + levelResults.NumKills.ToString();
            m_Scores.text = "Scores : " + levelResults.Scores.ToString();
            m_Time.text = "Time : " + levelResults.Time.ToString();*/

            //Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_Success) LevelSequenceController.Instance.AdvanceLevel();
            else LevelSequenceController.Instance.RestartLevel();
        }
        public void OnButtonMainMenu()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickname);
        }
    }
}