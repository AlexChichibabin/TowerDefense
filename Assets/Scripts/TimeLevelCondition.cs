using UnityEngine;
using SpaceShip;

namespace TowerDefense
{
    public class TimeLevelCondition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float timeLimit = 4f;

        public bool LevelIsStoped = false;
        public bool IsCompleted => CheckCompletion();


        private void Start()
        {
            timeLimit += Time.time;
        }
        private void FixedUpdate()
        {
            CheckCompletion();
        }

        private bool CheckCompletion()
        {
            if (Time.time >= timeLimit)
            {
                if (LevelIsStoped) return false;
                return true;
            }
            return false;
        }

    }
}