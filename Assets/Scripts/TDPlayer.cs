using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShip;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private int m_NumGold;
        [SerializeField] private Tower m_TowerPrefab;
        [SerializeField] private UpgradeAsset m_HealthUpgrade;
        public UpgradeAsset HealthUpgrade => m_HealthUpgrade;
        [SerializeField] private UpgradeAsset m_ManaUpgrade;
        [SerializeField] private float m_ManaTime;
        private float manaTimer;
        [SerializeField] private int m_ManaCountPerTime;

        private event Action<int> OnGoldUpdate;
        private event Action<int> OnManaUpdate;
        public event Action<int> OnLifeUpdate;

        private void Start()
        {
            var HealthLevel = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            if (Upgrades.GetUpgradeLevel(m_HealthUpgrade) >= 1) ReduceLife(-HealthLevel);
            ChangeManaCountPerTime(m_ManaUpgrade.m_IncreaseValue);

            manaTimer = m_ManaTime + Time.time;

            /*print("Mana per timeUnit is: " + m_ManaCountPerTime);
            print("Lives amount: " + NumLives);*/
        }

        private void Update()
        {
            if (Time.time >= manaTimer)
            {
                manaTimer = m_ManaTime + Time.time;
                ChangeMana(m_ManaCountPerTime);
            }
        }

        public static new TDPlayer Instance { get 
            { 
                return Player.Instance as TDPlayer; 
            } 
        }
        
        public void GoldUpdateSubscribe(Action<int> act)
        {
            Instance.OnGoldUpdate += act;
            act(Instance.m_NumGold);
        }

        public void ManaUpdateSubscribe(Action<int> act)
        {
            Instance.OnManaUpdate += act;
            act(Instance.m_NumMana);
        }

        public void LifeUpdateSubscribe(Action<int> act)
        {
            Instance.OnLifeUpdate += act;
            act(Instance.NumLives);
        }
  
        public void ChangeGold(int gold)
        {
            m_NumGold += gold;
            OnGoldUpdate(m_NumGold);
        }

        public void ChangeMana(int mana)
        {
            m_NumMana += mana;
            OnManaUpdate(m_NumMana);
        }

        public void ReduceLife(int damage)
        {
            ApplyDamage(damage);
            OnLifeUpdate(NumLives);
        }

        private void ChangeManaCountPerTime(int increaseCount)
        {
            if (m_ManaUpgrade) m_ManaCountPerTime += Upgrades.GetUpgradeLevel(m_ManaUpgrade) * increaseCount;
        }

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
            Destroy(buildSite.gameObject);
        }
    }
}