//using System;
using System.Collections;
using System.Collections.Generic;
//using System.IO;
using UnityEngine;
using TowerDefense;

namespace TowerDefense
{
    public class EntitySpawner : Spawner
    {
        /// <summary>
        /// —сылки на то, что спавнить
        /// </summary>
        [SerializeField] private GameObject[] m_EntityPrefabs;
        [SerializeField] private Path m_Path;

        /// <summary>
        /// ѕереопредел€ет сущность, которую надо спавнить
        /// </summary>
        /// <returns></returns>
        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }
    }
}