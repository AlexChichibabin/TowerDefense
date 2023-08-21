using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace SpaceShip
{
    /// <summary>
    /// some destructible object that can own hitpoints
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// object ignores damages
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// start hitpoints amount
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// current hitpoints amount
        /// </summary>
        private int m_CurrentHitPoints;
        public int CurrentHitPoints => m_CurrentHitPoints;

        /// <summary>
        ///  last position value before death
        /// </summary>
        private Vector3 m_LastPosition;
        public Vector3 LastPosition => m_LastPosition;

        [SerializeField] private int m_HitPointRegenPerSecond;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }

        private void Update()
        {
            UpdateHealthRegen();
        }

            #endregion

            #region Public API

            /// <summary>
            /// apply damage to object
            /// </summary>
            /// <param name="damage"></param>
            public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        private float additionalHP = 0.0f;
        private void UpdateHealthRegen()
        {
            additionalHP += (float)m_HitPointRegenPerSecond * Time.deltaTime;
            if (additionalHP > 1)
            {
                m_CurrentHitPoints = Mathf.Clamp((m_CurrentHitPoints + (int)additionalHP), 0, m_HitPoints);
                additionalHP -= (int)additionalHP;
            }
          
        }

        private bool m_IndestructibleTimed = false;
        public bool IndestructibleIsTimed => m_IndestructibleTimed;
        public void SetIndestructibility(bool indestructible)
        {
            m_Indestructible = indestructible;
            m_IndestructibleTimed = indestructible;
        }

        #endregion

        /// <summary>
        /// virtual event of object destroy, when current hitpoints becomes below or equal zero
        /// </summary>
        protected virtual void OnDeath()
        {
            m_LastPosition = gameObject.transform.position;

            Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;


        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            if(this != null && m_AllDestructibles != null) m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        #region Score
        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;
        #endregion
    }
}
