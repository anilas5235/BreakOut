using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public int currentLevel = 0 ;
    private BrickSpawner BrickSpawner;
    private Ball Ball;
    private int Fails ;
    private bool gamefinished = false;
    private TextMeshProUGUI LevelUI, FailsUI;
    private AudioSource levelaudio,failsound;
    public AudioClip finishsound;


    private void Awake()
    {
        Ball = FindObjectOfType<Ball>();
        BrickSpawner = GameObject.FindObjectOfType<BrickSpawner>();
        LevelUI = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        FailsUI = GameObject.Find("Fails").GetComponent<TextMeshProUGUI>();
        levelaudio = GameObject.Find("Globalsound").GetComponent<AudioSource>();
        failsound = GameObject.Find("failsound").GetComponent<AudioSource>();
    }

    private void Start()
    {
        currentLevel = 0;
        LoadLevel();
    }


    public void LoadLevel()
    {
        Ball.BallReset();
        print("currentLevel : "+currentLevel);
        BrickSpawner.SpawnBricks(currentLevel);
        LevelUI.text = "Level : " + currentLevel;
    }

    public void CheckLevelFinished()
    {
        if (FindObjectOfType<Brick>() == null) {print("level finished ");levelaudio.PlayOneShot(finishsound); }
    }

    public void InvokeCheckLevelFinished()
    {
        Invoke("CheckLevelFinished",0.2f);
    }

    public void FailHappend()
    {
        print("Fials +1 ");
        Fails++;
        FailsUI.text = "Fails : " + Fails;
        failsound.Play();
    }
}
