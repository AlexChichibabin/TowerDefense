using System;
using UnityEngine;
using SpaceShip;
using System.Collections;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Abilities : SingletonBase<Abilities>
    {
        public interface Usable { void Use(); }

        [Serializable]
        public class FireAbility: Usable
        {
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private int m_Damage = 50;
            public void Use()
            {

            }
        }

        [Serializable]
        public class FreezeAbility: Usable
        {
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private float m_CoolDown = 5f;
            [SerializeField] private float m_Duration = 5;
            public void Use()
            {
                void Slow(Enemy ship) // Slow all enemies
                {
                    ship.GetComponent<Ship>().HalfMaxLinearVelocity();
                }
                foreach (var ship in FindObjectsOfType<Ship>())
                {
                    ship.HalfMaxLinearVelocity();
                }
                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore() // Restore all enemies
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<Ship>())
                    {
                        ship.RestoreMaxLinearVelocity();
                    }
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }
                Instance.StartCoroutine(Restore());
                
                IEnumerator TimeAbilityButton()
                {
                    Instance.m_FreezeButton.interactable = false;
                    yield return new WaitForSeconds(m_CoolDown);
                    Instance.m_FreezeButton.interactable = true;
                }
                Instance.StartCoroutine(TimeAbilityButton());
            }
        }

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        [SerializeField] private FreezeAbility m_FreezeAbility;
        [SerializeField] private Button m_FreezeButton;
        public void UseFreezeAbility() => m_FreezeAbility.Use();
    }
}