using UnityEngine;
using SpaceShip;
using UnityEditor;
using UnityEditor.Events;

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        private int m_Damage;
        private int m_Gold;

        /*private void Start()
        {
            transform.GetComponent<TDPatrolController>().EndPath.AddListener(OnEndPath);
        }*/
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
            Player.Instance.ApplyDamage(m_Damage);
        }
        public void OnEnemyDeath()
        {
            (Player.Instance as TDPlayer).ChangeGold(m_Gold);
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