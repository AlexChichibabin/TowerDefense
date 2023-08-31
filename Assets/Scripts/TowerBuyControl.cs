using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_TowerAsset;
        [SerializeField] private Text m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform buildSite;
        public Transform BuildSite { set { buildSite = value; } }

        private void Awake()
        {
            
        }

        private void Start()
        {
            if (m_TowerAsset)
            {
                TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
                m_Text.text = m_TowerAsset.goldCost.ToString();
                m_Button.GetComponent<Image>().sprite = m_TowerAsset.towerGUI;

            }
        }

        private void GoldStatusCheck(int gold)
        {
            if (m_TowerAsset)
            {
                if (gold >= m_TowerAsset.goldCost != m_Button.interactable)
                {
                    m_Button.interactable = !m_Button.interactable;
                    m_Text.color = m_Button.interactable ? Color.white : Color.red;
                }
            }
        }

        public void Buy()
        {
            if (m_TowerAsset && buildSite)
            {
                TDPlayer.Instance.TryBuild(m_TowerAsset, buildSite);
            }
        }
    }
}