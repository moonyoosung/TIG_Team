using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYS_Clock : MonoBehaviour
{
    public static MYS_Clock Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Image timeImage;
    public Sprite[] timeYears;
    public bool findClock;

    //현재 년도를 저장할 변수 : 처음 시계가 발견된 년도 1987
    int currentYear = 1987;

    public void CheckTimer(int checkYear)
    {
        // 시계를 설치했던 시점에서 과거로 가면 안보이고
        if (currentYear > checkYear)
        {
            gameObject.SetActive(false);
        }
        else
        {
            // 미래로가면 보인다.
            gameObject.SetActive(true);
            // 해당 년도에 맞는 이미지로 바꾼다.
            int idx = 0;
            idx++;
            timeImage.sprite = timeYears[idx];
        }
    }
}
