using SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int m_LevelScore = 3;
        private int m_UpdatedLives = 0;

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
                        m_LevelScore--;
                    }
                    MapCompletion.SaveEpisodeResult(m_LevelScore);
                }
            );

            void StartLifeUpgrade(int _) // Начало костыля с расчетом жизней
            {
                m_UpdatedLives = _;
                //print(m_UpdatedLives);

                TDPlayer.Instance.OnLifeUpdate -= StartLifeUpgrade;
                TDPlayer.Instance.OnLifeUpdate += LifeScoreChange;
            }
            
            void LifeScoreChange(int _)
            {
                if (_ < m_UpdatedLives || _ < 3)
                {
                    m_LevelScore--;
                    TDPlayer.Instance.OnLifeUpdate -= StartLifeUpgrade;
                    TDPlayer.Instance.OnLifeUpdate -= LifeScoreChange;
                }
            }

            if (Upgrades.GetUpgradeLevel(TDPlayer.Instance.HealthUpgrade) > 0) TDPlayer.Instance.OnLifeUpdate += StartLifeUpgrade;
            else TDPlayer.Instance.OnLifeUpdate += LifeScoreChange; // Конец костыля
            
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
            DisableAll<EnemyWave>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
            DisableAll<Spell>();
            FindObjectOfType<Abilities>().gameObject.SetActive(false);
            GetComponent<TDPlayer>().enabled = false;
            TryGetComponent<TimeLevelCondition>(out var tlc);
            if (tlc) tlc.LevelIsStoped = true;
        }
    }
}