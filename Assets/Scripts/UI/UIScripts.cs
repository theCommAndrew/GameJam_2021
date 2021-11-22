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
    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public void Awake()
    {
        pauseMenuUI.SetActive(false);
        gameOverScreen.SetActive(false);
        Cursor.visible = false;
        startTime();

        Player.updateHealth += (playerHealth, maxHealth) => updateHealthBar(playerHealth, maxHealth);
        Player.playerDeath += gameOver;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            (gameIsPaused ? (Action)resumeGame : pauseGame)();
        }
    }

    private void updateHealthBar(int playerHealth, int maxHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = i < playerHealth ? fullHeart : emptyHeart;

            hearts[i].enabled = i < maxHealth;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    public IEnumerator delayDeath()
    {
        yield return new WaitForSeconds(3);
    }
}