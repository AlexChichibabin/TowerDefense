using System;
using UnityEngine;
using SpaceShip;
using System.Collections;
using UnityEngine.UI;
using static TowerDefense.TDProjectile;


namespace TowerDefense
{
    public class Abilities : SingletonBase<Abilities>
    {
        private Spell[] m_ActiveSpells;
        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button m_FreezeButton;
        [SerializeField] private Button m_FireButton;
        [SerializeField] private FreezeAbility m_FreezeAbility;
        [SerializeField] private FireAbility m_FireAbility;
        [SerializeField] private Text m_FreezeManaCostText;
        [SerializeField] private Text m_FireManaCostText;
        [SerializeField] private UpgradeAsset m_FreezeUpgrade;
        [SerializeField] private UpgradeAsset m_FireUpgrade;


        private void Start()
        {
            // Подписка на золото и манну
            TDPlayer.Instance.ManaUpdateSubscribe(ManaStatusCheck);
            m_FreezeManaCostText.text = m_FreezeAbility.ManaCost.ToString();
            m_FireManaCostText.text = m_FireAbility.ManaCost.ToString();
            if (m_FireUpgrade) ChangeExplosionDamage();
            if (m_FreezeUpgrade) ChangeFreezeDuration();

            /*print("Explosion spell damage is: " + m_FireAbility.m_Damage);
            print("Freeze spell duration is: " + m_FreezeAbility.m_Duration);*/

            m_ActiveSpells = GetComponentsInChildren<Spell>();

            foreach (var spell in m_ActiveSpells)
            {
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
                    spellRect.anchoredPosition = new Vector3(-400 + 200 * i, 0, 0);

                }
            }
        }
        private void ManaStatusCheck(int mana)
        {
            if (mana >= m_FreezeAbility.ManaCost != m_FreezeButton.interactable)
            {
                m_FreezeButton.interactable = !m_FreezeButton.interactable;
                m_FreezeManaCostText.color = m_FreezeButton.interactable ? Color.white : Color.red;
            }
            if (mana >= m_FireAbility.ManaCost != m_FireButton.interactable)
            {
                m_FireButton.interactable = !m_FireButton.interactable;
                m_FireManaCostText.color = m_FireButton.interactable ? Color.white : Color.red;
            }
        }
        private void ChangeExplosionDamage()
        {
            if (m_FireUpgrade) m_FireAbility.SetDamage(m_FireUpgrade.IncreaseValue * Upgrades.GetUpgradeLevel(m_FireUpgrade));
        }
        private void ChangeFreezeDuration()
        {
            if (m_FreezeUpgrade) m_FreezeAbility.SetDuration(m_FreezeUpgrade.IncreaseValue * Upgrades.GetUpgradeLevel(m_FreezeUpgrade));
        }

        [Serializable]
        public class FireAbility
        {
            [SerializeField] private ImpactAreaAttack m_ImpactAreaAttackPrefab;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private DamageType m_DamageType;
            [SerializeField] public int m_Damage = 50; // надо private
            [SerializeField] private int m_GoldCost = 10;
            [SerializeField] private int m_ManaCost = 10;
            public int GoldCost => m_GoldCost;
            public int ManaCost => m_ManaCost;


            public void SetDamage(int damageIncrease)
            {
                m_Damage += damageIncrease;
            }
            public void Use()
            {
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    if (m_ImpactAreaAttackPrefab != null)
                    {
                        TDPlayer.Instance.ChangeMana(-m_ManaCost);
                        ImpactAreaAttack expl = Instantiate(m_ImpactAreaAttackPrefab, position, Quaternion.identity);
                        expl.SetProjectileProperties(m_Damage, m_DamageType);
                    }
                });
            }
            
        }

        [Serializable]
        public class FreezeAbility
        {
            [SerializeField] private float m_CoolDown = 5f;
            [SerializeField] public float m_Duration = 5; // надо private
            [SerializeField] private int m_GoldCost = 10;
            [SerializeField] private int m_ManaCost = 10;
            public int GoldCost => m_GoldCost;
            public int ManaCost => m_ManaCost;

            public void SetDuration(int duationIncrease)
            {
                m_Duration += duationIncrease;
            }
            public void Use()
            {
                void Slow(Enemy ship) // Slow all enemies
                {
                    ship.GetComponent<Ship>().HalfMaxLinearVelocity();
                }
                TDPlayer.Instance.ChangeMana(-m_ManaCost);
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

        public void UseFireAbility() => m_FireAbility.Use();
        public void UseFreezeAbility() => m_FreezeAbility.Use();

    }
}