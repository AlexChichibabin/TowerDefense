using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TowerAsset[] m_BuildableTowers;
        public TowerAsset[] BuildableTowers { get { return m_BuildableTowers; } }
        public static event Action<BuildSite> OnClickEvent;

        public static void HideControls()
        {
            OnClickEvent(null);
        }
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }
    }
}