// Lexi Anderson
// Last modified: Dec 12, 2021
// CS 3410 Final Project
// SkidMarkRendererHandler -- Render skid marks upon drifting and braking

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarkRendererHandler : MonoBehaviour
{
    public TopDownCarController carController;
    TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //carController = GetComponent<TopDownCarController>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        // render trail if tires are skidding
        if (carController.IsTireSkidding(out float lateralVelocity, out bool isBraking))
            trailRenderer.emitting = true;
        else trailRenderer.emitting = false;
    }
}
