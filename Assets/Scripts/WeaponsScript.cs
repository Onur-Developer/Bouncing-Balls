using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsScript : MonoBehaviour
{
    public float velocityPower;
    public GameObject ammoObject;
    public Transform gunPosition;
    public Transform Ammos;
    public bool canFire = true;

    public Camera maincam;

    public Sprite gunFull, gunEmpty;
    public SpriteRenderer mysprite;
    public GameManager gm;

    public void ChangeSprite()
    {
        if (canFire)
            mysprite.sprite = gunFull;
        else
            mysprite.sprite = gunEmpty;
    }

    public void ınstantıateAmmo(GameObject ammo, Transform gunPosition, Vector2 direction, float velocityPower)
    {
        GameObject myAmmo = Instantiate(ammo, gunPosition);
        if (myAmmo.tag != "SpecialBullet")
        {
            myAmmo.GetComponent<Rigidbody2D>().velocity =
                new Vector2(direction.x, direction.y).normalized * velocityPower;
        }
        myAmmo.transform.parent = Ammos;
        canFire = false;
        ChangeSprite();
        gm.au.PlayOneShot(gm.firesound);
    }
}

public interface IWeaponNeeded
{
    public void Shoot();
    public byte bulletCount { get; set; }
}