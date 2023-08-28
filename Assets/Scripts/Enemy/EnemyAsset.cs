using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public Sprite sprite;
        public RuntimeAnimatorController animations;

        [Header("Игровые параметры")]
        public float moveSpeed = 1;
        public int hp = 1;
        public int score = 1;
        public float radius = 0.26f;
        public int damage = 1;
        public int gold = 1;
    }
}
