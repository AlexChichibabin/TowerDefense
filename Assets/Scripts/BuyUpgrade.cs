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
        [SerializeField] private Text level;
        [SerializeField] private Text levelNext;
        [SerializeField] private Text levelMax;
        [SerializeField] private Text costText;
        [SerializeField] private GameObject lvlsFolder;
        [SerializeField] private Button buyButton;

        [Header ("Icon")]
        [SerializeField] private bool plusTextIsActive;
        [SerializeField] private Text plusText;
        [SerializeField] private string plusString;
        [SerializeField] private Image upgradeIcon;

        private static int costNumber = 0; //Current cost
        private bool isMax = false;

        public void Initialize()
        {
            if (upgradeIcon) upgradeIcon.sprite = asset.sprite; // Допилить
            if (plusText)
            {
                if (!plusTextIsActive) plusText.gameObject.SetActive(false);
                else plusText.text = plusString;
            }

            var level = Upgrades.GetUpgradeLevel(asset); // Int level

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

        private void SetMaxLvlText(int level)
        {
            buyButton.interactable = false;
            buyButton.transform.Find("ImageStar").gameObject.SetActive(false);
            levelMax.gameObject.SetActive(true);
            lvlsFolder.gameObject.SetActive(false);
            costText.text = "X";
            levelMax.text = $"Lvl: {level} (Max)";
        }
        private void SetCurrentLvlText(int level)
        {
            this.level.text = $"Lvl: {level}"; // Текствовый объект level
            levelNext.text = $"Lvl: {level + 1}";
            costText.text = $"Buy: {asset.costByLevel[level]}";
            costNumber = asset.costByLevel[level];
            levelMax.gameObject.SetActive(false);
        }

        public void CheckCost(int money)
        {
            if (isMax) return;

            Initialize();
            buyButton.interactable = money >= costNumber;
        }
    }
}