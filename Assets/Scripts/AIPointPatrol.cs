using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class AIPointPatrol : MonoBehaviour
    {
        [Header("Common")]
        [SerializeField] private Color m_GizmoColor = Color.cyan;


        [Header("PatrolMode")]
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;


        [Header("PointsOnScriptPatrolMode")]
        [SerializeField] private float m_PatrolPointAccuracy;
        public float PatrolPointAccuracy => m_PatrolPointAccuracy;

        [SerializeField] private Vector3[] m_SpecifiedPositions;
        public Vector3[] SpecifiedPositions => m_SpecifiedPositions;


#if UNITY_EDITOR
        private void OnDrawGizmosSelected() // Для мода Patrol
        {
            Gizmos.color = m_GizmoColor;
            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
        private void OnDrawGizmos() // Для мода PointPatrol
        {
            Gizmos.color = m_GizmoColor;
            if (m_SpecifiedPositions.Length > 1)
            {
                for (int i = 0; i < m_SpecifiedPositions.Length; i++)
                {
                    Gizmos.DrawWireSphere(m_SpecifiedPositions[i], m_PatrolPointAccuracy);
                }
            }
        }
#endif
    }
}