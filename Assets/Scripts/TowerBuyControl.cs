using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_TowerAsset;
        public void SetTowerAsset(TowerAsset towerAsset) { m_TowerAsset = towerAsset; }
        [SerializeField] private Text m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform buildSite;

        private void Awake()
        {
            
        }

        private void Start()
        {
            if (m_TowerAsset)
            {
                TDPlayer.Instance.GoldUpdateSubscribe(GoldStatusCheck);
                m_Text.text = m_TowerAsset.goldCost.ToString();
                m_Button.GetComponent<Image>().sprite = m_TowerAsset.towerGUI;

            }
        }

        private void OnDestroy()
        {

        }

        public void SetBuildSite(Transform value)
        {
            buildSite = value;
        }
        private void GoldStatusCheck(int gold)
        {
            if (m_TowerAsset)
            {
                if (gold >= m_TowerAsset.goldCost != m_Button.interactable)
                {
                    m_Button.interactable = !m_Button.interactable;
                    m_Text.color = m_Button.interactable ? Color.white : Color.red;
                }
            }
        }
        public void Buy()
        {
            if (m_TowerAsset && buildSite)
            {
                TDPlayer.Instance.TryBuild(m_TowerAsset, buildSite);
                BuildSite.HideControls();
            }
        }
    }
}