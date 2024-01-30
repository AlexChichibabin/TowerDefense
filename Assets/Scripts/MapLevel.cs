using SpaceShip;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode;
        private int m_StarsAmount;
        [SerializeField] private LevelVisualScores m_VisualScores;

        public bool IsComplete { get { return gameObject.activeSelf && m_StarsAmount > 0; } }

        private void Awake()
        {
            m_VisualScores = GetComponentInChildren<LevelVisualScores>();
        }

        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        public void Initialize()
        {
            m_StarsAmount = MapCompletion.Instance.GetEpisodeScore(m_Episode);
            m_VisualScores.SetStars(m_StarsAmount);
        }

        public int GetStarsAmount()
        {
            return m_StarsAmount;
        }
    }
}