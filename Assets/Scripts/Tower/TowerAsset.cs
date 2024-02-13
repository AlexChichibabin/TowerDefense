using SpaceShip;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public Color spriteColor = Color.white;
        public TurretProperties turretProperties;
        public int goldCost = 15;
        public Sprite towerGUI;
        public Sprite towerSprite;
        [SerializeField] private UpgradeAsset requiredUpgrade;
        [SerializeField] private int requiredUpgradeLevel;


        public bool IsAvailable() => !requiredUpgrade ||
                    requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
        /*public bool IsAvailable()
        {
            //Debug.Log(Upgrades.GetUpgradeLevel(requiredUpgrade));
            if (requiredUpgrade != null) return true;
            else 
                if(requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade)) return true;

            return false;
        }*/


        public TowerAsset[] m_UpgradesTo;
    }
}