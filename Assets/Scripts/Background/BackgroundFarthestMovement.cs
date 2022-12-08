using EasyParallax;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFarthestMovement : MonoBehaviour
{
    private float speed;
    private GameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.Find("PlayerObject(Clone)");
    }

    private void Update()
    {
        if (playerObj == null)
            playerObj = GameObject.Find("PlayerObject(Clone)");
        if (GameObject.Find("PlayerObject(Clone)"))
        {
            //Save the current position, so we can edit it
            var newPosition = transform.position;
            speed = playerObj.GetComponent<Rigidbody2D>().velocity.normalized.x;
            speed *= 0.3f;
            //Move the position along the x axis by an amount that depends on the
            //defined speed and the deltaTime, so we can get a framerate independent movement
            newPosition.x -= speed * Time.deltaTime;
            //Update our position
            transform.position = newPosition;
        }
    }
}
