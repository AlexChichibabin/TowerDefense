using SpaceShip;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        private Episode m_Episode;
        private int m_StarsAmount;
        [SerializeField] private LevelVisualScores m_VisualScores;

        private void Awake()
        {
            m_VisualScores = GetComponentInChildren<LevelVisualScores>();
        }
        private void Start()
        {
            //print(m_Episode);
        }

        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        public void SetLevelData(Episode episode, int scores)
        {
            m_Episode = episode;
            m_StarsAmount = scores;
            m_VisualScores.SetStars(scores);
        }
        /*[Header("VisualLeftRight")]
        [SerializeField] private bool isLeftSided;

        [SerializeField] private SpriteRenderer m_ScrollSprite;
        [SerializeField] private SpriteRenderer m_Scores;
        [SerializeField] private Canvas m_Canvas;
        [SerializeField] private Text m_LevelNameText;
        [SerializeField] private RectTransform m_LevelClickArea;

        [Header("LeftSidePositions")]
        [SerializeField] private Vector3 m_LScrollSpritePosition;
        [SerializeField] private Vector3 m_LScoresPosition;
        [SerializeField] private Vector3 m_LCanvasPosition;
        [SerializeField] private Vector3 m_LLevelNameTextPosition;
        [SerializeField] private Vector3 m_LLevelClickAreaPosition;

        [Header("RightSidePositions")]
        [SerializeField] private Vector3 m_RScrollSpritePosition;
        [SerializeField] private Vector3 m_RScoresPosition;
        [SerializeField] private Vector3 m_RCanvasPosition;
        [SerializeField] private Vector3 m_RLevelNameTextPosition;
        [SerializeField] private Vector3 m_RLevelClickAreaPosition;

        

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (isLeftSided)
            {
                //m_ScrollSprite.transform.localPosition = m_ScrollSpritePosition;
               // m_Scores.transform.localposition = m_ScoresPosition;
                //m_Canvas.transform.localposition = m_CanvasPosition;
                m_LevelNameText.transform.localPosition = m_LLevelNameTextPosition;
                m_LevelNameText.text = m_LevelNameText.GetComponentInParent<MapLevel>().name;
                //m_LevelClickArea.localposition = m_LevelClickAreaPosition;
            }
            else
            {
                //m_ScrollSprite.transform.localPosition = m_ScrollSpritePosition;
                // m_Scores.transform.localposition = m_ScoresPosition;
                //m_Canvas.transform.localposition = m_CanvasPosition;
                m_LevelNameText.transform.localPosition = m_RLevelNameTextPosition;
                m_LevelNameText.text = m_LevelNameText.GetComponentInParent<MapLevel>().name;
                //m_LevelClickArea.localposition = m_LevelClickAreaPosition;
            }
        }
#endif*/
    }
}