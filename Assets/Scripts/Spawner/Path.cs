using SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea startArea;
        public CircleArea StartArea { get { return startArea; } }
        
        enum CircleGizmoMode
        {
            SolidDisk,
            WireDisk
        }

        [SerializeField] private CircleGizmoMode m_CircleGizmoMode;

        [SerializeField] private Color m_GizmosColor;

        [SerializeField] private AIPointPatrol[] points;

        public AIPointPatrol this[int i] { get { return points[i]; } }

        public int Length { get => points.Length; }



        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            foreach (var point in points)
            {
                if (m_CircleGizmoMode == CircleGizmoMode.SolidDisk)
                {
                    m_GizmosColor.a = 0.3f;
                    Handles.color = m_GizmosColor;
                    Handles.DrawSolidDisc(point.transform.position, transform.forward, point.Radius);
                }
                if (m_CircleGizmoMode == CircleGizmoMode.WireDisk)
                {
                    m_GizmosColor.a = 1;
                    Handles.color = m_GizmosColor;
                    Handles.DrawWireDisc(point.transform.position, transform.forward, point.Radius);
                }
            }
        }
        #endif
    }
}