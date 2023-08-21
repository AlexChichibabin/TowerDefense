using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShip
{
    public class TotalStatistics : SingletonBase<TotalStatistics>
    {
        [HideInInspector] public int TotalNumKills;
        [HideInInspector] public int TotalScores;
        [HideInInspector] public int TotalTime;

        [HideInInspector] public UnityEvent UpdateTotalStatistics;

        private void Start()
        {
            LoadResults();
            
        }

        public void OnButtonReset()
        {
            TotalNumKills = 0;
            TotalScores = 0;
            TotalTime = 0;
            SaveMainStatistic();
            UpdateTotalStatistics.Invoke();
        }

        private void LoadResults()
        {
            TotalNumKills = PlayerPrefs.GetInt("TotalNumKills: ");
            TotalScores = PlayerPrefs.GetInt("TotalScores: ");
            TotalTime = PlayerPrefs.GetInt("TotalTime: ");
        }

        public void SaveMainStatistic()
        {
            PlayerPrefs.SetInt("TotalNumKills: ", TotalNumKills);
            PlayerPrefs.SetInt("TotalScores: ", TotalScores);
            PlayerPrefs.SetInt("TotalTime: ", TotalTime);
        }
    }
}