using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class RoundManager : MonoBehaviour
    {
        public int currentRound = 0;
        public Button startRoundButton;
        public MonsterManager monsterManager;
        public float delayBetweenWaves = 10.0f;

        void Start()
        {
            startRoundButton.onClick.AddListener(StartRound);
        }

        public void StartRound()
        {
            currentRound++;
            StartCoroutine(SpawnMonsters());
            startRoundButton.interactable = false;
        }

        /// <summary>
        /// Start an infinite loop 
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnMonsters()
        {
            int countPerWave = 1;

            while (true)
            {
                for (int count = 0; count < countPerWave; count++)
                {
                    monsterManager.SpawnWolf();
                    monsterManager.SpawnSnake();
                    if (count < countPerWave - 1)
                    {
                        yield return new WaitForSeconds(1);
                    }
                }

               // add 1 minion to each wave
                countPerWave += 1;

                // Wait
                yield return new WaitForSeconds(delayBetweenWaves);
            }
        }

     

        public void OnRoundComplete()
        {
            startRoundButton.interactable = true;
        }
    }
}
