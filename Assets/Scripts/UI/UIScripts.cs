using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverScreen;
    public CameraFollow cameraFollow;
    private Player player; 

    //health bar stuff
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public void Start()
    {   
        pauseMenuUI.SetActive(false);
        gameOverScreen.SetActive(false);
        Cursor.visible = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();     
        cameraFollow.Setup(() => player.transform.position);
        
        player.updateHealth += updateHealthBar;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           (gameIsPaused ? (Action)resumeGame : pauseGame)();
        }

        if(Time.timeScale == 0 && !player.alive)
        {
            GameOver();
        }
    }

    private void updateHealthBar(object sender, Player.UpdateHealthEvent e){
        
        for(int i = 0; i < hearts.Length; i++){
            hearts[i].sprite = i < e.playerHealth ? fullHeart : emptyHeart;

            hearts[i].enabled = i < e.maxHealth;       
        }
    }

    public void pauseGame()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void resumeGame()
    {
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void quitGame(){
        Application.Quit();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}