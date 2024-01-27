using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [Header("UpgradeAsset")]
        [SerializeField] private UpgradeAsset asset;

        [Header("Interface")]
        [SerializeField] private Text m_LevelText;
        [SerializeField] private Text m_NextLevelText;
        [SerializeField] private GameObject m_LvlTextsFolder;
        [SerializeField] private Text m_MaxLevelText;
        [SerializeField] private Text m_UpgradeCostText;
        [SerializeField] private Button m_BuyButton;

        [Header ("Icon")]
        [SerializeField] private bool m_DescribtionTextIsActive;
        [SerializeField] private Text m_DescribtionText;
        [SerializeField] private string m_DescribtionTextString;
        [SerializeField] private Image m_UpgradeIcon;

        private static int costNumber = 0; // Current cost
        private bool isMax = false;

        public void Initialize()
        {
            if (m_UpgradeIcon) m_UpgradeIcon.sprite = asset.sprite;
            if (m_DescribtionText)
            {
                if (!m_DescribtionTextIsActive) m_DescribtionText.gameObject.SetActive(false);
                else m_DescribtionText.text = m_DescribtionTextString;
            }

            var level = Upgrades.GetUpgradeLevel(asset); 

            if (level >= asset.costByLevel.Length)
            {
                SetMaxLvlText(level);
                isMax = true;
                costNumber = int.MaxValue;
            }
            else
            {
                SetCurrentLvlText(level);
            }
        }
        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }
        public void CheckCost(int money)
        {
            if (isMax) return;

            Initialize();
            m_BuyButton.interactable = money >= costNumber;
        }



        private void SetMaxLvlText(int level)
        {
            m_BuyButton.interactable = false;
            m_BuyButton.transform.Find("ImageStar").gameObject.SetActive(false);
            m_MaxLevelText.gameObject.SetActive(true);
            m_LvlTextsFolder.gameObject.SetActive(false);
            m_UpgradeCostText.text = "X";
            m_MaxLevelText.text = $"Lvl: {level} (Max)";
        }
        private void SetCurrentLvlText(int level)
        {
            this.m_LevelText.text = $"Lvl: {level}"; // Текствовый объект level
            m_NextLevelText.text = $"Lvl: {level + 1}";
            m_UpgradeCostText.text = $"Buy: {asset.costByLevel[level]}";
            costNumber = asset.costByLevel[level];
            m_MaxLevelText.gameObject.SetActive(false);
        }


    }
}