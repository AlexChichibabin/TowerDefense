using SpaceShip;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_Path;
        private int pathIndex;

        [SerializeField] private UnityEvent EndPath;

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
                EndPath.Invoke();
                Destroy(gameObject);
            }

        }
    }
}