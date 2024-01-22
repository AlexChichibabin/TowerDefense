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
        
        private static event Action<int> OnGoldUpdate;
        public static void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_NumGold);
        }
        public static void GoldUpdateUnSubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }
        public static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
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
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            ApplyDamage(-level);
        }

        // TODO: Верим, что золота на постройку достаточно
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            /*if (m_NumGold >= m_TowerAsset.goldCost)
            {
            }*/
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(towerPrefab, buildSite.position, Quaternion.identity);
            SpriteRenderer sr = tower.GetComponentInChildren<SpriteRenderer>();
            sr.sprite = towerAsset.towerSprite;
            sr.color = towerAsset.spriteColor;
            Turret tur = tower.GetComponentInChildren<Turret>();
            tur.SetTurretProperties(towerAsset.turretProperties);
            Destroy(buildSite.gameObject); // Не работает

        }
    }
}