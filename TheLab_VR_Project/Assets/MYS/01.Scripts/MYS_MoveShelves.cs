using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_MoveShelves : MonoBehaviour
{
    AudioSource audioPlayer;
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    public void MoveBack()
    {
        audioPlayer.clip = MYS_SoundManager.Instance.SFX_MoveShelve;
        audioPlayer.volume = 0.5f;
        audioPlayer.Play();

        iTween.MoveTo(gameObject, iTween.Hash(
        "position", transform.position + transform.forward * -0.7f,
        "speed", 0.5f,
        "easetype", iTween.EaseType.linear,
        "oncompletetarget", gameObject,
        "oncomplete", "NextMove"));
    }
    public void NextMove()
    {
        audioPlayer.clip = MYS_SoundManager.Instance.SFX_MoveShelve;
        audioPlayer.volume = 0.5f;
        audioPlayer.Play();

        iTween.MoveTo(gameObject, iTween.Hash(
        "position", transform.position + transform.right * 2.0f,
        "speed", 0.5f,
        "easetype", iTween.EaseType.linear));
    }
}
