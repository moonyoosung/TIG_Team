using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_MoveShelves : MonoBehaviour
{
    public void MoveBack()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
        "position", transform.position + transform.forward * -0.7f,
        "speed", 0.5f,
        "easetype", iTween.EaseType.linear,
        "oncompletetarget", gameObject,
        "oncomplete", "NextMove"));
    }
    public void NextMove()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
        "position", transform.position + transform.right * 2.0f,
        "speed", 0.5f,
        "easetype", iTween.EaseType.linear));
    }
}
