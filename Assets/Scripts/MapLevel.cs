using SpaceShip;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode episode;
        private int starsAmount;


        [Header("VisualLeftRight")]
        [SerializeField] private bool isLeftSided;

        [SerializeField] private SpriteRenderer m_ScrollSprite;
        [SerializeField] private SpriteRenderer m_Scores;
        [SerializeField] private Canvas m_Canvas;
        [SerializeField] private Text m_LevelNameText;
        [SerializeField] private RectTransform m_LevelClickArea;

        [SerializeField] private Vector3 m_ScrollSpritePosition;
        [SerializeField] private Vector3 m_ScoresPosition;
        [SerializeField] private Vector3 m_CanvasPosition;
        [SerializeField] private Vector3 m_LevelNameTextPosition;
        [SerializeField] private Vector3 m_LevelClickAreaPosition;

        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(episode);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (isLeftSided)
            {
                m_ScrollSprite.transform.position = m_ScrollSpritePosition;
                m_Scores.transform.position = m_ScoresPosition;
                m_Canvas.transform.position = m_CanvasPosition;
                m_LevelNameText.transform.position = m_LevelNameTextPosition;
                m_LevelClickArea.position = m_LevelClickAreaPosition;
            }
        }
#endif
    }
}