using System;
using UnityEngine;
using SpaceShip;
using System.Collections;
using UnityEngine.UI;
using static TowerDefense.TDProjectile;
//using static UnityEditor.PlayerSettings;


namespace TowerDefense
{
    public class Abilities : SingletonBase<Abilities>
    {
        private Spell[] m_ActiveSpells;
        private void Start()
        {
            m_ActiveSpells = GetComponentsInChildren<Spell>();

            foreach (var spell in m_ActiveSpells)
            {
                print(spell.IsAvailable());
                if (spell.IsAvailable() == false)
                {
                    spell.gameObject.SetActive(false);
                }
            }
            
            if (m_ActiveSpells.Length > 0)
            {
                gameObject.SetActive(true);
                for (int i = 0; i < m_ActiveSpells.Length; i++)
                {
                    if (i > 4) continue;
                    var spellRect = m_ActiveSpells[i].GetComponent<RectTransform>();
                    spellRect.anchoredPosition = new Vector3(-400 + 200*i, 0, 0);
                    
                }
            }
        }
        [Serializable]
        public class FireAbility
        {
            [SerializeField] private ImpactAreaAttack m_ImpactAreaAttackPrefab;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private DamageType m_DamageType;
            [SerializeField] private int m_Damage = 50;
            [SerializeField] private int m_Cost = 10;      
            
            public void Use()
            {
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    if (m_ImpactAreaAttackPrefab != null)
                    {
                        ImpactAreaAttack expl = Instantiate(m_ImpactAreaAttackPrefab, position, Quaternion.identity);
                        expl.SetProjectileProperties(m_Damage, m_DamageType);
                    }
                });
            }
        }

        [Serializable]
        public class FreezeAbility
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

        [SerializeField] private Image m_TargetingCircle;
        //[SerializeField] private Image m_ClickProtection;
        [SerializeField] private Button m_FreezeButton;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        [SerializeField] private FreezeAbility m_FreezeAbility;
        public void UseFreezeAbility() => m_FreezeAbility.Use();

    }
}