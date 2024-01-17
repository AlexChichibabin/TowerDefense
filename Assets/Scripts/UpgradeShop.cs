using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [Serializable]
        private class UpgradeSlot
        {

        }

        [SerializeField] private int money;
        [SerializeField] private Text moneyText;

        [SerializeField] private BuyUpgrade[] sales;

        [SerializeField] private int lvlCurrent;
        [SerializeField] private Text lvlCurrentText;

        [SerializeField] private int lvlNext;
        [SerializeField] private Text lvlNextText;



        private void Start()
        {
            money = MapCompletion.Instance.TotalScores;
            moneyText.text = money.ToString();
        }
    }
}