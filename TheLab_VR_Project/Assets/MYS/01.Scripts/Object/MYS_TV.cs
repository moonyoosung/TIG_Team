using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MYS_TV : MonoBehaviour
{
    // 플레이어가 신호를 주면 동영상을 틀었다가 끄는 버튼
    public bool flag;
    VideoPlayer vp;
    public GameObject monitor;
    public GameObject monitor2;
    void Start()
    {
        vp = GetComponentInChildren<VideoPlayer>();
        monitor.SetActive(false);
        monitor2.SetActive(true);
    }

    void Update()
    {

    }
    public void ControlTV()
    {
        flag = !flag;
        if (flag)
        {
            monitor2.SetActive(false);
            monitor.SetActive(true);
            vp.Play();
        }
        else
        {
            vp.Stop();
            monitor.SetActive(false);
            monitor2.SetActive(true);
        }
    }
}
