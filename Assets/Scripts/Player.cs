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
        [SerializeField] private Ship m_Ship;
        //[SerializeField] private GameObject m_PlayerShipPrefab;
        [HideInInspector] public Ship ActiveShip => m_Ship;

        //public static event Action<int> OnLifeUpdate;

        //[SerializeField] private CameraController m_CameraController;
        //[SerializeField] private MovementController m_MovementController;

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }

        private void Start()
        {
            /*if (m_Ship != null)
            {
                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            }*/


            //Respawn();
        }
        
        private void OnShipDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
            {
                //Respawn();
            }
            else LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        protected void ApplyDamage(int damage)
        {
            m_NumLives -= damage;
            
            if (m_NumLives <= 0)
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }


        /*
        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<Ship>();
                //m_CameraController.SetTarget(m_Ship.transform);
                //m_MovementController.SetTargetShip(m_Ship);
                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            }
        }*/


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
