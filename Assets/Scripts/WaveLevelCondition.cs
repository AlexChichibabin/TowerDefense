using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShip;

namespace TowerDefense
{
    public class WaveLevelCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;

        private void Start()
        {
            //if(Time.time > 0.001f)
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                isCompleted = true;
            };
        }
        public bool IsCompleted {get {return isCompleted; } }
    }
}