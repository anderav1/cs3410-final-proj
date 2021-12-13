// Lexi Anderson
// Last modified Dec 12, 2021
// CS 3410 Final Project -- Obstacle Course Race
// MusicManager -- Keep background music playing through scene changes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
