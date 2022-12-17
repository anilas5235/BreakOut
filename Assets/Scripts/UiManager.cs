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
    [SerializeField] private GameObject startScreenController, audioOptionsController, levelSelectController, winScreenController;
    [SerializeField] private AudioSource musicSource, sfxSource, othersSource;
    [SerializeField] private GameObject[] levelOfLevelSelect;
    [SerializeField] private TextMeshProUGUI winMessage;
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
        LoadOptions();
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
            case Menu.AudioOptions: saveOptions(); audioOptionsController.SetActive(false); break;
            case Menu.LevelSelect: levelSelectController.SetActive(false); break;
            case Menu.Pause: break;
            case Menu.Win: winScreenController.SetActive(false); break;
            default: print(" Error, GameState does not exist");break;
        }
        CurrMenu = newState;
        
        switch (CurrMenu)
        {
            case Menu.None: ; break;
            case Menu.StartScreen: startScreenController.SetActive(true); Manager.Instance.CurrentGameState = Manager.GameState.menu;LoadLevel(0); break;
            case Menu.AudioOptions: audioOptionsController.SetActive(true);Manager.Instance.CurrentGameState = Manager.GameState.menu; break;
            case Menu.LevelSelect: levelSelectController.SetActive(true);  UpdateLevelSelectStars();
                Manager.Instance.CurrentGameState = Manager.GameState.menu; break;
            case Menu.Pause: break;
            case Menu.Win: winScreenController.SetActive(true); winMessage.text = "You achieved " + (5 - Manager.Instance.Fails) + " Stars"; 
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
    }
    
    public void ChangeToLevelSelection()
    {
        MenuChangeState(Menu.LevelSelect);
    }


    private void UpdateSoundOptions()
    {
        musicSource.volume = music.value;
        sfxSource.volume = sfx.value;
        othersSource.volume = others.value;
    }

    private void LoadOptions()
    {
        musicSource.volume = music.value = OptionsSave.faderSettingsAudio[0]; ;
        sfxSource.volume = sfx.value = OptionsSave.faderSettingsAudio[1];
        othersSource.volume = others.value = OptionsSave.faderSettingsAudio[2];
    }

    private void saveOptions()
    {
        OptionsSave.faderSettingsAudio[0] =music.value;
        OptionsSave.faderSettingsAudio[1]=sfx.value ;
        OptionsSave.faderSettingsAudio[2]=others.value ;
    }

    public void LoadLevel(int level)
    {
        if(level ==0){Manager.Instance.LoadLevel(level); }
        else
        {
            MenuChangeState(Menu.None);
            Manager.Instance.LoadLevel(level);
            Manager.Instance.CurrentGameState = Manager.GameState.playing;
        }

    }

    public void UpdateStars(int level, int stars)
    {
        if(stars < 1){return;}

        GameObject targetLevel;
        if (level == -1) { targetLevel = winScreenController; }
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
}
