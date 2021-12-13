// Lexi Anderson
// Last modified: Dec 12, 2021
// CS 3410 Final Project
// CarInputHandler -- Receive user input from directional keys

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    TopDownCarController carController;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<TopDownCarController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool gameOver = gameController.IsGameOver();
        Vector2 input = Vector2.zero;
        if (!gameOver)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
        }
        carController.SetInputVector(input);
    }
}
