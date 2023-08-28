using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShip;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private int m_NumGold;
        public void ChangeGold(int gold)
        {
            m_NumGold += gold;
            print(m_NumGold);
        }
    }
}