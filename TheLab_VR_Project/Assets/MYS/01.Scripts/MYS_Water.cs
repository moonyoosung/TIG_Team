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
        if (transform.localPosition.y >= -3.0f && transform.localPosition.y<=-2.5f)
        {
            waterInUI.SetActive(true);
            transform.GetComponent<MeshRenderer>().enabled = false;
        }    
    }
}
