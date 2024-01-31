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
    }
}