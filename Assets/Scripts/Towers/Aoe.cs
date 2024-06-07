using Assembly_CSharp;
using Assets.ObjectPooling;
using Assets.Scripts.Bullets;
using Assets.Scripts.Enums;
using Assets.Scripts.InterfacesAndImplementations.Tower;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class Aoe : MonoBehaviour
    {
        public ITowerStats TowerStats { get; private set; } 
        public ITowerLevel TowerLevel { get; private set; }
        public ITowerShoot TowerShoot { get; private set; }
       

        public GameObject Explosion;
        public float detectionRadius;
        public ObjectPool explosionPool;

        public GoldManager goldManager;
        public GameObject upgradeCanvas;
        public TextMeshProUGUI upgradeCostText;
        private bool isCanvasActive = false;

        private void Start()
        {
            InitializeAoeTower(new TowerStats(), new TowerLevel(), new TowerShoot(), goldManager);
            explosionPool.Initialize(20);
            StartCoroutine(DetectMonsters());

            StartCoroutine(DetectMonsters());

            if (upgradeCanvas != null)
            {
                upgradeCanvas.SetActive(false);
            }

            UpdateLevelUpText();
        }

  

        public void InitializeAoeTower(ITowerStats towerStats, ITowerLevel towerLevel, ITowerShoot towerShoot, GoldManager goldManager)
        {
            TowerStats = towerStats;
            TowerLevel = towerLevel;
            TowerShoot = towerShoot;
            this.goldManager = goldManager;
            FireRates fireRates = new FireRates();
            TowerStats.Range = 80f;
            TowerStats.Damage = 40;
            TowerStats.FireRate = fireRates.GetFireRateAndAttackTimer("0.5bullets/second").Item1;
            TowerStats.AttackTimer = fireRates.GetFireRateAndAttackTimer("0.5bullets/second").Item2;
            TowerStats.DamageType = DamageType.Area;
            TowerStats.ArmorPenetration = 20 / 100f;
            TowerLevel.Level = 1;

            detectionRadius = TowerStats.Range;
        }

        /// <summary>
        /// detect monsters using raycast
        /// </summary>
        /// <returns></returns>
        IEnumerator DetectMonsters()
        {
            while (true)
            {
                if (Time.time >= TowerStats.AttackTimer)
                {
                    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, detectionRadius, Vector2.zero);
                    foreach (var hit in hits)
                    {
                        if (hit.collider.CompareTag("Monster"))
                        {
                            GameObject explosion = explosionPool.Get();
                            explosion.transform.position = transform.position;
                            explosion.SetActive(true);

                            Explosion aoeComponent = explosion.GetComponent<Explosion>();
                            aoeComponent.SetPool(explosionPool); 

                            BulletInfo bulletInfo = new BulletInfo
                            {
                                TargetTranform = hit.transform,
                                Damage = TowerStats.Damage,
                                Speed = 5f 
                            };
                            aoeComponent.InitializeBullet(bulletInfo);

                            TowerStats.AttackTimer = Time.time + 1f / TowerStats.FireRate;
                            break; 
                        }
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        void OnDrawGizmosSelected()
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }


        private void OnMouseDown()
        {
            Debug.Log("Test??");
            if (upgradeCanvas != null)
            {
                isCanvasActive = !isCanvasActive;
                upgradeCanvas.SetActive(isCanvasActive);
            }
        }
        public void ShowUpgradeMenu()
        {
            if (upgradeCanvas != null)
            {
                isCanvasActive = !isCanvasActive;
                upgradeCanvas.SetActive(isCanvasActive);

                if (upgradeCanvas.activeSelf)
                {
                    RectTransform rectTransform = upgradeCanvas.GetComponent<RectTransform>();
                    rectTransform.position = transform.position + new Vector3(0, rectTransform.sizeDelta.y * 1, 0);
                }
            }
        }

        public void LevelUpTower()
        {
            if (TowerLevel != null && TowerStats != null)
            {
                int requiredGold = GetUpgradeCost(TowerLevel.Level + 1);
                if (goldManager.TotalGold >= requiredGold)
                {
                    TowerLevel.LevelUpTower(goldManager.TotalGold, TowerStats);
                    goldManager.RemoveGold(requiredGold);
                    UpdateLevelUpText();
                }
            }
        }

        private int GetUpgradeCost(int level)
        {
            switch (level)
            {
                case 1: return 50;
                case 2: return 100;
                case 3: return 150;
                case 4: return 200;
                default: return int.MaxValue;
            }
        }

        private void UpdateLevelUpText()
        {
            if (upgradeCostText != null && TowerLevel != null)
            {
                int nextLevel = TowerLevel.Level + 1;
                if (nextLevel <= 4)
                {
                    upgradeCostText.text = "Upgrade Cost: " + GetUpgradeCost(nextLevel);
                }
                else
                {
                    upgradeCostText.text = "Max Level Reached";
                }
            }
        }


    }
}
