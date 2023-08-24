//using System;
using System.Collections;
using System.Collections.Generic;
//using System.IO;
using UnityEngine;
using TowerDefense;

namespace TowerDefense
{
    public class EnemySpawner : Spawner
    {
        /// <summary>
        /// —сылки на то, что спавнить
        /// </summary>
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private EnemyAsset[] m_EnemyAssets;
        [SerializeField] private Path m_Path;

        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(m_EnemyPrefab);
            e.Use(m_EnemyAssets[Random.Range(0, m_EnemyAssets.Length)]);
            e.GetComponent<TDPatrolController>().SetPath(m_Path);

            return e.gameObject;
        }
    }
}