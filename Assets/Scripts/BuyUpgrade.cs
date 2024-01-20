using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Text level, levelNext, levelMax, costText;
        [SerializeField] private GameObject lvlsFolder;
        [SerializeField] private Button buyButton;

        private static int costNumber = 0;
        private bool isMax = false;

        public void Initialize()
        {
            if(upgradeIcon) upgradeIcon.sprite = asset.sprite; // Допилить
            var level = Upgrades.GetUpgradeLevel(asset); // Int level
          
            //this.level.text = $"Lvl: {level}"; 
            if (level >= asset.costByLevel.Length)
            {               
                SetMaxLvlText();
                levelMax.text = $"Lvl: {level} (Max)";
                isMax = true;
                costNumber = int.MaxValue;
            }
            else 
            {
                this.level.text = $"Lvl: {level}"; // Текствовый объект level
                levelNext.text = $"Lvl: {level + 1}";
                costText.text = $"Buy: {asset.costByLevel[level]}";
                costNumber = asset.costByLevel[level];
                levelMax.gameObject.SetActive(false);
            }
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }

        private void SetMaxLvlText()
        {
            buyButton.interactable = false;
            buyButton.transform.Find("ImageStar").gameObject.SetActive(false);
            levelMax.gameObject.SetActive(true);
            lvlsFolder.gameObject.SetActive(false);
            costText.text = "X";
        }

        public void CheckCost(int money)
        {
            if (isMax)
            {
                return;
            }
            Initialize();
            buyButton.interactable = money >= costNumber;
            print(costNumber);
        }
    }
}