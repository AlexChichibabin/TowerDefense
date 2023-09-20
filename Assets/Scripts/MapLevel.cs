using SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode episode;
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(episode);
        }
    }
}