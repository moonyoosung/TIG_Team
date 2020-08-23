using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_KeyFake : MonoBehaviour
{
    AudioSource audioPlayer;
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision coll)
    {
        audioPlayer.clip = MYS_SoundManager.Instance.SFX_Key;
        audioPlayer.volume = 0.05f;
        audioPlayer.Play();
    }
}
