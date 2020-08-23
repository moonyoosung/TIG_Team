using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_TVButton : MonoBehaviour
{
    Vector3 orizinPos;
    MYS_TV tv;
    private void Start()
    {
        orizinPos = transform.position;
        tv = GetComponentInParent<MYS_TV>();
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.transform.tag.Contains("Player"))
        {
            iTween.MoveTo(gameObject, iTween.Hash(
            "position", transform.position + transform.forward * -0.01f,
            "speed", 0.5f,
            "easetype", iTween.EaseType.linear,
            "oncompletetarget", gameObject,
            "oncomplete", "returnButtonAnim"));
        }
    }
    public void TVButton()
    {

        iTween.MoveTo(gameObject, iTween.Hash(
        "position", transform.position + transform.forward * -0.01f,
        "speed", 0.5f,
        "easetype", iTween.EaseType.linear,
        "oncompletetarget", gameObject,
        "oncomplete", "returnButtonAnim"));

    }
    public void returnButtonAnim()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
"position", orizinPos,
"speed", 0.5f,
"easetype", iTween.EaseType.linear,
"oncompletetarget", gameObject,
"oncomplete", "OnCompleteButtonAnim"));
    }
    public void OnCompleteButtonAnim()
    {
        tv.ControlTV();
    }
}
