using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] private AudioMixer mainAudioMixer;
    [SerializeField] private Slider main, music, sfx, others;
    [SerializeField] private GameObject startScreenController, audioOptionsController, levelSelectController, winScreenController, pauseScreenController,tipsController;
    [SerializeField] private GameObject[] levelOfLevelSelect;
    [SerializeField] private TextMeshProUGUI winMessage, pauseMessage;
    [SerializeField] public OptionsSave OptionsSave;
    private BrickSpawner BrickSpawner;
    public enum Menu
    {
        None = 0,
        StartScreen =1,
        AudioOptions =2,
        LevelSelect =3,
        Pause =4,
        Win=5,
    }

    public Menu CurrMenu;

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        BrickSpawner = BrickSpawner.Instance;
        MenuChangeState(Menu.StartScreen);
        LoadFromSaveText();
        LoadOptionsFormObject();
        UpdateSoundOptions();
    }


    // Update is called once per frame
    private  void Update()
    {
        switch (CurrMenu)
        {
            case Menu.None: break;
            case Menu.StartScreen: break;
            case Menu.AudioOptions: UpdateSoundOptions(); break;
            case Menu.LevelSelect: break;
            case Menu.Pause: break;
            case Menu.Win: break;
            default: print(" Error, GameState does not exist");break;
        }
    }

    public void MenuChangeState(Menu newState)
    {
        switch (CurrMenu)
        {
            case Menu.None: break;
            case Menu.StartScreen: startScreenController.SetActive(false);  break;
            case Menu.AudioOptions: SaveOptionsInObject(); audioOptionsController.SetActive(false); break;
            case Menu.LevelSelect: levelSelectController.SetActive(false); break;
            case Menu.Pause: pauseScreenController.SetActive(false); break;
            case Menu.Win: winScreenController.SetActive(false); break;
            default: print(" Error, GameState does not exist");break;
        }
        CurrMenu = newState;
        
        switch (CurrMenu)
        {
            case Menu.None: ; break;
            
            case Menu.StartScreen: startScreenController.SetActive(true); Manager.Instance.currentGameState = Manager.GameState.Menu;LoadLevel(0); break;
            
            case Menu.AudioOptions: audioOptionsController.SetActive(true);Manager.Instance.currentGameState = Manager.GameState.Menu; break;
            
            case Menu.LevelSelect: levelSelectController.SetActive(true);  UpdateLevelSelectStars(); Manager.Instance.currentGameState = Manager.GameState.Menu; break;
            
            case Menu.Pause: pauseScreenController.SetActive(true); pauseMessage.text = "You are about to achieve " +(5 - Manager.Instance.Fails) + " Star(s)";
                UpdateStars(-2,(5 - Manager.Instance.Fails));break;
            
            case Menu.Win: winScreenController.SetActive(true); winMessage.text = "You achieved " + (5 - Manager.Instance.Fails) + " Star(s)"; 
                UpdateStars(-1,(5 - Manager.Instance.Fails));
                break;
            
            default: print(" Error, GameState does not exist");break;
        }
    }

    public void ChangeToAudioOptions()
    {
        MenuChangeState(Menu.AudioOptions);
    }
    
    public void ChangeStartScreen()
    {
        MenuChangeState(Menu.StartScreen);
        Time.timeScale = 1;
    }
    
    public void ChangeToLevelSelection()
    {
        MenuChangeState(Menu.LevelSelect);
    }


    private void UpdateSoundOptions()
    {
        mainAudioMixer.SetFloat("Master_Volume", main.value);
        mainAudioMixer.SetFloat("SFX_Volume", sfx.value);
        mainAudioMixer.SetFloat("Music_Volume", music.value);
        mainAudioMixer.SetFloat("Other_Volume", others.value);
    }

    private void LoadOptionsFormObject()
    {
       main.value =  OptionsSave.faderSettingsAudio[0];
       sfx.value =  OptionsSave.faderSettingsAudio[1];
       music.value = OptionsSave.faderSettingsAudio[2]; 
       others.value =  OptionsSave.faderSettingsAudio[3];
    }

    private void SaveOptionsInObject()
    {
        mainAudioMixer.GetFloat("Master_Volume", out OptionsSave.faderSettingsAudio[0]) ;
        mainAudioMixer.GetFloat("SFX_Volume", out OptionsSave.faderSettingsAudio[1]) ;
        mainAudioMixer.GetFloat("Music_Volume", out OptionsSave.faderSettingsAudio[2]) ;
        mainAudioMixer.GetFloat("Other_Volume", out OptionsSave.faderSettingsAudio[3]) ;
    }

    public void LoadLevel(int level)
    {
        if(level ==0){Manager.Instance.LoadLevel(level); }
        else
        {
            MenuChangeState(Menu.None);
            Manager.Instance.LoadLevel(level);
            Manager.Instance.currentGameState = Manager.GameState.Playing;
        }

    }

    public void UpdateStars(int level, int stars)
    {
        if(stars < 0){return;}

        GameObject targetLevel;
        if (level == -1) { targetLevel = winScreenController; }
        else if (level == -2) { targetLevel = pauseScreenController; }
        else if(level > 0){targetLevel = levelOfLevelSelect[level-1];}
        else {return; }

        targetLevel.transform.GetChild(1).gameObject.SetActive(stars > 0);
        targetLevel.transform.GetChild(2).gameObject.SetActive(stars > 1);
        targetLevel.transform.GetChild(3).gameObject.SetActive(stars > 2);
        targetLevel.transform.GetChild(4).gameObject.SetActive(stars > 3);
        targetLevel.transform.GetChild(5).gameObject.SetActive(stars > 4);
    }

    private void UpdateLevelSelectStars()
    {
        for (int i = 0; i < levelOfLevelSelect.Length ; i++)
        {
            UpdateStars(i+1,BrickSpawner._levels[i+1].numberOfAchievedStars);
        }
    }

    public void RetryTheLevel()
    {
        LoadLevel(Manager.Instance.currentLevel);
    }

    public void CloseGame()
    {
        for (int i = 0; i < levelOfLevelSelect.Length; i++)
        {
            SaveSystem.instance.GetActiveSave().achievedStarsInLevels[i] = BrickSpawner.Instance._levels[i].numberOfAchievedStars;
        }
        for (int i = 0; i < OptionsSave.faderSettingsAudio.Length; i++)
        {
            SaveSystem.instance.GetActiveSave().audioOptions[i] = OptionsSave.faderSettingsAudio[i];
        }
        Application.Quit();
    }

    private void LoadFromSaveText()
    {
        for (int i = 0; i < levelOfLevelSelect.Length; i++)
        {
             BrickSpawner.Instance._levels[i].numberOfAchievedStars = SaveSystem.instance.GetActiveSave().achievedStarsInLevels[i];
        }
        for (int i = 0; i < OptionsSave.faderSettingsAudio.Length; i++)
        {
             OptionsSave.faderSettingsAudio[i] =SaveSystem.instance.GetActiveSave().audioOptions[i];
        }
    }

    public void ToggleTips(bool newState) { tipsController.SetActive(newState); }
    
    public void RestAllStars()
    {
        BrickSpawner.Instance.RestAllStarsData();
        MenuChangeState(Menu.LevelSelect);
    }

}
