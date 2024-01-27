using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSourse
        {
            Gold,
            Life, 
            WaveTimer
        }

        //[SerializeField] private EnemySpawner m_Spawner;

        private Text m_Text;
        public UpdateSourse m_Sourse = UpdateSourse.Gold;

        private void Start()
        {
            m_Text = GetComponent<Text>();

            switch (m_Sourse)
            {
                case UpdateSourse.Gold:
                    TDPlayer.Instance.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSourse.Life:
                    TDPlayer.Instance.LifeUpdateSubscribe(UpdateText);
                    break;
                case UpdateSourse.WaveTimer:
                    WaveTimeController.StartWaveTimerTextUpdateSubscribe += UpdateText;
                    break;
            }
        }

        private void UpdateText(int amount)
        {
            if (m_Text)
            {
                m_Text.text = amount.ToString();
            }
        }
    }
}