using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //음악플레이어
    public AudioSource myAudio;

    public AudioClip mainAudio;
    public AudioClip puzzleSuccess;
    public AudioClip puzzleFail;
    public AudioClip gameStart;
    public AudioClip gameOver;
    public AudioClip gameClear;





    void Awake()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        myAudio.Play();
      
    }


}
