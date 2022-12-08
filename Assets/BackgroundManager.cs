using EasyParallax;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("PlayerObject (Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        float speed = playerObj.GetComponent<Rigidbody2D>().velocity.x;
        SetBackgroundSpeeds(0);
    }

    void SetBackgroundSpeeds(float speed)
    {
        PlayerPrefs.SetFloat("BackgroundNearestSpeed", 1f);
    }
}
