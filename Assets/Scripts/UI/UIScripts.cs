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

    public void Awake()
    {
        pauseMenuUI.SetActive(false);
        gameOverScreen.SetActive(false);
        Cursor.visible = false;
        startTime();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cameraFollow.Setup(() => player.transform.position);

        player.updateHealth += updateHealthBar;
        player.playerDeath += gameOver;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            (gameIsPaused ? (Action)resumeGame : pauseGame)();
        }
    }

    private void updateHealthBar(object sender, Player.UpdateHealthEvent e)
    {
        print("Updating healthbar");
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < e.playerHealth ? fullHeart : emptyHeart;

            hearts[i].enabled = i < e.maxHealth;
        }
    }

    public void pauseGame()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        stopTime();
    }

    public void resumeGame()
    {
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        startTime();
    }

    public void restartGame()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name);
        gameOverScreen.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void gameOver(object sender, System.EventArgs e)
    {
        stopTime();
        gameOverScreen.SetActive(true);
    }

    private void stopTime()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
    }

    private void startTime()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
    }
}