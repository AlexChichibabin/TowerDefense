using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShip
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        //[SerializeField] private Text m_Kills;
        //[SerializeField] private Text m_Scores;
        //[SerializeField] private Text m_Time;

        [SerializeField] private Text m_Result;

        [SerializeField] private Text m_ButtonNextText;
        [SerializeField] private OnEnableSound m_OnEnableSound;
        [SerializeField] private LevelSceneAnimation m_LevelAnimation;
        //[SerializeField] private Button m_MainMenuButton;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            m_Success = success;
            m_OnEnableSound.OnEnabled(success);

            m_Result.text = success ? "������" : "���������";
            m_ButtonNextText.text = success ? "������" : "������";

            /*m_Kills.text = "Kills : " + levelResults.NumKills.ToString();
            m_Scores.text = "Scores : " + levelResults.Scores.ToString();
            m_Time.text = "Time : " + levelResults.Time.ToString();*/

            //Time.timeScale = 0;
        }

        private void UpdateCurrentLevelStats() 
        {
            int timeBonus = (int)(LevelController.Instance.ReferenceTime - (int)LevelController.Instance.LevelTime);

            if(timeBonus > 0)
            {

            }
        }
        
        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_Success) LevelSequenceController.Instance.AdvanceLevel();
            else LevelSequenceController.Instance.RestartLevel();
        }
        public void OnButtonMapMenu()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;
            m_LevelAnimation.AnimationOnLoad(1);
            //SceneManager.LoadScene(LevelSequenceController.MapSceneNickname);
        }

        public void OnButtonMainMenu()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;
            m_LevelAnimation.AnimationOnLoad(0);
            //SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickname);
        }
    }
}