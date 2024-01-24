using System;
using UnityEngine;
using UnityEditor;
using Unity.Burst.CompilerServices;


namespace SpaceShip
{
    public class Projectile : Entity
    {
        public void SetFromOtherProjectile(Projectile other)
        {
            other.SetData(out m_Velocity, out m_LifeTime, out m_Damage, out m_Nickname, out m_ImpactExplosionPrefab, out IsSelfDirected, out m_DirSensity, out m_Area);
        }

        private void SetData(out float m_Velocity, out float m_LifeTime, out int m_Damage, out string m_Nickname, out ImpactExplosion m_ImpactExplosionPrefab, out bool IsSelfDirected, out float m_DirSensity, out CircleArea m_Area)
        {
            m_Velocity = this.m_Velocity;
            m_LifeTime = this.m_LifeTime;
            m_Damage = this.m_Damage;
            m_Nickname = this.m_Nickname;
            m_ImpactExplosionPrefab = this.m_ImpactExplosionPrefab;
            IsSelfDirected = this.IsSelfDirected;
            m_DirSensity = this.m_DirSensity;
            m_Area = this.m_Area;
        }

        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;
        [SerializeField] private float m_LifeTime;
        [SerializeField] protected int m_Damage;
        public int Damage => m_Damage;

        [Header("RocketAndSelfdirection")]
        [SerializeField] protected ImpactExplosion m_ImpactExplosionPrefab;
        [SerializeField] protected bool IsSelfDirected;
        [SerializeField] protected float m_DirSensity;

        [SerializeField] protected CircleArea m_Area;
        //[SerializeField] private bool isPlayer;

        private Destructible m_Target; // Для самонаводящихся снарядов



        private float m_Timer;

        private void Update()
        {
            float stepLenght = m_Velocity * Time.deltaTime;
            Vector2 step = transform.up * stepLenght;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            OnHit(hit);

            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime) Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);

            ControllRocket();
        }

        protected virtual void OnHit(RaycastHit2D hit)
        {
            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null && dest != m_Parent)
            {
                if (m_ImpactExplosionPrefab == null)
                {
                    dest.ApplyDamage(m_Damage);
                }
            }
            if (!hit.collider.transform.root.GetComponent<ImpactExplosion>())
            {
                OnProjectileLifeEnd(hit.collider, hit.point);
            }
        }

        public void MultiplyVelocity(float coefficient) // Using by Upgrade
        {
            m_Velocity *= coefficient;
        }

        protected void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            print("yes");
            if (m_ImpactExplosionPrefab != null)
            {
                print(m_ImpactExplosionPrefab);
                ImpactExplosion expl = Instantiate(m_ImpactExplosionPrefab, pos, Quaternion.identity);
                //expl.SetParentShooter(m_Parent);
            }
            print(gameObject);

            Destroy(gameObject);
        }

        private Destructible m_Parent; // For explosion function

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        private void ControllRocket()
        {
            if (IsSelfDirected == true)
            {

                if (m_Target != null)
                {
                    CorrectDirection();
                }
            }
        }

        private void GetTarget() // Выбор цели в радиусе вокруг снаряда
        {
            Collider2D targetHit = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), m_Area.Radius);
            print(targetHit);
            targetHit.transform.root.TryGetComponent(out Destructible dest);

            if (dest != null && targetHit != null && dest != m_Parent)
            {
                m_Target = dest;
            }
            if (m_Target == null) return;
        }
        private void CorrectDirection() // Смена курса по направлению к цели
        {
            Vector3 dir = (m_Target.transform.position - transform.position).normalized;
            transform.up = Vector3.Slerp(transform.up, dir, Time.deltaTime * m_DirSensity);
        }

        public void SetTarget(Destructible target) // Для самонаводящихся снарядов
        {
            m_Target = target;
        }
    }
}
namespace TowerDefense
{
    [CustomEditor(typeof(SpaceShip.Projectile))]
    public class ProjectileInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create TDProjectile")) // Creates TDProjectile component with the same settings as Projectile and destroys last one
            {
                var target = this.target as SpaceShip.Projectile;
                var tdProj = target.gameObject.AddComponent<TDProjectile>();
                tdProj.SetFromOtherProjectile(target);
                DestroyImmediate(target, true);
            }
        }
    }
}