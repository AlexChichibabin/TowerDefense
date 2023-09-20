using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShip;

namespace TowerDefense
{
    public class TimeLevelCondition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float timeLimit = 4f;
        private float currentLevelTime = 0;
        public bool IsCompleted => Time.time >= timeLimit;

        private void Start()
        {
            timeLimit += Time.time;
        }

    }
}