using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite Sprite;
        public int IncreaseValue = 1;

        public int[] CostByLevel = { 4 };

    }
}
