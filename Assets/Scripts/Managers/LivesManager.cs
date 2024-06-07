using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets
{
    public class LivesManager : MonoBehaviour
    {
        public int startingLives = 20;
        private int currentLives;
        public TextMeshProUGUI livesText;
        public GameOverManager gameOverManager;

        void Start()
        {
            currentLives = startingLives;
            UpdateLivesUI();
        }

        public void LoseLife()
        {
            if (currentLives > 0)
            {
                currentLives--;
                UpdateLivesUI();
                if (currentLives <= 0)
                {
                    gameOverManager.GameOver();
                }
            }
        }

        private void UpdateLivesUI()
        {
            livesText.text = "Lives: " + currentLives;
        }

        public int GetLives()
        {
            return currentLives;
        }
       
    }
}
