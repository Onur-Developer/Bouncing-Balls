using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : WeaponsScript, IWeaponNeeded
{
   private Vector3 myMousePos;
   private GameObject followObj;
   private AmmoScript4 fw;
   private int followWall;
   public GameObject PausePanel;
   public byte bulletCount { get; set; }


   private void Start()
   {
      mysprite = GetComponent<SpriteRenderer>();
      gm = GameObject.FindWithTag("Main").GetComponent<GameManager>();
   }

   public void Shoot()
   {
      if (Ammos.transform.childCount == 0)
      {
         ınstantıateAmmo(ammoObject,gunPosition,myMousePos-transform.position,velocityPower);
         gm.changeWeapon = false;
         bulletCount--;
         gm.writeBullet();
      }
      else if(ammoObject.tag!="SpecialBullet")
      {
         fw = Ammos.GetChild(0).gameObject.GetComponent<AmmoScript4>();
         followObj = Instantiate(ammoObject, gunPosition);
         followObj.GetComponent<AmmoScript>().exposionTimer = 10;
         StartCoroutine(fakeUpdate());
         gm.changeWeapon = false;
         bulletCount--;
         gm.writeBullet();
         gm.au.PlayOneShot(gm.firesound);
      }
   }

   IEnumerator fakeUpdate()
   {
      while (fw.lr.positionCount>followWall && followObj!=null)
      {
         if (Time.timeScale != 0)
         {
            followObj.transform.position = fw.lr.GetPosition(followWall);
            followWall++;
         }
         yield return null;
      }
      Destroy(fw.gameObject);

      followWall = 0;
   }
   
   private void Update()
   {
      if (Time.timeScale != 0)
      {
         myMousePos = maincam.ScreenToWorldPoint(Input.mousePosition);
         Vector3 rotation = myMousePos - transform.position;

         float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

         transform.rotation = Quaternion.Euler(0, 0, rotz);

         if (Input.GetMouseButtonDown(0) && canFire && bulletCount>0)
         {
            Shoot();
         }
      }
      
      if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) && !PausePanel.activeSelf)
      {
         PausePanel.SetActive(true);
         Time.timeScale = 0;
      }
   }
}
