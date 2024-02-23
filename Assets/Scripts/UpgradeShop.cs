using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private int money;
        [SerializeField] private Text moneyText;

        [SerializeField] private BuyUpgrade[] sales;

        private void Awake()
        {
            if (m_PreviousPage == null) m_PreviousButton.gameObject.SetActive(false);
            if (m_NextPage == null) m_NextButton.gameObject.SetActive(false);
        }
        private void Start()
        {
            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }
            UpdateMoney();
        }
        public void UpdateMoney()
        {
            money = MapCompletion.Instance.TotalScores;
            money -= Upgrades.Instance.GetTotalCost();
            moneyText.text = money.ToString();
            foreach (var slot in sales)
            {
                slot.CheckCost(money);
            }
        }

        #region UI
        [Header("UI")]
        [SerializeField] private GameObject m_PreviousPage;
        [SerializeField] private GameObject m_NextPage;
        [SerializeField] private Button m_PreviousButton;
        [SerializeField] private Button m_NextButton;

        public void OnPreviousButtonClick()
        {
            gameObject.SetActive(false);
            m_PreviousPage.SetActive(true);
        }
        public void OnNextButtonClick()
        {
            gameObject.SetActive(false);
            m_NextPage.SetActive(true);
        }
        #endregion
    }
}