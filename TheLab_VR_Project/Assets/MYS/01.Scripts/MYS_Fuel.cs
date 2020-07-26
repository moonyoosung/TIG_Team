using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_Fuel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Fuel"))
        {
            //게임오브젝트의 레이어를 바꾼다.
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
            transform.eulerAngles = Vector3.zero;
            transform.position = other.transform.position;
            GameObject TM = GameObject.FindGameObjectWithTag("TM");
            TM.GetComponent<MYS_TimeMachine>().fuel = true;
            MYS_Inventory.Instance.DeleteItemInven(gameObject);
        }
    }
}
