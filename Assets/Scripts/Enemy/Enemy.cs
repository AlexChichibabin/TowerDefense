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
        [SerializeField] private int m_Damage;
        [SerializeField] private int m_Gold;
        [SerializeField] private int m_Armor;

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
        }
        public void OnEndPath()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }
        public void OnEnemyDeath()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }
        public void TakeDamage(int damage)
        {
            m_Destructible.ApplyDamage(Mathf.Max(1, damage - m_Armor));
            print(Mathf.Max(1, damage - m_Armor));
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