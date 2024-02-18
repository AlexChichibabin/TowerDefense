using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text m_BonusAmount;
        [SerializeField] private Text m_TimeToNextWaveText;

        private EnemyWaveManager m_Manager;
        private float m_TimeToNextWave;


        private void Awake()
        {
            EnemyWave.OnWavePrepare += (float time) =>
            {
                m_TimeToNextWave = time - 0.002f;
            };
        }
        private void Start()
        {
            m_Manager = FindObjectOfType<EnemyWaveManager>();
        }

        public void CallWave()
        {
            m_Manager.ForceNextWave();
        }

        private void Update()
        { 
            var bonus = (int)m_TimeToNextWave + 1;
            if (m_TimeToNextWave <= 0) bonus = 0 ;
            m_BonusAmount.text = bonus.ToString();
            m_TimeToNextWaveText.text = bonus.ToString();
            m_TimeToNextWave -= Time.deltaTime;
        }
    }
}