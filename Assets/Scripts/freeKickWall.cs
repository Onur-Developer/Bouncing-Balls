using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class freeKickWall : MonoBehaviour
{
    public GameObject otherWall;
    private float speed;
    private bool isRight;

    private void Awake()
    {
        int randomNumber = Random.Range(0, 7);
        speed = Random.Range(2, 11);
        instantiateOtherWall(randomNumber);
    }

    void instantiateOtherWall(int number)
    {
        float xValue = 0;
        for (int i = 0; i < number; i++)
        {
            xValue += 4.5f;
            GameObject newWall = Instantiate(otherWall, gameObject.transform);
            newWall.transform.localPosition = new Vector2(xValue, newWall.transform.position.y);
        }
    }

    private void Update()
    {
        if (!isRight)
         transform.Translate(Vector3.left*speed*Time.deltaTime);
        else
            transform.Translate(Vector3.right*speed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("BorderWalls"))
        {
            isRight = !isRight;
        }
    }
}