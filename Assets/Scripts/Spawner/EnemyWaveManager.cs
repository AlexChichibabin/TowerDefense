using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static event Action<Enemy> OnEnemySpawn;
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;
        [SerializeField] private GameObject m_NextWaveGUI;
        public EnemyWave CurrentWave => currentWave;

        [SerializeField] private int activeEnemyCount = 0;

        public event Action OnAllWavesDead;
        private void RecordEnemyDead() 
        {
            if (--activeEnemyCount == 0)
            {
                if (currentWave)
                {
                    ForceNextWave();
                }
                else
                {
                    OnAllWavesDead?.Invoke();
                }
            }
        }

        private void Awake()
        {
            
        }
        private void Start()
        {
            currentWave.Prepare(SpawnEnemies); // Подписка на currentWave.OnWaveReady происходит внутри Prepare
        }

        private void SpawnEnemies()
        {
            //Sound.Arrow.Play();
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EnemyPrefab, paths[pathIndex].StartArea.GetRandomInsideZone2D(), Quaternion.identity);
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                        e.OnEnd += RecordEnemyDead;
                        activeEnemyCount++;
                        OnEnemySpawn?.Invoke(e);
                    }
                }
                else
                {
                    Debug.Log($"Invalid pathIndex in {name}");
                }
            }
            currentWave = currentWave.PrepareNext(SpawnEnemies); // Отписка текущей волны и подписка следующей на currentWave.OnWaveReady
        }
        public void SetActiveNextWaveGUI(bool active)
        {
            m_NextWaveGUI.SetActive(active);
        }

        public void ForceNextWave()
        {
            if (currentWave)
            {
                TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime());
                SpawnEnemies();
            }
            
        }
    }
}