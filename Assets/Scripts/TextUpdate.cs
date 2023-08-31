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
            Life
        }

        private Text m_Text;
        public UpdateSourse m_Sourse = UpdateSourse.Gold;

        private void Start()
        {
            m_Text = GetComponent<Text>();

            switch (m_Sourse) 
            {
                case UpdateSourse.Gold:
                    TDPlayer.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSourse.Life:
                    TDPlayer.LifeUpdateSubscribe(UpdateText);
                    break;
            }
        }

        private void UpdateText(int amount)
        {
            m_Text.text = amount.ToString();
        }
    }
}