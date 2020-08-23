using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYS_Lever : MonoBehaviour
{
    public Image led;
    public Transform lever;
    float angleX;
    public bool index;
    MYS_TimePuzzle TP;
    bool value;
    AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        led = GetComponentInChildren<Image>();
        lever = transform.GetChild(1);
        angleX = -45;
        angleX = Mathf.Clamp(angleX, -45, 45);
        lever.transform.localEulerAngles = new Vector3(angleX, 0, 0);
        TP = GetComponentInParent<MYS_TimePuzzle>();
        TP.InitTable(int.Parse(transform.name), value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCheckLever()
    {
        print(lever.transform.localEulerAngles);
       
        if(angleX < 0)
        {
            print("레버올림");
            angleX = 45;
            led.color = Color.red;
            value = true;
            audioPlayer.clip = MYS_SoundManager.Instance.SFX_LeverOn;
            audioPlayer.volume = 0.3f;
            audioPlayer.Play();
        }
        else if(angleX > 0)
        {
            angleX = -45;
            led.color = Color.white;
            value = false;
            audioPlayer.clip = MYS_SoundManager.Instance.SFX_LeverOff;
            audioPlayer.volume = 0.3f;
            audioPlayer.Play();
        }
        TP.InitTable(int.Parse(transform.name), value);
        lever.transform.localEulerAngles = new Vector3(angleX, 0, 0);

    }
}
