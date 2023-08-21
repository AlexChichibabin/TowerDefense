using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class AIPointPatrol : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        [SerializeField] private Vector3[] m_SpecifiedPositions;
        public Vector3[] SpecifiedPositions => m_SpecifiedPositions;

        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

        [SerializeField] private float m_PatrolPointAccuracy;
        public float PatrolPointAccuracy => m_PatrolPointAccuracy;



#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = GizmoColor;
            if (m_SpecifiedPositions.Length > 1)
            {
                for (int i = 0; i < m_SpecifiedPositions.Length; i++)
                {
                    Gizmos.DrawSphere(m_SpecifiedPositions[i], m_PatrolPointAccuracy);
                }
            }
        }
        #endif
    }
}