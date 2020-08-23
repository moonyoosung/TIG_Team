using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_Water : MonoBehaviour
{
    public static MYS_Water Instace;
    private void Awake()
    {
        Instace = this;
    }
    public GameObject waterInUI;
    private void Update()
    {
        if (transform.localPosition.y >= -1.5f )
        {
#if EDITOR
            waterInUI.SetActive(true);
            transform.GetComponent<MeshRenderer>().enabled = false;
#elif VR
            MYS_GameManager.Instance.OnPlayerDieWater();
#endif
        }    
    }
}
