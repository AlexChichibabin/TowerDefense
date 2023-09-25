using SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        public int levelScore => 1;
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(PlayerStatistics.Instance, false);
            };
            m_EventLevelCompleted.AddListener(() =>
                {
                    StopLevelActivity();
                    MapCompletion.SaveEpisodeResult(levelScore);
                }
            );
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
            TimeLevelCondition tlc = GetComponent<TimeLevelCondition>();
            tlc.LevelIsStoped = true;
        }
    }
}