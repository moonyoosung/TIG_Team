using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public GameObject MainUI;
    public GameObject GameOverUI;
    public GameObject GameClearUI;

    void Start()
    {
        MainUI.SetActive(true);
        // 시작화면 사운드 재생

        GameOverUI.SetActive(false);
        GameClearUI.SetActive(false);
    }


    void Update()
    {

    }


}
