// Lexi Anderson
// Last modified: Dec 12, 2021
// CS 3410 Final Project
// TopDownCarController -- control car movement and collisions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float minTurnSpeedFactor = 8f;
    public float maxRoadSpeed = 20.0f;
    public float offroadSpeed = 15.0f;
    public float driftSpeed = 4.0f;

    private float maxSpeed;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityVsUp = 0;

    private Rigidbody2D rb2d;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = maxRoadSpeed;

        rb2d = GetComponent<Rigidbody2D>();
        rotationAngle = rb2d.rotation;
    }
    
    void FixedUpdate()
    {
        ApplyEngineForce();
        AdjustOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        // calculate forward movement in relation to velocity direction
        velocityVsUp = Vector2.Dot(transform.up, rb2d.velocity);

        // limit forward speed to max speed
        if (velocityVsUp > maxSpeed && accelerationInput > 0) return;

        // limit reverse speed to 0.5 * max speed
        if (velocityVsUp > -maxSpeed * 0.5f && accelerationInput < 0) return;

        // limit speed in any direction
        if (rb2d.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0) return;

        // apply drag if there is no acceleration input
        if (accelerationInput == 0)
            rb2d.drag = Mathf.Lerp(rb2d.drag, 3.0f, Time.fixedDeltaTime * 3);
        else rb2d.drag = 0;

        Vector2 engineForce = transform.up * accelerationInput * accelerationFactor;

        // apply force to rigidbody to push car forward
        rb2d.AddForce(engineForce, ForceMode2D.Force);
    }

    void AdjustOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb2d.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb2d.velocity, transform.right);

        rb2d.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    void ApplySteering()
    {
        // limit car's ability to turn at low speed
        float minTurnSpeed = (rb2d.velocity.magnitude / minTurnSpeedFactor);
        minTurnSpeed = Mathf.Clamp01(minTurnSpeed);

        rotationAngle -= steeringInput * turnFactor * minTurnSpeed;  // update car rotation angle
        rb2d.MoveRotation(rotationAngle);  // apply rotation
    }

    // Return how fast the car is moving sideways
    private float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, rb2d.velocity);
    }

    public bool IsTireSkidding(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        // check if braking while moving forward
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        // check if drifting
        if (Mathf.Abs(lateralVelocity) > driftSpeed) return true;

        return false;
    }

    // Get the magnitude of the car's velocity
    public float GetVelocityMag()
    {
        return rb2d.velocity.magnitude;
    }

    public void SetInputVector(Vector2 input)
    {
        steeringInput = input.x;
        accelerationInput = input.y;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Track"))
        {
            //rb2d.drag += offroadDrag;
            maxSpeed = offroadSpeed;
            Debug.Log("Going off road");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Track"))
        {
            //rb2d.drag -= offroadDrag;
            maxSpeed = maxRoadSpeed;
            Debug.Log("Got back on road");

            // clear warning message
            gameController.DisplayWarningText(false);
        } else if (collision.CompareTag("FinishLine"))
        {
            gameController.OnCrossFinishLine();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            gameController.DisplayWarningText(true);
        }
    }
}
