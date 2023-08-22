using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.IO;
using TowerDefense;

namespace SpaceShip
{
    [RequireComponent(typeof(Ship))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol,
            PointsOnScriptPatrol,
            PointsOnPathObjectPatrol
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [SerializeField] private AIPointPatrol m_PatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_EvadeCollisionTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EvadeRadius;

        [SerializeField] private float m_ShootDistance;

        [SerializeField] private float m_EnemyDetectDistance;

        [SerializeField] private float m_StartTime;

        //[SerializeField] private Vector3[] m_SpecifiedPositions;

        private int m_PatrolPointNumber;

        //[SerializeField] private float m_PatrolPointAccuracy;

        private Ship m_Ship;

        private Vector3 m_MovePosition;

        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_EvadeCollisionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;
        private Timer m_StartTimer;


        private void Start()
        {
            m_Ship = GetComponent<Ship>();

            InitTimers();

            if (m_AIBehaviour == AIBehaviour.PointsOnScriptPatrol)
            {
                InitPatrolPoint();
            }

            //m_AIBehaviour = AIBehaviour.Null;
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
            {
                //if (m_StartTimer.IsFinished == true) m_AIBehaviour = AIBehaviour.Patrol;
            }

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }

            if (m_AIBehaviour == AIBehaviour.PointsOnScriptPatrol)
            {
                UpdateBehaviourPatrol();
            }

            if (m_AIBehaviour == AIBehaviour.PointsOnPathObjectPatrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            //ActionFindNewAttackTarget();
            ActionFire();
            //ActionEvadeCollision();
        }

        private void FixedUpdate()
        {
            
        }




        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.PointsOnScriptPatrol)
            {
                if (m_SelectedTarget != null) // Если есть цель атаки
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else // Если нет цели атаки
                {
                    {
                        if ((m_Ship.transform.position - m_MovePosition).magnitude < m_PatrolPoint.PatrolPointAccuracy) // Если корабль достаточно близок к точке, чтобы выбрать следующую
                        {
                            if (m_PatrolPoint.SpecifiedPositions.Length > 1) // Если в редакторе выбрано от 2х точек и больше
                            {
                                m_PatrolPointNumber++;
                                if (m_PatrolPointNumber == m_PatrolPoint.SpecifiedPositions.Length) m_PatrolPointNumber = 0;
                                m_MovePosition = m_PatrolPoint.SpecifiedPositions[m_PatrolPointNumber];
                            }
                        }
                        if (m_EvadeCollisionTimer.IsFinished == true && m_MovePosition != m_PatrolPoint.SpecifiedPositions[m_PatrolPointNumber]) // Если таймер прошел, а корабль движется не к нужной точке патрулирования
                        {
                            m_MovePosition = m_PatrolPoint.SpecifiedPositions[m_PatrolPointNumber];
                            m_RandomizeDirectionTimer.Start(m_FindNewTargetTime);
                        }
                    } 
                }
            }


            if (m_AIBehaviour == AIBehaviour.PointsOnPathObjectPatrol) // Мб не нужно
            {
                /*if ((m_Ship.transform.position - m_MovePosition).magnitude < m_PatrolPoint.PatrolPointAccuracy) // Если корабль достаточно близок к точке, чтобы выбрать следующую
                {
                    GetNewPoint();
                    m_MovePosition = m_PatrolPoint.transform.position;
                }*/

                if (m_PatrolPoint != null)
                {
                    bool isInsidePatrolZone = (m_PatrolPoint.transform.position - m_Ship.transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                    if (isInsidePatrolZone == true)
                    {
                        GetNewPoint();
                    }
                    else
                    {
                        m_MovePosition = m_PatrolPoint.transform.position;
                    }
                }
            } 


            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null) // Если есть цель атаки
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else // Если нет цели атаки
                {
                    if (m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - m_Ship.transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }


        }
        protected virtual void GetNewPoint()
        {
            if (m_RandomizeDirectionTimer.IsFinished == true)
            {
                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                m_MovePosition = newPoint;

                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
            }
        }

        private void InitPatrolPoint() // Инициализировать точки патрулирования при старте
        {
            if (m_PatrolPoint.SpecifiedPositions.Length > 1) // Если в редакторе выбрано от 2х точек и больше
            {
                m_PatrolPointNumber = m_PatrolPoint.SpecifiedPositions.Length - 1;
                m_PatrolPointNumber++;
                if (m_PatrolPointNumber == m_PatrolPoint.SpecifiedPositions.Length) m_PatrolPointNumber = 0;
                m_MovePosition = m_PatrolPoint.SpecifiedPositions[m_PatrolPointNumber];
            }
        }

        /*private void ActionEvadeCollision()
        {
            Collider2D hit = Physics2D.OverlapCircle(m_Ship.transform.position, m_EvadeRadius);

            //float angle = ComputeAlignTorqueNormalized(hit.gameObject.transform.position, m_Ship.transform, 90);

            Vector2 localTargetPosition = m_Ship.transform.InverseTransformPoint(hit.transform.position);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -90, 90);


            //Debug.Log(angle);

            if (hit != null && m_EvadeCollisionTimer.IsFinished == true)
            {
                if (angle > 0)
                {
                    m_MovePosition = transform.position - transform.right * 0.5f * angle;
                }
                if (angle < 0)
                {
                    m_MovePosition = transform.position + transform.right * 0.5f * angle;
                }
                m_EvadeCollisionTimer.Start(m_EvadeCollisionTime);
            }
        }*/


        private void ActionControlShip()
        {
            m_Ship.ThrustControl = m_NavigationLinear;

            m_Ship.TorqueControl = ComputeAlignTorqueNormalized(m_MovePosition, m_Ship.transform, MAX_ANGLE) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship, float maxAngle)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -maxAngle, maxAngle) / maxAngle;

            //Debug.Log(-angle);

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_AIBehaviour != AIBehaviour.Null)
            {
                if (m_FindNewTargetTimer.IsFinished == true)
                {
                    m_SelectedTarget = FindNearestDestructibleTarget();

                    m_FindNewTargetTimer.Start(m_FindNewTargetTime);
                }
            }
            
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = m_EnemyDetectDistance;

            Destructible potentialTarget = null;

            if (m_SelectedTarget != null) //Сохраняет текущую цель
            {
                potentialTarget = m_SelectedTarget; 
                return potentialTarget;
            }     

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<Destructible>() == m_Ship) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_Ship.TeamId) continue;

                float dist = Vector2.Distance(m_Ship.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            return potentialTarget;
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, m_ShootDistance); // Стрельба при видимом противнике
                
                if (hit)
                {
                    Destructible barrier = hit.collider.transform.root.GetComponent<Destructible>();

                    if (barrier != null && barrier == m_SelectedTarget)
                    {
                        if (m_FireTimer.IsFinished == true)
                        {
                            m_Ship.Fire(TurretMode.Primary);

                            m_FireTimer.Start(m_ShootDelay);
                        }
                    }
                }
            }
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }
        public void SetPointsOnScriptPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.PointsOnScriptPatrol;
            m_PatrolPoint = point;
        }
        public void SetPointsOnPathObjectPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.PointsOnPathObjectPatrol;
            m_PatrolPoint = point;
        }

        #region Timers
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(0);
            m_FireTimer = new Timer(0);
            m_FindNewTargetTimer = new Timer(0);
            m_EvadeCollisionTimer = new Timer(0);
            m_StartTimer = new Timer(0);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
            m_EvadeCollisionTimer.RemoveTime(Time.deltaTime);
            m_StartTimer.RemoveTime(Time.deltaTime);
        }
        #endregion


    }
}