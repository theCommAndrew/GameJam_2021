using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    private static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverScreen;
    public CameraFollow cameraFollow;
    private Player player; 

    //health bar stuff
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake() { }
    public void Start()
    {
        pauseMenuUI.SetActive(false);
        gameOverScreen.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();     
        cameraFollow.Setup(() => player.transform.position);
        
        player.updateHealth += updateHealthBar;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           (gameIsPaused ? (Action)Resume : Pause)();
        }

        if(Time.timeScale == 0 && !player.alive)
        {
            GameOver();
        }

        void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
    }


    private void updateHealthBar(object sender, Player.UpdateHealthEvent e){
        
        for(int i = 0; i < hearts.Length; i++){
            hearts[i].sprite = i < e.playerHealth ? fullHeart : emptyHeart;

            hearts[i].enabled = i < e.maxHealth;       
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}