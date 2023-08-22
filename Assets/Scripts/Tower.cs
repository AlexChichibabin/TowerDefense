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

        private void Start()
        {
            m_Turrets = GetComponentsInChildren<Turret>();
        }

        private void Update()
        {
            var enter = Physics2D.OverlapCircle(transform.position, m_Area.Radius);
            if (enter)
            {
                foreach (Turret t in m_Turrets)
                {
                    t.Fire();
                }
            }
        }
    }
}