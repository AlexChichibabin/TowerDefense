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
    }
}