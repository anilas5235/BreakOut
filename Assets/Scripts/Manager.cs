using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour
{
    public int currentLevel = 0 ;
    private BrickSpawner BrickSpawner;
    private Ball Ball;
    private Bar player;
    public int Fails ;
    private TextMeshProUGUI LevelUI, FailsUI;
    private AudioSource levelaudio,failsound;
    public AudioClip finishsound;
    public static Manager Instance;
    private SpriteRenderer spriteRendererPlayer, spriteRendererBall;
    public enum GameState
    {
        Menu   = 0, Playing = 1, Win = 2
    }
    public GameState currentGameState;


    private void Awake()
    {
        Instance = this;
        Ball = FindObjectOfType<Ball>();
        spriteRendererBall = Ball.gameObject.GetComponentInChildren<SpriteRenderer>();
        BrickSpawner = FindObjectOfType<BrickSpawner>();
        LevelUI = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        FailsUI = GameObject.Find("Fails").GetComponent<TextMeshProUGUI>();
        levelaudio = GameObject.Find("Backgroundmusic").GetComponent<AudioSource>();
        failsound = GameObject.Find("failsound").GetComponent<AudioSource>();
        player = FindObjectOfType<Bar>();
        spriteRendererPlayer = player.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentGameState = GameState.Menu;
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
            currentGameState = GameState.Win;
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

    public void TriggerReckingBallPower() { StartCoroutine(ReckingBallPowerUp()); }
    private IEnumerator ReckingBallPowerUp()
    {
        Transform T = Ball.gameObject.transform;
        T.localScale *= 2f;
        yield return new WaitForSeconds(20f);
        T.localScale *= 1/2f;
    }

    public void TriggerSpeedBallPower() { StartCoroutine(SpeedBallPowerUp()); }
    private int speedBallStackCounter;
    private IEnumerator SpeedBallPowerUp()
    {
        speedBallStackCounter++;
        Ball.speedMultiplier *= 1.5f;
        spriteRendererBall.color = new Color(197 / 255f, 1, 165 / 255f, 1);
        yield return new WaitForSeconds(20f);
        speedBallStackCounter--;
        if (speedBallStackCounter<1 ) { spriteRendererBall.color = Color.red;}
        Ball.speedMultiplier *= 1/1.5f;
        if (Ball.startSpeed < 1) { Ball.speedMultiplier = 1; }
    }
    
    public void TriggerSpeedPlayerPower() { StartCoroutine(SpeedPlayerPowerUp()); }
    private int speedPlayerStackCounter;
    private IEnumerator SpeedPlayerPowerUp()
    {
        speedPlayerStackCounter++;
        player.moveSpeed *= 1.3f;
        spriteRendererPlayer.color = new Color(102 / 255f, 245 / 255f, 248/255f, 1);
        yield return new WaitForSeconds(20f);
        speedPlayerStackCounter--;
        if(speedPlayerStackCounter < 1 && giantPlayerStackCounter < 1){spriteRendererPlayer.color = Color.white;}
        player.moveSpeed *= 1/1.3f;
    }
    
    public void TriggerGiantPlayerPower() { StartCoroutine(GiantPlayerPowerUp()); }
    private int giantPlayerStackCounter;
    private IEnumerator GiantPlayerPowerUp()
    {
        giantPlayerStackCounter++;
        Transform T = player.gameObject.transform;
        spriteRendererPlayer.color = new Color(184 / 255f, 66 / 255f, 1, 1);
        T.localScale *= 2f;
        yield return new WaitForSeconds(20f);
        giantPlayerStackCounter--;
        if(speedPlayerStackCounter < 1 && giantPlayerStackCounter < 1){spriteRendererPlayer.color = Color.white;}
        T.localScale *= 1/2f;
    }
}
