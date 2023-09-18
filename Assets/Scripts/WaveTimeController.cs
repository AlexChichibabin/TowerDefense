using UnityEngine;
using UnityEngine.UI;
using System;

namespace TowerDefense
{
    public class WaveTimeController : SingletonBase<WaveTimeController>
    {
        [SerializeField] private EnemySpawner m_Spawner;
        [SerializeField] private Text m_TimerText;
        [SerializeField] private float m_StartWaveTime;
        public float StartWaveTime => m_StartWaveTime;

        private Timer m_StartWaveTimer;
        private int m_CurrentInt;

        public static event Action<int> StartWaveTimerTextUpdateSubscribe;

        private void Start()
        {
            m_Spawner.gameObject.SetActive(false);
            m_StartWaveTimer = new Timer(m_StartWaveTime);
            m_StartWaveTimer.Start(m_StartWaveTime);
            m_CurrentInt = (int)m_StartWaveTimer.CurrentTime;
        }

        private void Update()
        {
            if (m_StartWaveTimer.CurrentTime > 0)
            {
                m_StartWaveTimer.RemoveTime(Time.deltaTime);
                if ((m_CurrentInt) > (int)m_StartWaveTimer.CurrentTime)
                {
                    m_CurrentInt = (int)m_StartWaveTimer.CurrentTime;
                    StartWaveTimerTextUpdateSubscribe((int)m_StartWaveTimer.CurrentTime);
                }
            }
            if (m_StartWaveTimer.IsFinished == true)
            {
                m_Spawner.gameObject.SetActive(true);
                m_TimerText.gameObject.SetActive(false);
            }
        }
    }
}