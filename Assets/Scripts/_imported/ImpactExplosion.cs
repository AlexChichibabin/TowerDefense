using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense;

namespace SpaceShip
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ImpactExplosion : ImpactEffect
    {
        [SerializeField] private float m_Radius;
        [SerializeField] private Projectile m_Projectile;
        private int m_Damage;


        private void Start()
        {
            m_Damage = m_Projectile.Damage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, m_Radius);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i])
                {
                    Destructible dest = hit[i].transform.root.GetComponent<Destructible>();
                    //print(dest.name);

                    if (dest != null /*&& dest != m_Parent*/)
                    {
                        //if (m_Parent.CurrentHitPoints > 0 && m_Parent != null)
                        {
                            //print(m_Parent.CurrentHitPoints);
                            //print(m_Parent);
                            dest.ApplyDamage(m_Damage);

                            /*if (m_Parent == Player.Instance.ActiveShip)
                            {
                                Player.Instance.AddScore(dest.ScoreValue * 2); // (2) More scores for explosion, than for common projectile
                                if (dest.CurrentHitPoints <= 0 && dest.TeamId != 0 && dest != Player.Instance.ActiveShip) Player.Instance.AddKill();
                            }*/
                        }
                    }
                }
            }
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
