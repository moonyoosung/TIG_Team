using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneToLoad;

    public GameObject MainUI;
    public GameObject GameOverUI;
    public GameObject GameClearUI;

    public SoundManager SoundMngr;

    AudioSource myAudio;
    void Start()
    {
        MainUI.SetActive(true);
        GameOverUI.SetActive(false);
        GameClearUI.SetActive(false);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("MYS_WorkScene");
        myAudio = SoundMngr.GetComponent<AudioSource>();
        myAudio.Play();
    }

    void Update()
    {

        //1. 게임이 오버되면 게임오버 UI로 전환이 필요
        //   게임오버 UI만 true, 나머지는 false
        //   Sound Play
        myAudio = SoundMngr.GetComponent<AudioSource>();

        // 2. 게임 클리어 되면 게임 클리어 UI로 전환이 필요
        //    게임클리어 UI만 true, 나머지는 false
        //   Sound Play
    }


}
