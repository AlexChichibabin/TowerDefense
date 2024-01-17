using SpaceShip;
using System;
using UnityEngine;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string filename = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            else
                print($"Episode complete with score {levelScore}");
        }
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in completionData)
            {
                if (item.episode == currentEpisode)
                {
                    if (item.score < levelScore)
                    {
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, completionData);
                    }
                }
            }
        }

        [SerializeField] private EpisodeScore[] completionData;
        [SerializeField] private int totalScores;
        public int TotalScores { get { return totalScores; } }

        //[SerializeField] private EpisodeScore[] branchCompletionData;

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);

            foreach (var score in completionData)
            {
                Instance.totalScores += score.score;
            }
            print("total scores (awake) are:" + Instance.totalScores);
        }


        public int GetEpisodeScore(Episode m_Episode)
        {
            foreach (var data in completionData)
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