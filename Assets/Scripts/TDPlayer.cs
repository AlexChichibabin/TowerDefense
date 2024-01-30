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

        private event Action<int> OnGoldUpdate;
        public event Action<int> OnLifeUpdate;

        private void Start()
        {
            var HealthLevel = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            ApplyDamage(-HealthLevel);
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

        public void ReduceLife(int damage)
        {
            ApplyDamage(damage);
            OnLifeUpdate(NumLives);
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