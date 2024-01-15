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

        public void TryActivate()
        {
            gameObject.SetActive(rootLevel.IsComplete);
            print($"rootLevel is: {rootLevel.IsComplete}");
            print($"TotalScores from MapCompletion: {MapCompletion.Instance.TotalScores}");
            if (needStars > MapCompletion.Instance.TotalScores)
            {
                pointText.text = (needStars - MapCompletion.Instance.TotalScores).ToString();
            }
            else
            {
                print($"blockPanel before: {pointText.transform.parent.gameObject.activeSelf}");
                pointText.transform.parent.gameObject.SetActive(false);
                print($"blockPanel after: {pointText.transform.parent.gameObject.activeSelf}");
                GetComponent<MapLevel>().Initialize();
            }
        }

    }
}