using UnityEngine;
using SpaceShip;
using UnityEditor;
using UnityEditor.Events;
using System;

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))] // obsidian://open?vault=Scripts&file=TDPatrolController.cs
    public class Enemy : MonoBehaviour
    {
        private int m_Damage;
        private int m_Gold;

        public event Action OnEnd;
        private void OnDestroy()
        {
            OnEnd?.Invoke();
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
        }
        public void OnEndPath()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }
        public void OnEnemyDeath()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
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