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

        private EnemyWaveManager manager;
        private float timeToNextWave;
        

        private void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        public void CallWave()
        {
            manager.ForceNextWave();
        }

        private void Update()
        { 
            var bonus = (int)timeToNextWave;
            if (timeToNextWave <= 0) bonus = 0 ;
            m_BonusAmount.text = bonus.ToString();
            m_TimeToNextWaveText.text = bonus.ToString();
            timeToNextWave -= Time.deltaTime;
        }
    }
}