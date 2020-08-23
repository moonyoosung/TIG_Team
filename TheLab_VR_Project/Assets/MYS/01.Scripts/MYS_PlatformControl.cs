using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_PlatformControl : MonoBehaviour
{
    public GameObject editorCamera;
    public GameObject vrCamera;
    private void Awake()
    {
#if EDITOR
        editorCamera.SetActive(true);
        vrCamera.SetActive(false);
#elif VR
        editorCamera.SetActive(false);
        vrCamera.SetActive(true);
#endif
    }
}
