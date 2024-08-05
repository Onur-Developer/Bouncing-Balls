using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public GameManager gm;
    public WeaponScript wp;
    public int addScore;
    public AudioSource au;
    public byte soundbyte;
    public float exposionTimer=3;

    public virtual void Start()
    {
        wp = GameObject.FindWithTag("Player").GetComponent<WeaponScript>();
        gm = GameObject.FindWithTag("Main").GetComponent<GameManager>();
        Destroy(gameObject, exposionTimer);
    }

    private void OnDestroy()
    {
        wp.canFire = true;
        wp.ChangeSprite();
        gm.changeWeapon = true;
        gm.isFinishGame();
        if (soundbyte != 0)
            gm.soundBall(soundbyte);
        else
        {
            addScore = 1;
            gm.writeGainScore(addScore);
        }
    }

    public virtual void addRigidbody(Collision2D col)
    {
        col.gameObject.AddComponent<Rigidbody2D>();
        col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("GameController"))
        {
            GainScore();
        }
        else
        {
            addScore++;
            gm.writeGainScore(addScore);
            au.Play();
        }
    }

    public void GainScore()
    {
        gm.Score = addScore;
        gm.flytext(addScore);
        gm.callMyFunc();
        Destroy(gameObject);
    }
}