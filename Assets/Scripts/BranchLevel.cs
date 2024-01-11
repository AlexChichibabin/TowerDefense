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
        [SerializeField] private Text pointText;
        
        [SerializeField] private MapLevel rootLevel;

        [SerializeField] private int needStars = 3;

        internal void TryActivate()
        {
            gameObject.SetActive(rootLevel.IsComplete);
            if (needStars > MapCompletion.Instance.TotalScores)
            {
                pointText.text = (needStars - MapCompletion.Instance.TotalScores).ToString();
            }
            else
            {
                pointText.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}