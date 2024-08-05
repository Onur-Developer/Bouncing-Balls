using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript4 : AmmoScript
{
    public Camera maincam;
    private Vector2 mousepos;
    private Rigidbody2D rb;
    public LineRenderer lr;
    private int currentpos = 0;
    private bool isStop;

    public override void Start()
    {
        wp = GameObject.FindWithTag("Player").GetComponent<WeaponScript>();
        gm = GameObject.FindWithTag("Main").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        maincam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (!isStop)
        {
            mousepos = maincam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.MoveTowards(transform.position, mousepos, 4 * Time.deltaTime);
            lr.positionCount = currentpos + 1;
            lr.SetPosition(currentpos, transform.position);
            currentpos++;
            if(transform.position== new Vector3(mousepos.x,mousepos.y,0))
                Destroy(gameObject);
        }
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("GameController"))
        {
            gm.au.PlayOneShot(gm.goalyellowsound);
            isStop = true;
            GetComponent<SpriteRenderer>().enabled = false;
            rb.simulated = false;
            wp.canFire = true;
            wp.ChangeSprite();
            gm.changeWeapon = true;
        }
        else if (!col.gameObject.CompareTag("BorderWalls"))
        {
            gm.au.PlayOneShot(gm.failyellowsound);
            Destroy(gameObject);
        }
    }
}