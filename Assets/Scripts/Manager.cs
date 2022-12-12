using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    private Vector2[] Levels = new Vector2[20];
    public int currentLevel = -1;
    private BrickSpawner BrickSpawner;
    private Ball Ball;
    private int Fails = -1;
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
        Levels[0] = new Vector2(5, 5);
        for (int i = 1; i < Levels.Length; i++)
        {
            Levels[i] = Levels[i - 1] + new Vector2(Random.Range(0,3), Random.Range(0,3) );
        }
        NextLevel(); FailHappend();
    }


    public void NextLevel()
    {
        if(gamefinished){return;}
        Ball.BallReset();
        currentLevel++;
        levelaudio.PlayOneShot(finishsound);
        if (currentLevel >= Levels.Length) { gamefinished = true; } print("currentLevel : "+currentLevel);
        BrickSpawner.SpawnBricks( (int)Levels[currentLevel].x,  (int)Levels[currentLevel].y);
        LevelUI.text = "Level : " + currentLevel;
    }

    public void CheckLevelFinished()
    {
        if (FindObjectOfType<Brick>() == null) {print("level finished "); NextLevel(); }
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
