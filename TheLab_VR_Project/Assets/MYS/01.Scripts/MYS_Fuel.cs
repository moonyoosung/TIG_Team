using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_Fuel : MonoBehaviour
{
    MYS_PlayerClick pc;
    GameObject hit;
    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<MYS_PlayerClick>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Fuel_Int"))
        {
            //게임오브젝트의 레이어를 바꾼다.
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
            MYS_Inventory.Instance.DeleteItemInven(gameObject);
            pc.PutDownGrapObj();
            hit = other.gameObject;
            transform.position = other.transform.position;
            transform.up = other.transform.forward;


            // 연료 주입 애니메이션 실행
            iTween.MoveTo(gameObject, iTween.Hash(
            "position", hit.transform.position,
            "speed", 1,
            "easetype", iTween.EaseType.linear,
            "oncompletetarget", gameObject,
            "oncomplete", "RotateFuel"));
        }
    }
    void RotateFuel()
    {
        iTween.RotateTo(gameObject, iTween.Hash(
        "rotation", new Vector3(90, 0, 0),
        "speed", 30,
        "easetype", iTween.EaseType.linear,
                "oncompletetarget", gameObject,
                "oncomplete", "OnCompleteFuelInit"));
    }
    void OnCompleteFuelInit()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
        "position", transform.position +transform.up*2f,
        "speed", 5,
        "easetype", iTween.EaseType.linear));
        GameObject TM = GameObject.FindGameObjectWithTag("TM");
        TM.GetComponent<MYS_TimeMachine>().fuel = true;
        Destroy(gameObject, 1f);
    }
}
