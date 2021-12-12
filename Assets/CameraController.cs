using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        float x = player.transform.position.x;
        float y = player.transform.position.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
