using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShip;

namespace TowerDefense
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string filename = "upgrades.dat";


        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }

        [SerializeField] private UpgradeSave[] save;
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    upgrade.level++;
                    Saver<UpgradeSave[]>.Save(filename, Instance.save);
                }
            }
        }

        public int GetTotalCost()
        {
            int result = 0;

            foreach (var upgrade in Instance.save)
            {
                for (int i = 0; i < upgrade.level; i++)
                {
                    result += upgrade.asset.costByLevel[i];
                }
            }
            return result;
        }

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(filename, ref save);
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.level;
                }
            }
            return 0;
        }
    }
}