using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript3 : AmmoScript
{
   private Transform Player;

   private void Awake()
   {
      Player = GameObject.FindWithTag("MainPlayer").transform;
   }

   public override void OnCollisionEnter2D(Collision2D col)
   {
      Player.transform.position = new Vector2(col.contacts[0].point.x, col.contacts[0].point.y);
      Destroy(gameObject);
   }
}
