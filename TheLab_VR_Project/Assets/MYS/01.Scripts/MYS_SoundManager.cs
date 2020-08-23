using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 사운드 클립 관리 스크립트

public class MYS_SoundManager : MonoBehaviour
{
    public static MYS_SoundManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [Header("- AudioClip")]
    public AudioClip BGM_Start;
    public AudioClip BGM_Main;
    public AudioClip BGM_GameClear;
    public AudioClip BGM_GameOver;
    public AudioClip SFX_Succes;
    public AudioClip SFX_Fail;
    public AudioClip SFX_Keypad;
    public AudioClip SFX_Door;
    public AudioClip SFX_Fuel;
    public AudioClip SFX_TMActive;
    public AudioClip SFX_TMActiveFail;
    public AudioClip SFX_LeverOn;
    public AudioClip SFX_LeverOff;
    public AudioClip SFX_Doll;
    public AudioClip SFX_BookPage;
    public AudioClip SFX_Key;
    public AudioClip SFX_KeyOpen;
    public AudioClip SFX_FuelDown;
    public AudioClip SFX_PuzzleBlock;
    public AudioClip SFX_PuzzleOpen;
    public AudioClip SFX_WrtieLetter;
    public AudioClip SFX_SlidePuzzle;
    public AudioClip SFX_moveTransPuzzle;
    public AudioClip SFX_MoveShelve;
    public AudioClip SFX_TVButton;
    public AudioClip SFX_Water;

    [Header("- AudioSource")]
    public AudioSource BGMPlayer;
    public AudioSource KeypadPlayer;
    public AudioSource DoorPlayer;
    public AudioSource Fuel_InPlayer;
    public AudioSource TMPlayer;
    public AudioSource BookPlayer;
    public AudioSource TVButtonPlayer;

    // 백그라운드 사운드 재생
    public void OnPlayerStartSound()
    {
        BGMPlayer.clip = BGM_Start;
        BGMPlayer.loop = true;
        BGMPlayer.volume = 0.1f;
        BGMPlayer.Play();
    }
    public void OnPlayBGMSound()
    {
        BGMPlayer.clip = BGM_Main;
        BGMPlayer.loop = true;
        BGMPlayer.volume = 0.05f;
        BGMPlayer.Play();
    }
    // 게임 클리어 사운드 재생
    public void OnPlayerGameClearSound()
    {
        BGMPlayer.clip = BGM_GameClear;
        BGMPlayer.loop = true;
        BGMPlayer.volume = 0.1f;
        BGMPlayer.Play();
    }
    // 게임 오버 사운드 재생
    public void OnPlayerGameOverSound()
    {
        BGMPlayer.clip = BGM_GameOver;
        BGMPlayer.loop = true;
        BGMPlayer.volume = 0.1f;
        BGMPlayer.Play();
    }
    //키패드 사운드 재생
    public void OnPlayerKeypadSound()
    {
        KeypadPlayer.clip = SFX_Keypad;
        KeypadPlayer.volume = 0.3f;
        KeypadPlayer.Play();
    }
    public void OnPlayerKeypadSuccess()
    {
        KeypadPlayer.clip = SFX_Succes;
        KeypadPlayer.volume = 0.3f;
        KeypadPlayer.Play();
    }
    public void OnPlayerKeypadFail()
    {
        KeypadPlayer.clip = SFX_Fail;
        KeypadPlayer.volume = 0.3f;
        KeypadPlayer.Play();
    }
    // 문 열고 닫히는 소리
    public void OnPlayerDoorSound()
    {
        DoorPlayer.clip = SFX_Door;
        DoorPlayer.volume = 0.5f;
        DoorPlayer.Play();
    }
    // 연료 넣는 소리
    public void OnPlayerFuelInSound()
    {
        Fuel_InPlayer.clip = SFX_Fuel;
        Fuel_InPlayer.volume = 0.1f;
        Fuel_InPlayer.Play();
    }
    // 타임머신 가동될 때 재생되는 소리
    public void OnPlayerTMActive()
    {
        TMPlayer.clip = SFX_TMActive;
        TMPlayer.volume = 0.5f;
        TMPlayer.Play();
    }
    // 타임머신 연료 없이 가동될 때 소리
    public void OnPlayerTMActiveFail()
    {
        TMPlayer.clip = SFX_TMActiveFail;
        TMPlayer.volume = 0.5f;
        TMPlayer.Play();
    }
    public void OnPlayerBookPage()
    {
        BookPlayer.clip = SFX_BookPage;
        BookPlayer.volume = 0.05f;
        BookPlayer.Play();
    }
    public void OnPlayerTVButton()
    {
        TVButtonPlayer.clip = SFX_TVButton;
        TVButtonPlayer.volume = 0.3f;
        TVButtonPlayer.Play();
    }
}
