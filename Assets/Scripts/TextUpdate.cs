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

        }

        private Text m_Text;

        private void Start()
        {
            m_Text = GetComponent<Text>();
            TDPlayer.OnLifeUpdate += UpdateText;
        }

        private void UpdateText(int gold)
        {
            m_Text.text = gold.ToString();
        }
    }
}