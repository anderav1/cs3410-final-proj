using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    private Toggle toggleButton;
    private Image bkgd;
    private Image checkmark;
    private int toggle = 0;  // audio on

    public GameObject player;
    private AudioSource[] sfx;
    private AudioSource bkgdMusic;

    // Start is called before the first frame update
    void Start()
    {
        toggleButton = GetComponent<Toggle>();
        bkgd = toggleButton.transform.Find("Background").GetComponent<Image>();
        checkmark = bkgd.transform.Find("Checkmark").GetComponent<Image>();

        sfx = player.GetComponentsInChildren<AudioSource>();
        bkgdMusic = GameObject.FindObjectOfType<AudioSource>();
    }
    
    public void OnValueChanged()
    {
        if (toggleButton.isOn)
        {
            toggle = 1;  // audio off
            bkgd.enabled = false;
            checkmark.enabled = true;
            foreach (AudioSource sound in sfx) sound.mute = true;
            bkgdMusic.mute = true;
        } else
        {
            toggle = 0;  // audio on
            bkgd.enabled = true;
            checkmark.enabled = false;
            foreach (AudioSource sound in sfx) sound.mute = false;
            bkgdMusic.mute = false;
        }
    }
}
