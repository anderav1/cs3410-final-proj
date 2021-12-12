using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEmissionHandler : MonoBehaviour
{
    private float emissionRate = 0;

    TopDownCarController carController;
    ParticleSystem smokeParticles;
    ParticleSystem.EmissionModule emission;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<TopDownCarController>();
        smokeParticles = GetComponent<ParticleSystem>();
        emission = smokeParticles.emission;
        emission.rateOverTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // reduce particles over time
        emissionRate = Mathf.Lerp(emissionRate, 0, Time.deltaTime * 5);
        emission.rateOverTime = emissionRate;

        if (carController.IsTireSkidding(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking) emissionRate = 30;  // braking produces a lot of smoke
            else emissionRate = Mathf.Abs(lateralVelocity) * 2;  // more drift = more smoke
        }
    }
}
