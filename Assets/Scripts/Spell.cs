using UnityEngine;


namespace TowerDefense
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset requiredUpgrade;
        [SerializeField] private int requiredUpgradeLevel;

        public bool IsAvailable() => !requiredUpgrade ||
                    requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
    }
}