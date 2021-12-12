using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TopDownCarController carController;
    public Text warningText;
    public Text timerText;
    float timer = 0f;

    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        warningText.text = "";
        timerText.text = TimeToString(timer);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = TimeToString(timer);
    }

    public void DisplayWarningText(bool display)
    {
        if (display) warningText.text = "Warning: Too far off track. Head back.";
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
}
