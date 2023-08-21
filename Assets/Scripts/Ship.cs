using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.EventSystems.EventTrigger;

namespace SpaceShip
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ship : Destructible
    {
        /// <summary>
        /// mass for automatic rigid set
        /// </summary>
        [Header("Space Ship")]
        [SerializeField] private float m_Mass;
        /// <summary>
        /// force power
        /// </summary>
        [SerializeField] private float m_Thrust;
        /// <summary>
        /// rotate power
        /// </summary>
        [SerializeField] private float m_Mobility;
        /// <summary>
        /// maximum linear speed
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;
        /// <summary>
        /// maximum rotate speed
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// saved link for rigid
        /// </summary>
        private Rigidbody2D m_Rigid;
        [SerializeField] private GameObject m_PrefabExplosion;

        //[SerializeField] private float m_Inertia;

        

        #region Unity Events
        protected override void Start()
        {
            base.Start();
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 0.1f;

            //InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();

            /*UpdateEnergyRegen();

            CheckIndestructibility();

            CheckAcceleration();*/
        }


        #endregion

        /// <summary>
        /// Method, that get moveforce to ship
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce( ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force );

            m_Rigid.AddForce( -m_Rigid.velocity * ( m_Thrust / m_MaxLinearVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force );

            m_Rigid.AddTorque( TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force );

            m_Rigid.AddTorque( -m_Rigid.angularVelocity * ( m_Mobility / m_MaxAngularVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force );
        }

        #region Public API
        /// <summary>
        /// thrust control. from -1.0 to +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// torque control. from -1.0 to +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        /// <summary>
        /// TODO: Расписать метод-заглушку
        /// Используется турелями.
        /// </summary>
        /// <param name="count"></param>
        /// <returns> Если отнимает патроны, то возвращает true. Иначе false. </returns>
        public bool DrawEnergy(int count)
        {
            return true;
        }
        /// <summary>
        /// TODO: Расписать метод-заглушку
        /// Используется турелями.
        /// </summary>
        /// <param name="count"></param>
        /// <returns> Если отнимает патроны, то возвращает true. Иначе false. </returns>
        public bool DrawAmmo(int count)
        {
            return true;
        }

        /// <summary>
        /// TODO: Расписать метод-заглушку стрельбы
        /// Использует AIController
        /// </summary>
        /// <param name="mode"></param>
        public void Fire(TurretMode mode)
        {
            return;
        }


        //Offensive methods
        /* public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i] != null && m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }
        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }
        public void AddThrust(int thrust)
        {
            m_Thrust += thrust;
            m_AccelerationValue = thrust;
            m_Accelerated = true;
        }
        public bool DrawEnergy(int count)
        {
            if (count == 0)
            {
                return true;
            }
            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }
            return false;
        }
        public bool DrawAmmo(int count)
        {
            if (count == 0)
            {
                return true;
            }
            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }
        public void DrawThrust(int thrust)
        {
            m_Thrust -= thrust;
            m_Accelerated = false;
        }
        public void SetIndestructibilityTimer(float timer)
        {
            m_IndestructibilityTimer = timer;
        }
        public void SetAccelerationTimer(float timer)
        {
            m_AccelerationTimer = timer;
        }*/


        #endregion

        protected override void OnDeath()
        {
            base.OnDeath();
            Instantiate(m_PrefabExplosion, LastPosition, Quaternion.identity);
        }
        //Offensive
        /*
        #region EnergyAmmoWeapons
        [SerializeField] private Turret[] m_Turrets;
        [SerializeField] private int m_MaxEnergy;
        public int MaxEnergy => m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        public int MaxAmmo => m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        public float PrimaryEnergy => m_PrimaryEnergy;


        private int m_SecondaryAmmo;
        public int SecondaryAmmo => m_SecondaryAmmo;

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }
        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime, 0, m_MaxEnergy);
        }

        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }
        #endregion

        #region Indestructibility
        private float m_IndestructibilityTimer;
        
        private void CheckIndestructibility()
        {
            if (m_IndestructibilityTimer > 0)
            {
                m_IndestructibilityTimer -= Time.deltaTime;
            }
            if (m_IndestructibilityTimer <= 0)
            {
                if (IsIndestructible == true && IndestructibleIsTimed == true)
                {
                    SetIndestructibility(false);
                }
            }
        }
        #endregion

        #region Acceleration
        private float m_AccelerationTimer;
        private int m_AccelerationValue;
        private bool m_Accelerated;
        private void CheckAcceleration()
        {
            if (m_AccelerationTimer > 0)
            {
                m_AccelerationTimer -= Time.deltaTime;
            }
            if (m_Accelerated == true && m_AccelerationTimer <= 0)
            {
                DrawThrust(m_AccelerationValue);
            }
        }
        #endregion*/
    }
}

