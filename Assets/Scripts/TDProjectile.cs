using SpaceShip;
using UnityEngine;

namespace TowerDefense
{
    public class TDProjectile : Projectile
    {
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

        [SerializeField] private DamageType m_DamageType;

        protected override void OnHit(RaycastHit2D hit)
        {
            if (hit)
            {
                Enemy enemy = hit.collider.transform.root.GetComponent<Enemy>();

                if (enemy != null)
                {
                    if (m_ImpactAreaAttack == null)
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
            if (m_ImpactAreaAttack != null)
            {
                ImpactAreaAttack expl = Instantiate(m_ImpactAreaAttack, pos, Quaternion.identity);
                expl.SetProjectileProperties(m_Damage, m_DamageType);
                //expl.SetParentShooter(m_Parent);
            }

            base.OnProjectileLifeEnd(col, pos);

        }
    }
}