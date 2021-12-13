// Lexi Anderson
// Last modified: Dec 12, 2021
// CS 3410 Final Project
// GameController -- Run timer, end game, manage UI input

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TopDownCarController carController;
    public CarSFXHandler sfxHandler;
    public Text warningText;
    public Text timerText;
    float timer = 0f;
    public Text winText;
    public GameObject endGamePanel;
    public Button replayButton;
    public Button menuButton;
    public Button quitButton;

    bool startedRace = false;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        warningText.text = "";
        timerText.text = TimeToString(timer);
        winText.text = "";
        endGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            timer += Time.deltaTime;
            timerText.text = TimeToString(timer);
        }
    }

    public void DisplayWarningText(bool display)
    {
        if (display && !gameOver) warningText.text = "Warning: Too far off track. Head back.";
        else warningText.text = "";
    }

    // Format time string
    private string TimeToString(float time)
    {
        int min = (int)time / 60;
        int sec = (int)time % 60;
        float fraction = time * 100;
        fraction %= 100;

        string timeStr = String.Format("{0:00}:{1:00}:{2:00}", min, sec, fraction);
        return timeStr;
    }

    // Finish line will be crossed twice: once at the beginning of the race, and once at the end
    public void OnCrossFinishLine()
    {
        if (!startedRace)
        {
            startedRace = true;
            return;
        } else  // finished race
        {
            gameOver = true;
            winText.text = "You finished the race!\nYour time: " + TimeToString(timer);
            endGamePanel.SetActive(true);
        }
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void OnReplayButtonPress()
    {
        SceneManager.LoadScene("Track1");
    }

    public void OnMenuButtonPress()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
