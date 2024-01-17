using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Text levelCurrent, levelNext, cost;
        [SerializeField] private Button buyButton;

        public void SetUpgrade(UpgradeAsset asset, int levelNext = 1)
        {
            upgradeIcon.sprite = asset.sprite;
            this.levelNext.text = levelNext.ToString();
            levelCurrent.text = (levelNext-1).ToString();
            cost.text = asset.costByLevel[levelNext].ToString();
        }
    }
}