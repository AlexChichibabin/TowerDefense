using SpaceShip;
using UnityEngine;

namespace TowerDefense
{
    public class TDProjectile : Projectile
    {
        [SerializeField] private Sound m_ShotSound = Sound.Arrow;
        [SerializeField] private Sound m_HitSound = Sound.ArrowHit;

        public enum DamageType
        {
            Base, // main physic damage
            Magic, // main damage to magic armor, ignore phisic armor
            Flammable, // inflammation enemies, periodic damage
            Explosive, // momentaly damage to area
            Freezing, // low damage and slow down enemies
            Acidic, // periodic damage to physic armor
            Toxic, // periodic damage with ignore armor
            Holy // increase damage to dark type of armor
        }
        private void Start()
        {
            m_ShotSound.Play();
        }

        /// <summary>
        /// Type of damage
        /// </summary>
        [SerializeField] private DamageType m_DamageType;

        /// <summary>
        /// Получение от цели попадания объекта врага. 
        /// Вызов на нем нанесения урона или вызов взрыва при наличии префаба последнего
        /// </summary>
        /// <param target="hit"></param>
        protected override void OnHit(RaycastHit2D hit)
        {
            if (hit)
            {
                Enemy enemy = hit.collider.transform.root.GetComponent<Enemy>();
                m_HitSound.Play();

                if (enemy != null)
                {
                    if (m_ImpactAreaAttackPrefab == null)
                    {
                        enemy.TakeDamage(m_Damage, m_DamageType);
                    }
                }
                if (!hit.collider.transform.root.GetComponent<ImpactAreaAttack>())
                {
                    OnProjectileLifeEnd(hit.collider, hit.point);
                }
            }
        }

        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            if (m_ImpactAreaAttackPrefab != null)
            {
                ImpactAreaAttack expl = Instantiate(m_ImpactAreaAttackPrefab, pos, Quaternion.identity);
                expl.SetProjectileProperties(m_Damage, m_DamageType);
            }

            base.OnProjectileLifeEnd(col, pos);
        }
    }
}