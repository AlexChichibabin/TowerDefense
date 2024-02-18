using UnityEngine;
using System;
using System.Collections.Generic;


namespace TowerDefense
{
    public class EnemyWave: MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        [SerializeField] private EnemyWave m_NextWave;
        public EnemyWave NextWave => m_NextWave;

        [SerializeField] private PathGroup[] m_Groups;

        [SerializeField] private float m_PrepareTime = 10f;
        public float PrepareTime => m_PrepareTime;
        [HideInInspector] public float GetRemainingTime() { return m_PrepareTime - Time.time; }

        public static Action<float> OnWavePrepare;
        

        private void Awake()
        {
            enabled = false;
        }

        private event Action OnWaveReady;

        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(m_PrepareTime);
            m_PrepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
            
        }

        private void Update()
        {
            if (Time.time >= m_PrepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke(); // Такая запись с "?" равна if(OnWaveReady) {Invoke()}  Т.е. если подписка на OnWaveReady не null
            }
        }
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < m_Groups.Length; i++)
            {
                foreach (var squad in m_Groups[i].squads) 
                {
                    yield return (squad.asset, squad.count, i);
                }
            }
        }
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if (m_NextWave) m_NextWave.Prepare(spawnEnemies);
            else
            {
                OnWavePrepare?.Invoke(0);
                GetComponentInParent<EnemyWaveManager>().SetActiveNextWaveGUI(m_NextWave); // Ищет родителя EnemyWaveManager и вырубает UI отсчет времени и бонусное окно
            }               
            return m_NextWave;
        }


    }
}