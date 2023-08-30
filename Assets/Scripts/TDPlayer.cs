using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShip;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance { get 
            { 
                return Player.Instance as TDPlayer; 
            } 
        }
        
        public static event Action<int> OnGoldUpdate;
        public static event Action<int> OnLifeUpdate;

        [SerializeField] private int m_NumGold;

        private void Start()
        {
            OnGoldUpdate(m_NumGold);
            OnLifeUpdate(NumLives);
        }
        public void ChangeGold(int gold)
        {
            m_NumGold += gold;
            OnGoldUpdate(m_NumGold);

        }

        public void ReduceLife(int lives)
        {
            ApplyDamage(lives);
            OnLifeUpdate(NumLives);

        }
    }
}