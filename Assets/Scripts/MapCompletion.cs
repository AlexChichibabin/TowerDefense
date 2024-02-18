using SpaceShip;
using System;
using UnityEngine;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string m_FileName = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        [SerializeField] private EpisodeScore[] m_CompletionData;
        public int TotalScores { private set; get; }
        //[SerializeField] private EpisodeScore[] branchCompletionData;

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(m_FileName, ref m_CompletionData);
            Instance.TotalScores = 0;
            foreach (var score in m_CompletionData)
            {
                Instance.TotalScores += score.score;
            }
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            { 
                foreach (var item in Instance.m_CompletionData)
                {   // Сохранение новых очков прохожения
                    if (item.episode == LevelSequenceController.Instance.CurrentEpisode)
                    {
                        if (item.score < levelScore)
                        {
                            Instance.TotalScores += levelScore - item.score;
                            item.score = levelScore;
                            Saver<EpisodeScore[]>.Save(m_FileName, Instance.m_CompletionData);
                        }
                    }
                }
                print($"Episode complete with score {levelScore}");
            }
            else
                print($"Episode complete with score {levelScore}");
        }
        public int GetEpisodeScore(Episode m_Episode)
        {
            foreach (var data in m_CompletionData)
            {
                if (data.episode == m_Episode)
                {
                    return data.score;
                }
            }
            return 0;
        }
    }
}