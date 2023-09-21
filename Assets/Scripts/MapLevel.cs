using SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode episode;
        [SerializeField] private bool isLeftSided;
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(episode);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (isLeftSided)
            {
                
            }
            
        }
#endif
    }
}