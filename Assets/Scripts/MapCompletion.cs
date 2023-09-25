using SpaceShip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string filename = "completion.dat";

        /*public static void ResetSavedData()
        {
            Saver<EpisodeScore[]>.Reset(filename);
        }*/

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
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

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
        }
        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < completionData.Length)
            {
                episode = completionData[id].episode;
                score = completionData[id].score;
                return true;
            }
            episode = null;
            score = 0;
            return false;
        }
    }
}