using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlock : MonoBehaviour
{
    public CheckBlocks cb;

    private void OnTriggerEnter(Collider other)
    {
        cb.Check(int.Parse(gameObject.name), true);
        cb.AllCheck();
    }


    private void OnTriggerExit(Collider other)
    {
        cb.Check(int.Parse(gameObject.name), false);
        cb.AllCheck();
    }
}
