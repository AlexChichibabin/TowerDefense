using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private Text m_PointText;
        
        [SerializeField] private MapLevel m_RootLevel;

        [SerializeField] private int m_NeedStars = 3;


        /// <summary>
        /// ѕопытка активации ответвленного уровн€.
        /// јктиваци€ требует наличи€ очков и выполнени€ root-уровн€.
        /// </summary>
        public void TryActivate()
        {
            gameObject.SetActive(m_RootLevel.IsComplete);

            if (m_NeedStars > MapCompletion.Instance.TotalScores)
            {
                m_PointText.text = (m_NeedStars - MapCompletion.Instance.TotalScores).ToString();
            }
            else
            {
                m_PointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialize();
            }
        }

    }
}