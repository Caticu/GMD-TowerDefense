using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets
{
    /// <summary>
    /// Singleton pattern
    /// used for changing scenes
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public AudioSource backgroundMusic;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                        go.AddComponent<AudioSource>(); 
                    }
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
                backgroundMusic = GetComponent<AudioSource>(); 
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (backgroundMusic != null && !backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
            }
        }

        public void GoToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Game"); 
        }
    }
}
