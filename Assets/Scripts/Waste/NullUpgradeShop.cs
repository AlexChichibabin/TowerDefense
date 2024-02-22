using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class NullUpgradeShop : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Animator m_UpgradePanel;

        public void OnPointerDown(PointerEventData eventData)
        {
            m_UpgradePanel.Play("UpgradesPanelDisappearance");
        }
    }
}