using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class GoldTextUpdate : MonoBehaviour
    {
        private Text m_Text;
        
        private void Start()
        {
            m_Text = GetComponent<Text>();
            TDPlayer.OnGoldUpdate += UpdateText;
        }

        private void UpdateText(int gold)
        {
            m_Text.text = gold.ToString();
        }
    }
}