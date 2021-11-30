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

    public void Awake()
    {
        /*
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UI");
        if(objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        */
        pauseMenuUI.SetActive(false);
        gameOverScreen.SetActive(false);
        Cursor.visible = false;
        startTime();
        

        Player.playerDeath += gameOver;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            (gameIsPaused ? (Action)resumeGame : pauseGame)();
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
        SceneManager.LoadScene(1);
        pauseMenuUI.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void quitGame()
    {
        SceneManager.LoadScene(0);
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