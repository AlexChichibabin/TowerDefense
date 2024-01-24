using UnityEngine;
using SpaceShip;
using UnityEditor;
using UnityEditor.Events;
using System;

namespace TowerDefense
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            (int power, TDProjectile.DamageType type, int armor) =>
            {// ArmorType Base
                switch(type) 
                {
                    case TDProjectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power-armor, 1);
                }
            },
            (int power, TDProjectile.DamageType type, int armor) =>
            {// ArmorType Magic
                switch(type)
                {
                    case TDProjectile.DamageType.Magic: return Mathf.Max(power-armor, 1);
                    case TDProjectile.DamageType.Base: return power/2;
                    default: return Mathf.Max(power-armor, 1);
                }
            }

        };
        public enum ArmorType
        {
            Base, // main physic armor, decrease damage
            Magic, // ignore phisic damage
            Fire, // ignore inflammation damage, momentaly low damage
            Heavy, // low damage from explosive
            Freezing, // ignore freeze damage and slow down towers
            Gold, // ignore base and acidic damage
            Capitan, // increase armor to near enemies
            Dark // high value of armor, but many damage from holy
        }

        [SerializeField] private int m_Damage;
        [SerializeField] private int m_Gold;
        [SerializeField] private int m_Armor;

        [SerializeField] private ArmorType m_ArmorType;

        private Destructible m_Destructible;

        public event Action OnEnd;
        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        private void Awake()
        {
            m_Destructible = GetComponent<Destructible>();
        }


        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = asset.spriteScale;
            sr.sprite = asset.sprite;
            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;
            

            GetComponent<Ship>().Use(asset);

            var collider = GetComponentInChildren<CircleCollider2D>();
            collider.radius = asset.radius;

            m_Damage = asset.damage;
            m_Gold = asset.gold;
            m_Armor = asset.armor;
            m_ArmorType = asset.armorType;
        }
        public void OnEndPath()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }
        public void OnEnemyDeath()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }
        public void TakeDamage(int damage, TDProjectile.DamageType damageType)
        {
            m_Destructible.ApplyDamage(ArmorDamageFunctions[(int)m_ArmorType](damage, damageType, m_Armor));
            //print(Mathf.Max(1, damage - m_Armor));
        }
    }

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;

            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
    }
}