using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;

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

        [SerializeField] private PathGroup[] groups;

        [SerializeField] private float prepareTime = 10f;
        public float PrepareTime => prepareTime;

        [HideInInspector]
        public float GetRemainingTime() { return prepareTime - Time.time; }

        public static Action<float> OnWavePrepare;
        

        private void Awake()
        {
            enabled = false;
        }

        private event Action OnWaveReady;

        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(prepareTime);
            prepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
            
        }

        private void Update()
        {
            if (Time.time >= prepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke(); // Такая запись с "?" равна if(OnWaveReady) {Invoke()}  Т.е. если подписка на OnWaveReady не null
            }
        }
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads) 
                {
                    yield return (squad.asset, squad.count, i);
                }
            }
        }

        [SerializeField] private EnemyWave nextWave;
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if (nextWave) nextWave.Prepare(spawnEnemies);
            else OnWavePrepare?.Invoke(0);
            return nextWave;
        }


    }
}