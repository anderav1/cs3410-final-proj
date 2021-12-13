// Lexi Anderson
// Last modified Dec 12, 2021
// CS 3410 Final Project -- Obstacle Course Race
// CarSFXHandler -- handle car sound effects such as engine, tire screech

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarSFXHandler : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource tireScreechAudio;
    public AudioSource engineAudio;

    private TopDownCarController carController;

    private float targetEnginePitch = 0.5f;
    private float tireScreechPitch = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<TopDownCarController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateTireScreechSFX();
    }

    void UpdateEngineSFX()
    {
        float velocityMag = carController.GetVelocityMag();

        float targetEngineVolume = velocityMag * 0.05f;
        targetEngineVolume = Mathf.Clamp(targetEngineVolume, 0.2f, 1.0f);

        engineAudio.volume = Mathf.Lerp(engineAudio.volume, targetEngineVolume, Time.deltaTime * 10); // increase volume over time

        // vary engine pitch by speed
        targetEnginePitch = velocityMag * 0.2f;
        targetEnginePitch = Mathf.Clamp(targetEnginePitch, 0.5f, 2f);
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, targetEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTireScreechSFX()
    {
        if (carController.IsTireSkidding(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tireScreechAudio.volume = Mathf.Lerp(tireScreechAudio.volume, 0.8f, Time.deltaTime * 10);
                tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
            } else  // drifting
            {
                tireScreechAudio.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        } else
        {
            // fade out screech SFX
            tireScreechAudio.volume = Mathf.Lerp(tireScreechAudio.volume, 0, Time.deltaTime * 10);
        }
    }
}
