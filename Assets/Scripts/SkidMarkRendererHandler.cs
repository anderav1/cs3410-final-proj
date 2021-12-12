using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarkRendererHandler : MonoBehaviour
{
    TopDownCarController carController;
    TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<TopDownCarController>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.IsTireSkidding(out float lateralVelocity, out bool isBraking))
            trailRenderer.emitting = true;
        else trailRenderer.emitting = false;
    }
}
