using UnityEngine;

namespace TowerDefense
{
    public class LevelVisualScores : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] starsObjects;

        [SerializeField] private Color starColor = Color.yellow;

        public void SetStars(int scores)
        {
            PaintStars(scores);
        }

        private void PaintStars(int scores)
        {
            for (int i = 0; i < scores; i++)
            {
                starsObjects[i].color = starColor;
            }
        }
    }
}