using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class PlayerStatistics : SingletonBase<PlayerStatistics>
    {
        [HideInInspector] public int NumKills;
        [HideInInspector] public int Scores;
        [HideInInspector] public int Time;

        public void Reset()
        {
            NumKills = 0;
            Scores = 0;
            Time = 0;
        }
    }
}