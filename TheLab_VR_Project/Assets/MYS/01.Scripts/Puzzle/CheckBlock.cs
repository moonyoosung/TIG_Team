using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlock : MonoBehaviour
{
    public CheckBlocks cb;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Block")
        {
            cb.Check(int.Parse(gameObject.name), 1);
            //cb.AllCheck();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            cb.Check(int.Parse(gameObject.name), 0);
            //cb.AllCheck();

        }
    }
}
