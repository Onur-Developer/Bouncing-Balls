using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript2 : AmmoScript
{
    public Sprite barrierBlue;


    public override void addRigidbody(Collision2D col)
    {
        base.addRigidbody(col);
        col.gameObject.GetComponent<Rigidbody2D>().gravityScale = .5f;
        if (col.gameObject.layer == 4)
            col.gameObject.GetComponent<SpriteRenderer>().color=Color.blue;
        else if(col.gameObject.layer==1)
            col.gameObject.GetComponent<SpriteRenderer>().sprite = barrierBlue;


    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null && !col.gameObject.CompareTag("BorderWalls"))
        {
            addRigidbody(col);
            Destroy(gameObject);
        }
    }
}