using SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SpaceShip;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private CircleArea m_Area;
        private Turret[] m_Turrets;
        private Destructible m_Target;

        private void Start()
        {
            m_Turrets = GetComponentsInChildren<Turret>();
        }

        private void Update()
        {
            if (m_Target != null)
            {
                Vector2 targetVector = m_Target.transform.position - transform.position;
                if (targetVector.magnitude <= m_Area.Radius)
                {
                    foreach (Turret t in m_Turrets)
                    {
                        t.transform.up = targetVector/*.normalized*/;
                        t.Fire();
                    }
                }
                else
                {
                    m_Target = null;
                    foreach (Turret t in m_Turrets)
                    {
                        t.SetTarget(m_Target);
                    }
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Area.Radius);
                if (enter)
                {
                    m_Target = enter.transform.root.GetComponent<Destructible>();
                    foreach (Turret t in m_Turrets)
                    {
                        t.SetTarget(m_Target);
                    }
                }
            }
        }
    }
}