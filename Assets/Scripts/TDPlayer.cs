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
        [SerializeField] private int m_ManaCountPerTime;
        private float m_ManaTimer;

        private event Action<int> OnGoldUpdate;
        private event Action<int> OnManaUpdate;
        public event Action<int> OnLifeUpdate;

        private void Start()
        {
            if (m_HealthUpgrade && Upgrades.GetUpgradeLevel(m_HealthUpgrade) >= 1) 
                ReduceLife(-Upgrades.GetUpgradeLevel(m_HealthUpgrade)); // Если уровень апгрейда жизней > 0, то прибавляет жизни

            if (m_ManaUpgrade && Upgrades.GetUpgradeLevel(m_ManaUpgrade) >= 1) ChangeManaCountPerTime(); // Если уровень апгрейда маны > 0, то прибавляет ману за время
            //else SceneManager.LoadScene(0); // Временная дебаговая функция. Если не находит m_ManaUpgrade, то выходит в меню

            m_ManaTimer = m_ManaTime + Time.time; // Стартовый отсчет таймера прибавления маны
        }

        private void Update()
        {
            if (Time.time >= m_ManaTimer) 
            {
                m_ManaTimer = m_ManaTime + Time.time;
                ChangeMana(m_ManaCountPerTime);
            }
        }
        private void ChangeManaCountPerTime() // Прибавляет апгрейд маны к текущему значению маны за время
        {
            m_ManaCountPerTime += (Upgrades.GetUpgradeLevel(m_ManaUpgrade) * m_ManaUpgrade.IncreaseValue);
        }

        public static new TDPlayer Instance { get 
            { 
                return Player.Instance as TDPlayer; 
            } 
        }
        
        public void GoldUpdateSubscribe(Action<int> act) // Подписывает на изменение золота и сразу передает
        {
            Instance.OnGoldUpdate += act;
            act?.Invoke(Instance.m_NumGold);
        }

        public void ManaUpdateSubscribe(Action<int> act) // Подписывает на изменение маны и сразу передает
        {
            Instance.OnManaUpdate += act;
            act?.Invoke(Instance.m_NumMana);
        }

        public void LifeUpdateSubscribe(Action<int> act) // Подписывает на изменение изней и сразу передает
        {
            Instance.OnLifeUpdate += act;
            act?.Invoke(Instance.NumLives);
        }
  
        public void ChangeGold(int gold) // Прибавляет золото и выполняет OnGoldUpdate.Invoke()
        {
            m_NumGold += gold;
            OnGoldUpdate?.Invoke(m_NumGold);
        }

        public void ChangeMana(int mana) // Прибавляет ману и выполняет OnManaUpdate.Invoke()
        {
            m_NumMana += mana;
            OnManaUpdate?.Invoke(m_NumMana);
        }

        public void ReduceLife(int damage)  // Прибавляет/отнимает жизни и выполняет OnLifeUpdate.Invoke()
        {
            ApplyDamage(damage);
            OnLifeUpdate?.Invoke(NumLives);
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