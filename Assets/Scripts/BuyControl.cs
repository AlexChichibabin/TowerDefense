using UnityEngine;
using System.Collections.Generic;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_RectTransform;
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
        private List<TowerBuyControl> m_ActiveControl;
        private bool IsSubscribed;

        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            IsSubscribed = true;
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            if (IsSubscribed == true) BuildSite.OnClickEvent -= MoveToBuildSite;
        }

        private void MoveToBuildSite(BuildSite builtSite)
        {
            if (builtSite)
            {                
                var position = Camera.main.WorldToScreenPoint(builtSite.transform.root.position);
                m_RectTransform.anchoredPosition = position;

                foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                {
                    Destroy(tbc.gameObject);
                }
                m_ActiveControl = new List<TowerBuyControl>();              

                foreach (var asset in builtSite.buildableTowers)
                {
                    if (asset.IsAvailable())
                    {
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActiveControl.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }

                if (m_ActiveControl.Count > 0)
                {
                    gameObject.SetActive(true);
                    var angle = 360 / m_ActiveControl.Count;
                    for (int i = 0; i < m_ActiveControl.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector3.up * 21 * m_ActiveControl.Count;
                        m_ActiveControl[i].transform.position += offset;
                    }
                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(builtSite.transform.root);
                    }
                }
            }
            else
            {
                if (m_ActiveControl != null)
                {
                    foreach (var control in m_ActiveControl) Destroy(control.gameObject);
                    m_ActiveControl.Clear();
                }                   
                gameObject.SetActive(false);
            }
        }
    }
}