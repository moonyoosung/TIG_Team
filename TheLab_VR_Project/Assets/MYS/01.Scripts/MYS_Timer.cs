using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MYS_Timer : MonoBehaviour
{
    public bool timerActive;
    public TextMeshPro min;
    public TextMeshPro sec;
    float currentTime;
    int timeMin;
    int timeSec;

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
            timeSec = (int)Mathf.Round(currentTime);
            if(timeSec >= 60)
            {
                timeMin++;
                currentTime = 0;
            }
            string s_min = timeMin.ToString();
            string s_sec = timeSec.ToString();
            min.SetText(s_min);
            sec.SetText(s_sec);
            if (timeMin >= 30)
            {
                MYS_GameManager.Instance.OnPlayerDieTimeOver();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            timeMin = 30;
        }
        
    }
}
