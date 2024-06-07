using Assets.Scripts.Bullets;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.InterfacesAndImplementations.Tower;
using System.Collections;
using Assets.ObjectPooling;
using Assembly_CSharp;
using TMPro;
namespace Assets.Scripts.Towers
{
    public class Archer : MonoBehaviour
    {
        public ITowerStats TowerStats { get; private set; } 
        public ITowerLevel TowerLevel { get; private set; }
        public ITowerShoot TowerShoot { get; private set; }

        public GameObject Arrow;
        public float detectionRadius;
        public ObjectPool arrowPool;

        public GoldManager goldManager;
        public GameObject upgradeCanvas;
        public TextMeshProUGUI upgradeCostText;
        private bool isCanvasActive = false;

        private void Start()
        {
            
            InitializeArcherTower(new TowerStats(), new TowerLevel(), new TowerShoot(), goldManager);
            arrowPool.Initialize(20);
            StartCoroutine(DetectMonsters());
            if (upgradeCanvas != null)
            {
                upgradeCanvas.SetActive(false);
            }

            UpdateLevelUpText();
        }
       
        public void InitializeArcherTower(ITowerStats towerStats, ITowerLevel towerLevel, ITowerShoot towerShoot, GoldManager goldManager)
        {
            TowerStats = towerStats;
            TowerLevel = towerLevel;
            TowerShoot = towerShoot;
            this.goldManager = goldManager;
            FireRates fireRates = new FireRates();
            TowerStats.Range = 120f; 
            TowerStats.Damage = 20;
            TowerStats.FireRate = fireRates.GetFireRateAndAttackTimer("2bullets/second").Item1;
            TowerStats.AttackTimer = fireRates.GetFireRateAndAttackTimer("2bullets/second").Item2;
            TowerStats.DamageType = DamageType.Physical;
            TowerStats.ArmorPenetration = 20 / 100f;
            TowerStats.MagicPenetration = 0;
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
                            GameObject arrow = arrowPool.Get();
                            arrow.transform.position = transform.position;
                            arrow.SetActive(true);

                            Arrow arrowComponent = arrow.GetComponent<Arrow>();
                            arrowComponent.SetPool(arrowPool); 

                            BulletInfo bulletInfo = new BulletInfo
                            {
                                TargetTranform = hit.transform,
                                Damage = TowerStats.Damage,
                                Speed = 5f  
                            };
                            arrowComponent.InitializeBullet(bulletInfo);
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
                var t = goldManager.GetGold();
                int requiredGold = GetUpgradeCost(TowerLevel.Level + 1);
                if (goldManager.GetGold() >= requiredGold)
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
