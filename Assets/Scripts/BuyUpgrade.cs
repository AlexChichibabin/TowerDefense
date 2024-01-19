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
        [SerializeField] private Text level, levelNext, cost;
        [SerializeField] private Button buyButton;

        public void Initialize()
        {
            if(upgradeIcon) upgradeIcon.sprite = asset.sprite;
            var level = Upgrades.GetUpgradeLevel(asset); // Int level
            levelNext.text = $"Lvl: {level+1}";
            this.level.text = $"Lvl: {level}"; // Текствовый объект level
            if (level >= asset.costByLevel.Length)
            {
                buyButton.enabled = false;
                buyButton.transform.Find("heart_sprite").gameObject.SetActive(false);
                buyButton.transform.Find("text").gameObject.SetActive(false);
                cost.text = "X";
            } 
            else { cost.text = $"Buy: {asset.costByLevel[level]}"; }
            
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }
    }
}