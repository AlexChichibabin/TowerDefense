using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private Ship m_Ship;
        #endregion

        #region UnityEvents
        private void Start()
        {
            m_Ship = transform.root.GetComponent<Ship>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
        }
        #endregion

        #region PublicAPI
        public void Fire()
        {
            if (m_TurretProperties == null) return;
            if (m_RefireTimer > 0) return;

            if (m_Ship != null && m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
            if (m_Ship != null && m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = m_FireSource.transform.position;
            projectile.transform.up = m_FireSource.transform.up;

            projectile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            if(m_Mode == TurretMode.Primary)
            {
                Instantiate(m_ImpactSoundPrimary, projectile.transform.position, Quaternion.identity);
            }
            if (m_Mode == TurretMode.Secondary)
            {
                Instantiate(m_ImpactSoundSecondary, projectile.transform.position, Quaternion.identity);
            }
        }

        public void AssignLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = props;
        }

        #endregion
    }
}
