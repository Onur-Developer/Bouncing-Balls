using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settingsScript : MonoBehaviour
{
    public Slider musicSlider;
    public AudioSource backgroundMusic;
    public AudioSource mainSound, ballSound;
    public Slider soundSlider;
    public GameObject PausePanel;


    public void changeSoundSlider()
    {
        mainSound.volume = soundSlider.value;
        ballSound.volume = soundSlider.value;
        PlayerPrefs.SetFloat("SoundValue",soundSlider.value);
    }


    public void changeMusicSlider()
    {
        backgroundMusic.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicValue", musicSlider.value);
    }

    void loadMusicValues()
    {
        if (PlayerPrefs.HasKey("MusicValue"))
        {
            backgroundMusic.volume = PlayerPrefs.GetFloat("MusicValue");
            musicSlider.value = PlayerPrefs.GetFloat("MusicValue");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicValue", 0.5f);
            backgroundMusic.volume = PlayerPrefs.GetFloat("MusicValue");
            musicSlider.value = PlayerPrefs.GetFloat("MusicValue");
        }

    }

    void loadSoundValues()
    {
        if (PlayerPrefs.HasKey("SoundValue"))
        {
            mainSound.volume = PlayerPrefs.GetFloat("SoundValue");
            ballSound.volume = PlayerPrefs.GetFloat("SoundValue");
            soundSlider.value=PlayerPrefs.GetFloat("SoundValue");
        }
        else
        {
            PlayerPrefs.SetFloat("SoundValue", 0.5f);
            mainSound.volume = PlayerPrefs.GetFloat("SoundValue");
            ballSound.volume = PlayerPrefs.GetFloat("SoundValue");
            soundSlider.value=PlayerPrefs.GetFloat("SoundValue");
        }
    }

    public void startButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ResumeButton()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void exitButton()
    {
        Application.Quit();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name=="GameScene")
        {
            loadMusicValues();
            loadSoundValues();
        }
        else
        { 
            loadMusicValues();
        }
    }
}