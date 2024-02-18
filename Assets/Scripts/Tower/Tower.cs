using SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SpaceShip;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private CircleArea m_Area;
        private Turret[] m_Turrets;
        private Destructible m_Target;
        private Rigidbody2D m_TargetRB;

        [SerializeField] private UpgradeAsset AccuracyUpgrade;
        [SerializeField] private UpgradeAsset ArrowAccelerationUpgrade;
        private int arrowAccelerationLevel = 0;
        [SerializeField] private float m_Lead; // Коэффициент стрельбы на упреждение

        public int ArrowAccelerationLevel => arrowAccelerationLevel;

        private void Awake()
        {
            var AccuracyLevel = Upgrades.GetUpgradeLevel(AccuracyUpgrade);
            if (AccuracyLevel >= 1)
            {
                m_Area.SetRadius(m_Area.Radius*(1.0f + (float)AccuracyLevel / 10.0f));
            }

            arrowAccelerationLevel = Upgrades.GetUpgradeLevel(ArrowAccelerationUpgrade);
        }
        private void Start()
        {
            
        }

        private void Update()
        {
            if (m_Target != null)
            {
                if (Vector3.Distance(m_Target.transform.position, transform.position) <= m_Area.Radius)
                {
                    foreach (Turret t in m_Turrets)
                    {
                        t.transform.up = m_Target.transform.position - t.transform.position + (Vector3)m_TargetRB.velocity * m_Lead;
                        t.Fire();
                    }
                }
                else
                {
                    m_Target = null;
                    foreach (Turret t in m_Turrets)
                    {
                        t.SetTarget(m_Target);
                    }
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Area.Radius);
                if (enter)
                {
                    m_Target = enter.transform.root.GetComponent<Destructible>();
                    foreach (Turret t in m_Turrets)
                    {
                        m_TargetRB = m_Target.GetComponent<Rigidbody2D>();
                        t.SetTarget(m_Target);
                    }
                }
            }
        }
        public void Use(TowerAsset asset)
        {
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            sr.sprite = asset.towerSprite;
            sr.color = asset.spriteColor;
            m_Turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in m_Turrets)
            {
                turret.SetTurretProperties(asset.turretProperties);
            }
            GetComponentInChildren<BuildSite>().SetBuildableTowers(asset.m_UpgradesTo);
        }
    }
}