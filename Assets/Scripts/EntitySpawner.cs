//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }
        [SerializeField] private SpawnMode m_SpawnMode;
        [SerializeField] private Entity[] m_EntityPrefabs;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private int m_NumSpawns;
        [SerializeField] private float m_RespawnTime;

        [SerializeField] private AIPointPatrol m_PatrolPoint;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }
        }

        private void Update()
        {
            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
            }

            if (m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            if (m_EntityPrefabs != null && m_EntityPrefabs.Length > 0)
            {
                for (int i = 0; i < m_NumSpawns; i++)
                {
                    int index = Random.Range(0, m_EntityPrefabs.Length);

                    GameObject e = Instantiate(m_EntityPrefabs[index].gameObject);

                    e.transform.position = m_Area.GetRandomInsideZone();

                    if (e.TryGetComponent<AIController>(out var ai))
                    {
                        ai.SetPointPatrolBehaviour(m_PatrolPoint);
                    }
                }
            }

        }

    }
}