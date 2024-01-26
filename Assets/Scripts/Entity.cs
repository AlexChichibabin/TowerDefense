using UnityEngine;

namespace SpaceShip
{
    /// <summary>
    /// base class for all interactive game objects on scene
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// object name for user
        /// </summary>
        [SerializeField] protected string m_Nickname;
        public string Nickname => m_Nickname;

        public void SetName(string nickname)
        {
            m_Nickname = nickname;
            gameObject.name = nickname;
        }
    }
}
