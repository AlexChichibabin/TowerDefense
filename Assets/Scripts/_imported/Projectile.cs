using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_LifeTime;
        [SerializeField] private int m_Damage;
        public int Damage => m_Damage;
        [SerializeField] private ImpactExplosion m_ImpactExplosionPrefab;
        [SerializeField] private bool IsSelfDirected;
        [SerializeField] private float m_DirSensity;
        private GameObject m_RocketTarget;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private bool isPlayer;



        private float m_Timer;

        private void Update()
        {
            float stepLenght = m_Velocity * Time.deltaTime;
            Vector2 step = transform.up * stepLenght;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent)
                {
                    if (m_ImpactExplosionPrefab == null)
                    {
                        //if (m_Parent == null) return;

                            dest.ApplyDamage(m_Damage);

                            /*if (m_Parent == Player.Instance.ActiveShip) // Если родитель - игрок
                            {
                                Player.Instance.AddScore(dest.ScoreValue); // Добавить очки за попадание
                                if (dest.CurrentHitPoints <= 0 && dest.TeamId != 0 && dest != Player.Instance.ActiveShip) Player.Instance.AddKill(); // Добавить убийства
                            }*/
                        
                    }
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime) Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);

            ControllRocket();
        }

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            if (m_ImpactExplosionPrefab != null)
            {
                ImpactExplosion expl = Instantiate(m_ImpactExplosionPrefab, pos, Quaternion.identity);
                expl.SetParentShooter(m_Parent);
            }

            Destroy(gameObject);
        }

        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        private void ControllRocket()
        {
            if (IsSelfDirected == true)
            {
                if (m_RocketTarget == null)
                {
                    GetTarget();
                }
                if (m_RocketTarget != null)
                {
                    CorrectDirection();
                }
            }
        }

        private void GetTarget()
        {
                Collider2D targetHit = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), m_Area.Radius);
                if (targetHit != null && targetHit.transform.root.GetComponent<Destructible>() != m_Parent)
                {
                    m_RocketTarget = targetHit.transform.gameObject;
                }
                if (m_RocketTarget == null) return;
        }
        private void CorrectDirection()
        {
                Vector3 dir = (m_RocketTarget.transform.position - transform.position).normalized;
                transform.up = Vector3.Slerp(transform.up, dir, Time.deltaTime * m_DirSensity);
        }

    }
}