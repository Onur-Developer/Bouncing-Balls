using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public Transform Guns, Kale, goalKeeper;
    private byte activeChild;
    public bool changeWeapon = true;
    public GameObject[] spawnPoints;

    public GameObject[] spawnObjects;

    public delegate void Mydelegate();

    public event Mydelegate myFunc;

    private int spawnBorder;

    public AudioSource au;

    public Animator flyTextAnim, cameraAnim;

    public WeaponScript[] ws;
    public Text[] bulletText;

    public GameObject gameOverPanel;
    public Text gameOverScoreText, gainText, flyText;

    public AudioClip sound1, sound2, changeWeaponSound, firesound, failyellowsound, goalyellowsound;

    public Texture2D[] cursors;
    private Vector2 cursorHotspot;

    public Transform ammos;

    void changeCursor(byte cursor)
    {
        Cursor.SetCursor(cursors[cursor], cursorHotspot, CursorMode.Auto);
    }


    public void writeGainScore(int addscore)
    {
        gainText.text = "Current Gain Score: " + addscore;
    }

    public void flytext(int addscore)
    {
        flyText.text = "+" + addscore;
    }


    public void soundBall(byte value)
    {
        switch (value)
        {
            case 1:
                au.PlayOneShot(sound1);
                break;
            case 2:
                au.PlayOneShot(sound2);
                break;
        }
    }

    public void isFinishGame()
    {
        if (ws[0].bulletCount <= 0)
        {
            gameOverPanel.SetActive(true);
            gameOverScoreText.text = "Score: " + Score.ToString();
            Time.timeScale = 0;
        }
    }


    public void writeBullet()
    {
        for (int i = 0; i < ws.Length; i++)
        {
            bulletText[i].text = ws[i].bulletCount.ToString();
        }
    }

    void addBullet()
    {
        byte count = 3;
        for (int i = 0; i < ws.Length; i++)
        {
            ws[i].bulletCount = count;
            count--;
            if (i == 3)
                ws[i].bulletCount = 1;
        }
    }

    void reloadSpawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randi = Random.Range(0, spawnObjects.Length);
            Instantiate(spawnObjects[randi], spawnPoints[i].transform);
        }
    }

    void reloadPositions()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float xRandi = Random.Range(-8.5f, 8.5f);
            float yRandi = Random.Range(-3.5f, 2.8f);
            spawnPoints[i].transform.position = new Vector2(xRandi, yRandi);
        }
    }

    void deleteChilds()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject destroyObj = spawnPoints[i].transform.GetChild(0).gameObject;
            Destroy(destroyObj);
        }
    }

    void resetPosition()
    {
        Guns.transform.position = new Vector2(0, -4.43f);
        Guns.transform.rotation = quaternion.identity;
        Kale.transform.position = new Vector2(-0.48f, 4);
        Kale.transform.rotation = quaternion.identity;
        goalKeeper.transform.rotation=quaternion.identity;
        Transform[] ts ={ Guns, Kale, goalKeeper };
        for (int i = 0; i < ts.Length; i++)
        {
            if (ts[i].gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Destroy(ts[i].gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }

    void addSpawnerPoint()
    {
        if (spawnPoints.Length > spawnBorder)
        {
            spawnPoints[spawnBorder].SetActive(true);
            spawnBorder++;
        }
        else if (spawnPoints.Length == spawnBorder)
        {
            Kale.transform.position = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(0, 5f));
        }
    }

    private void Awake()
    {
        myFunc += reloadPositions;
        myFunc += reloadSpawn;
        myFunc += resetPosition;
        myFunc += addSpawnerPoint;
        myFunc += addBullet;
        myFunc += writeBullet;
    }

    private void Start()
    {
        myFunc();
        changeCursor(0);
        myFunc += deleteChilds;
        myFunc += () => flyTextAnim.SetTrigger("isFly");
        myFunc += () => cameraAnim.SetTrigger("isShake");
    }

    public void callMyFunc()
    {
        myFunc();
    }

    private void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0 && changeWeapon && Time.timeScale != 0)
        {
            Guns.GetChild(activeChild).gameObject.SetActive(false);
            activeChild++;
            if (Guns.transform.childCount == activeChild)
            {
                Guns.GetChild(0).gameObject.SetActive(true);
                activeChild = 0;
            }
            else
                Guns.GetChild(activeChild).gameObject.SetActive(true);

            au.PlayOneShot(changeWeaponSound);
            changeCursor(activeChild);
        }
        else if (scrollWheel < 0 && changeWeapon && Time.timeScale != 0)
        {
            Guns.GetChild(activeChild).gameObject.SetActive(false);
            activeChild--;
            if (activeChild == 255)
            {
                Guns.GetChild(Guns.transform.childCount - 1).gameObject.SetActive(true);
                activeChild = 3;
            }

            else
                Guns.GetChild(activeChild).gameObject.SetActive(true);

            au.PlayOneShot(changeWeaponSound);
            changeCursor(activeChild);
        }

        /*if (Input.GetKeyDown(KeyCode.Escape) && !PausePanel.activeSelf)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        } */
    }


    public int Score
    {
        get { return score; }
        set
        {
            score += value;
            scoreText.text = "Score: " + Score;
        }
    }
}