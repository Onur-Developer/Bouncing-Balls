using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalkeeperScript : MonoBehaviour
{
    private float speed = 3.5f;
    private float xMin = -1.5f;
    private float xMax = 1.5f;
    private float currentvalue;

    private void Update()
    {
        transform.localPosition =
                new Vector2(Mathf.PingPong(Time.time * speed, xMax - xMin) + xMin, -1);
    }
}