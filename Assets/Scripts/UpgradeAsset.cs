using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite Sprite;
        public int IncreaseValue;

        public int[] CostByLevel = { 4 };

    }
}
