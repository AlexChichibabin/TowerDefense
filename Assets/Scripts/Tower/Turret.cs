using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense;

namespace SpaceShip
{
    public class Turret : MonoBehaviour
    {
        #region Parameters
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private float m_FireDelay;

        [SerializeField] private TurretProperties m_TurretProperties;

        [SerializeField] private Transform m_FireSource;

        [SerializeField] private GameObject m_ImpactSoundPrimary;

        [SerializeField] private GameObject m_ImpactSoundSecondary;

        private Destructible m_Target;
        public Destructible Target => m_Target;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private Ship m_Ship;
        private Tower m_Tower;

        private bool accelerationIsActive = false;

        #endregion

        #region UnityEvents


        private void Start()
        {
            m_Ship = transform.root.GetComponent<Ship>();
            m_Tower = transform.root.GetComponent<Tower>();
            if (m_TurretProperties == ArcherProps)
            {
                //print("m_TurretProperties is " + m_TurretProperties);
                if (m_Tower.ArrowAccelerationLevel >= 1)
                {
                    //print("ArrowAccelerationLevel OnStart is " + m_Tower.ArrowAccelerationLevel);
                    accelerationIsActive = true;
                }
            }
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
            else if (m_Mode == TurretMode.Auto)
            {
                Fire();
            }
        }
        #endregion

        #region PublicAPI
        public void Fire()
        {
            if (m_TurretProperties == null) return;
            if (m_RefireTimer > 0) return;

            if (m_Ship != null)
            {
                if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
                if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            }
            

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = m_FireSource.transform.position;
            projectile.transform.up = m_FireSource.transform.up;
            projectile.SetTarget(m_Target);
            //print("Velocity before is " + projectile.Velocity);
            AccelerateArrows(projectile);
            //print("Velocity after is " + projectile.Velocity);

            if (m_Ship != null)
            {
                projectile.SetParentShooter(m_Ship);
            }

            m_RefireTimer = m_TurretProperties.RateOfFire;

            if(m_Mode == TurretMode.Primary && m_ImpactSoundPrimary != null)
            {
                Instantiate(m_ImpactSoundPrimary, projectile.transform.position, Quaternion.identity);
            }
            if (m_Mode == TurretMode.Secondary && m_ImpactSoundSecondary != null)
            {
                Instantiate(m_ImpactSoundSecondary, projectile.transform.position, Quaternion.identity);
            }

        }

        [SerializeField] private TurretProperties ArcherProps;
        private void AccelerateArrows(Projectile projectile)
        {
            //print("acceleration is " + accelerationIsActive);
            if (accelerationIsActive == true)
            {
                projectile.MultiplyVelocity(1.0f + (float)m_Tower.ArrowAccelerationLevel / 10.0f);
            }
        }

        public void SetTurretProperties(TurretProperties tp)
        {
            m_TurretProperties = tp;
        }

        public void AssignLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = props;
        }

        public void SetTarget(Destructible target) // Для самонаводящихся снарядов
        {
            m_Target = target;
        }
        #endregion
    }
}
