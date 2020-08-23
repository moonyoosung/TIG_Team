using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos_PuzzleIndex : MonoBehaviour
{
    public int value = 1;
    public MYS_PlayerClick pc;
    public AudioSource[] audios;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PosPuzzle")
        {            
            //MYS_Inventory.Instance.DeleteItemInven(gameObject);
            pc.PutDownGrapObj();
            transform.position = other.transform.position;
            transform.up = other.transform.up;
            transform.forward = other.transform.forward;
            //print("인형"+other.gameObject.name);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Doll")
        {
            // 만약 오디오 소스1번 부터 검사하여 재생중이라면 다음 순서에 재생
            for (int i = 0; i < audios.Length; i++)
            {
                if (!audios[i].isPlaying)
                {
                    audios[i].clip = MYS_SoundManager.Instance.SFX_Doll;
                    audios[i].volume = 0.01f;
                    audios[i].Play();
                    break;
                }
            }
        }
    }
}
