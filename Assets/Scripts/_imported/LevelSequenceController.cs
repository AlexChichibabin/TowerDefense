using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShip
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "MainMenu";
        public static string MapSceneNickname = "LevelMap";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public static Ship PlayerShip { get; set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public TotalStatistics MainStatistics { get; private set; }

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            //сбрасываем статы перед началом эпизода
            /*LevelStatistics = PlayerStatistics.Instance;
            LevelStatistics.Reset();
            MainStatistics = TotalStatistics.Instance;*/

            if (CurrentEpisode.Levels.Length > 0 && CurrentEpisode.Levels[CurrentLevel] != null)

                SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            //SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            //CalculateLevelStatistic();
            //CalculateMainStatistic();
            //MainStatistics.SaveMainStatistic();
            //PlayerPrefs.Save();

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);
            //print(LastLevelResult);
        }

        public void AdvanceLevel()
        {
            //LevelStatistics.Reset();
            if (CurrentEpisode)
            {
                CurrentLevel++;

                if (CurrentEpisode.Levels.Length <= CurrentLevel)
                {
                    SceneManager.LoadScene(MapSceneNickname);
                }
                else
                {
                    SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
                }
            }
            else
            {
                SceneManager.LoadScene(MapSceneNickname);
            }        
        }

        /*private void CalculateLevelStatistic()
        {
            LevelStatistics.NumKills = Player.Instance.NumKills;
            LevelStatistics.Scores = Player.Instance.Score;
            LevelStatistics.Time = (int)LevelController.Instance.LevelTime;
        }
        private void CalculateMainStatistic()
        {
            MainStatistics.TotalNumKills += Player.Instance.NumKills;
            MainStatistics.TotalScores += Player.Instance.Score;
            MainStatistics.TotalTime += (int)LevelController.Instance.LevelTime;
        }*/

    }
}