using System.Collections;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int currentLevel = 0 ;
    private BrickSpawner BrickSpawner;
    private Ball Ball;
    public int Fails ;
    private TextMeshProUGUI LevelUI, FailsUI;
    private AudioSource levelaudio,failsound;
    public AudioClip finishsound;
    public static Manager Instance;
    public enum GameState
    {
        menu   = 0,
        playing = 1,
        win     = 2
    }
    public GameState CurrentGameState;


    private void Awake()
    {
        Instance = this;
        Ball = FindObjectOfType<Ball>();
        BrickSpawner = FindObjectOfType<BrickSpawner>();
        LevelUI = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        FailsUI = GameObject.Find("Fails").GetComponent<TextMeshProUGUI>();
        levelaudio = GameObject.Find("Backgroundmusic").GetComponent<AudioSource>();
        failsound = GameObject.Find("failsound").GetComponent<AudioSource>();
    }

    private void Start()
    {
        CurrentGameState = GameState.menu;
        FailsUI.text = "Fails : " + Fails;
    }


    public void LoadLevel(int level)
    {
        currentLevel = level;
        Fails = 0;FailsUI.text = "Fails : " + Fails; 
        Ball.BallReset(); Ball.PlayerReset();
        print("currentLevel : "+currentLevel);
        BrickSpawner.SpawnBricks(currentLevel);
        LevelUI.text = "Level : " + currentLevel;
    }

    public void CheckLevelFinished()
    {
        if (FindObjectOfType<Brick>() == null)
        {
            print("level finished ");
            levelaudio.PlayOneShot(finishsound);Ball.BallReset();
            UiManager.Instance.MenuChangeState(UiManager.Menu.Win);
            if(5-Fails > BrickSpawner._levels[currentLevel].numberOfAchievedStars)
            { BrickSpawner.Instance.UpdateStarsInData(currentLevel, 5-Fails);}
            CurrentGameState = GameState.win;
        }
    }

    public void InvokeCheckLevelFinished()
    {
        Invoke("CheckLevelFinished",0.2f);
    }

    public void FailHappend()
    {
        print("Fails +1 ");
        Fails++;
        FailsUI.text = "Fails : " + Fails;
        failsound.Play();
    }

    public IEnumerator ReckingBallPowerUp()
    {
        Ball.transform.localScale *= 2;
        yield return new WaitForSeconds(10f);
        Ball.transform.localScale *= 1/2f;
    }

    
}
