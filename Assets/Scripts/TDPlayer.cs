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
        private static event Action<int> OnLifeUpdate;
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

        public void ReduceLife(int lives)
        {
            ApplyDamage(lives);
            OnLifeUpdate(NumLives);
        }

        // TODO: �����, ��� ������ �� ��������� ����������
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            /*if (m_NumGold >= m_TowerAsset.goldCost)
            {
            }*/
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(towerPrefab, buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.towerSprite;
            Destroy(buildSite.gameObject); // �� ��������

        }
    }
}