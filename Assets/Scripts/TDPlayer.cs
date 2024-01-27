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
        
        private event Action<int> OnGoldUpdate;
        public void GoldUpdateSubscribe(Action<int> act)
        {
            Instance.OnGoldUpdate += act;
            act(Instance.m_NumGold);
        }
        /*public void GoldUpdateUnSubscribe(Action<int> act) ///
        {
            Instance.OnGoldUpdate -= act;
        }*/
        public event Action<int> OnLifeUpdate;
        public void LifeUpdateSubscribe(Action<int> act)
        {
            Instance.OnLifeUpdate += act;
            act(Instance.NumLives);
        }


        [SerializeField] private int m_NumGold;
        [SerializeField] private Tower towerPrefab;

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

        [SerializeField] private UpgradeAsset healthUpgrade;
        private new void Awake()
        {
            base.Awake();
            var HealthLevel = Upgrades.GetUpgradeLevel(healthUpgrade);
            ApplyDamage(-HealthLevel);
        }

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
            Destroy(buildSite.gameObject);
        }
    }
}