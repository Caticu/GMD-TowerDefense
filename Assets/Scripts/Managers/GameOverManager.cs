using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets
{
    public class GameOverManager : MonoBehaviour
    {
        public GameObject gameOverCanvas; 
        public TextMeshProUGUI livesText;
        public AudioSource backgroundMusic;
        void Start()
        {
            gameOverCanvas.SetActive(false); 
        }

        public void GameOver()
        {
            livesText.text = "Game Over";
            gameOverCanvas.SetActive(true); 
            Time.timeScale = 0f;
            if (GameManager.Instance.backgroundMusic != null)
            {
                GameManager.Instance.backgroundMusic.Stop();
            }
        }

        public void GoToMenu()
        {
            GameManager.Instance.GoToMenu();
        }

        public void RestartGame()
        {
            GameManager.Instance.RestartGame();
        }
    }
}
