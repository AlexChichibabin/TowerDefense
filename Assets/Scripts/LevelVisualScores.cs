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
        //[SerializeField][Range(0, 255)] private int blackInvisibility = 188;

        private void Start()
        {
            //PaintStars(currentScores);
        }

        public void SetStars(int scores)
        {
            currentScores = scores;
            PaintStars(scores);
        }

        private void PaintStars(int scores)
        {

            /*foreach (var s in starsObjects)
            {
                s.color = new Color(0, 0, 0, blackInvisibility);
                //print(s.color.a);
            }*/

            for (int i = 0; i < scores; i++)
            {
                starsObjects[i].color = starColor;
            }

        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            //PaintStars(currentScores);
        }
#endif
    }
}