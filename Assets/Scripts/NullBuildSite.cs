using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TowerDefense
{
    public class NullBuildSite : BuildSite
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}