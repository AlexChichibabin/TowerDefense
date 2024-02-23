using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        public int NumLives { get { return m_NumLives; } }

        [SerializeField] protected int m_NumMana;
        public int NumMana { get { return m_NumMana; } }
        [SerializeField] private Ship m_Ship;
        [HideInInspector] public Ship ActiveShip => m_Ship;

        public event Action OnPlayerDead;

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }

        protected void ApplyDamage(int damage)
        {
            m_NumLives -= damage;
            
            if (m_NumLives <= 0)
            {
                m_NumLives = 0;
                OnPlayerDead?.Invoke();
            }
        }

        #region Score
        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }
        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion

    }
}
