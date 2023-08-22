using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShip
{
    public class CircleArea : MonoBehaviour
    {
        enum CircleGizmoMode
        {
            SolidDisk,
            WireDisk
        }

        [SerializeField] private float m_Radius;
        [SerializeField] private Color m_GizmosColor;
        [SerializeField] private CircleGizmoMode m_CircleGizmoMode;
        public float Radius => m_Radius;

        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_Radius;
        }

        #if UNITY_EDITOR
        //private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            
            if(m_CircleGizmoMode == CircleGizmoMode.SolidDisk)
            {
                m_GizmosColor.a = 0.3f;
                Handles.color = m_GizmosColor; 
                Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
            }
            if (m_CircleGizmoMode == CircleGizmoMode.WireDisk)
            {
                m_GizmosColor.a = 1;
                Handles.color = m_GizmosColor;
                Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
            }
        }
        #endif


    }
}