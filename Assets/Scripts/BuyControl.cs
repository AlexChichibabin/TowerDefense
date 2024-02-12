using UnityEngine;
using System.Collections.Generic;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_RectTransform;
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
        private List<TowerBuyControl> m_ActiveControl;

        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
            //m_ActiveControl = new List<TowerBuyControl>();
        }
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }

        private void MoveToBuildSite(BuildSite builtSite)
        {
            if (builtSite)
            {
                var position = Camera.main.WorldToScreenPoint(builtSite.transform.root.position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);

                m_ActiveControl = new List<TowerBuyControl>();
                foreach (var asset in builtSite.BuildableTowers)
                {
                    if (asset.IsAvailable())
                    {
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActiveControl.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }

                var angle = 360 / m_ActiveControl.Count;
                for (int i = 0; i < m_ActiveControl.Count; i++)
                {
                    var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector3.up * 21 * m_ActiveControl.Count;
                    m_ActiveControl[i].transform.position += offset;
                }
            }
            else
            {
                foreach (var control in m_ActiveControl) Destroy(control.gameObject);
                gameObject.SetActive(false);
            }

            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(builtSite.transform.root);
            }
        }
    }
}