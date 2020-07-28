using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    //텔레포트했을때 도착하는 타겟 지점
    public Transform teleportTarget;

    public void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (col.gameObject.tag == "Teleport")
        {
            print(gameObject.name + "텔레포팅!!");
            transform.position = teleportTarget.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    // 이 스크립트는 플레이어에 붙어있음.
    // 원래는 teleportStartPoint에 스크립트를 붙여, 플레이어가 닿으면 순간이동이 되게 하려고 했으나,
    // 플레이어 자체가 cc로 되있어서 그런지, OnCollisionEnter나 OnTriggerEnter를 사용해서 플레이어와 닿게 하려고 했으나,
    // 아예 두개의 기능들이 다 되지 않음.
    // 그래서 현재는 플레이어에만 붙여있는데,
    // 지금 현재는 텔레포트가 한번밖에 되지 않음.. 어떻게 해야하지ㅠㅠㅠㅠ 고민이 필요해...ㅠㅠㅠㅠ


}
