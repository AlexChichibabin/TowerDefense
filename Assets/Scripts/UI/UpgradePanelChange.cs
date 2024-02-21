using UnityEngine;

namespace TowerDefense
{
    public class UpgradePanelChange : MonoBehaviour
    {
        [SerializeField] private Animator m_PanelAnimator;
        [SerializeField] private Animator m_ButtonAnimator;

        public void DisappearUpgradeButton()
        {
            m_ButtonAnimator.Play("UpgradesButtonDisappearance");
        }
        public void AppearUpgradeButton()
        {
            m_ButtonAnimator.Play("UpgradesButtonAppearance");
        }

        public void DisappearUpgradePanel()
        {
            m_PanelAnimator.Play("UpgradesPanelDisappearance");
        }
        public void AppearUpgradePanel()
        {
            m_PanelAnimator.Play("UpgradesPanelAppearance");

        }
    }
}