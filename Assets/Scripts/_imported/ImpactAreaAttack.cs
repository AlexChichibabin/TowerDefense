using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense;
using static TowerDefense.TDProjectile;

namespace SpaceShip
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ImpactAreaAttack : ImpactEffect
    {
        private float m_Radius;
        [SerializeField] private TDProjectile m_Projectile;
        private SpriteRenderer m_LargeCircle;
        private int m_Damage;
        private DamageType m_DamageType;


        private void Start()
        {
            m_LargeCircle = FindFirstObjectByType<SpriteRenderer>();
            m_Radius = m_LargeCircle.transform.localScale.x/2;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, m_Radius);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i])
                {
                    hit[i].transform.root.TryGetComponent<Enemy>(out var enemy);

                    if (enemy != null)
                    {
                        enemy.TakeDamage(m_Damage, m_DamageType);
                    }
                }
            }
        }

        public void SetProjectileProperties(int damage, DamageType damageType)
        {
            m_Damage = damage;
            m_DamageType = damageType;
        }


        private Destructible m_Parent;
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
        #endif
    }
}
