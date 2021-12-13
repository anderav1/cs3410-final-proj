// Lexi Anderson
// MenuController -- Handle button click events in Menu scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject helpPanel;

    private void Start()
    {
        helpPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Track1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Switch between panels

    public void HowToPlay()
    {
        menuPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        helpPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
