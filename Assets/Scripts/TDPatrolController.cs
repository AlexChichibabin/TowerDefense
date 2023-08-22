using SpaceShip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_Path;
        private int pathIndex;

        public void SetPath(Path newPath)
        {
            m_Path = newPath;
            pathIndex = 0;
            SetPointsOnPathObjectPatrolBehaviour(m_Path[pathIndex]);
        }

        protected override void GetNewPoint()
        {
            pathIndex += 1;
            if (m_Path.Length > pathIndex)
            {
                SetPointsOnPathObjectPatrolBehaviour(m_Path[pathIndex]);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}