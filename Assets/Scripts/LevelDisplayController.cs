using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] levels;
        [SerializeField] private BranchLevel[] branchLevels;

        private void Start()
        {
            var drawLevel = 0;
            int score = 1;

            while (score != 0 && drawLevel < levels.Length && 
                MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                levels[drawLevel].SetLevelData(episode, score);
                drawLevel++;
            }
            for (int i = drawLevel; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < branchLevels.Length; i++)
            {
                branchLevels[i].TryActivate();
            }
        }
    }
}