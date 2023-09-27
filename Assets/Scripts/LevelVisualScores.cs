using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class LevelVisualScores : MonoBehaviour
    {
        private int currentScores;
        [SerializeField] private SpriteRenderer[] starsObjects;

        [SerializeField] private Color starColor = Color.yellow;

        private void Start()
        {

        }

        public void SetStars(int scores)
        {
            currentScores = scores;
            PaintStars(scores);
        }

        private void PaintStars(int scores)
        {
            for (int i = 0; i < scores; i++)
            {
                starsObjects[i].color = starColor;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {

        }
#endif
    }
}