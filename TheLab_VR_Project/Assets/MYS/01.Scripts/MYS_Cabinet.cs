using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_Cabinet : MonoBehaviour
{
    Vector3 orizinPos;
    Vector3 targetPos;
    public bool keyopen;
    bool open;
    // Start is called before the first frame update
    void Start()
    {
        orizinPos = transform.position;
        targetPos = transform.position + transform.right * 2f;
    }
    public void MoveCabinet()
    {
        if (!open)
        {
            iTween.MoveTo(gameObject, iTween.Hash(
                            "position", targetPos,
                            "speed", 1,
                            "easetype", iTween.EaseType.linear));
        }
        else
        {
            iTween.MoveTo(gameObject, iTween.Hash(
                            "position", orizinPos,
                            "speed", 1,
                            "easetype", iTween.EaseType.linear));
        }
        open = !open;
    }

}
