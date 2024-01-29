using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_RectTransform;
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;


        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            //gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }

        private void MoveToBuildSite(Transform builtSite)
        {
            if (builtSite)
            {
                var position = Camera.main.WorldToScreenPoint(builtSite.position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }

            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>()) 
            {
                tbc.SetBuildSite(builtSite);
            }
        }
    }
}