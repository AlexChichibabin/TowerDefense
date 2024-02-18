using UnityEngine;

namespace TowerDefense
{
    public class OnEnableSound : MonoBehaviour
    {
        [SerializeField] private Sound m_PlayerWin;
        [SerializeField] private Sound m_PlayerDefeat;
        public void OnEnabled(bool success)
        {
            Sound sound = success ? m_PlayerWin : m_PlayerDefeat;
            sound.Play();
        }
    }
}