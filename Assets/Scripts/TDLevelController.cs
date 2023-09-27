using SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;
        
        //public int levelScore => 1;
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(PlayerStatistics.Instance, false);
            };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener(() =>
                {
                    StopLevelActivity();
                    if (m_ReferenceTime < Time.time)
                    {
                        levelScore--;
                    }
                    MapCompletion.SaveEpisodeResult(levelScore);
                }
            );

            void LifeScoreChange(int _)
            {
                levelScore--;
                TDPlayer.OnLifeUpdate -= LifeScoreChange;
            }
            TDPlayer.OnLifeUpdate += LifeScoreChange;
        }

        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<Ship>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
            TryGetComponent<TimeLevelCondition>(out var tlc);
            if (tlc) tlc.LevelIsStoped = true;
        }
    }
}