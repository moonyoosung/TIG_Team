using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_GameManager : MonoBehaviour
{
    public static MYS_GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject player;
    public GameObject StartUI;
    public GameObject OverUI;
    public GameObject ClearUI;

    public GameObject DieWaterMessage;
    public GameObject DieTimeOverMessage;
    public GameObject DieDoctor;

    public void OnPlayerDieWater()
    {
        MYS_SoundManager.Instance.OnPlayerGameOverSound();
        // 플레이어를 처음 위치로
        player.transform.position = new Vector3(-30, 1, 0);
        player.GetComponent<MYS_PlayerClick>().UI.SetActive(true);
        OverUI.SetActive(true);
        DieWaterMessage.SetActive(true);
    }
    public void OnPlayerDieTimeOver()
    {
        MYS_SoundManager.Instance.OnPlayerGameOverSound();
        // 플레이어를 처음 위치로
        player.transform.position = new Vector3(-30, 1, 0);
        player.GetComponent<MYS_PlayerClick>().UI.SetActive(true);
        OverUI.SetActive(true);
        DieTimeOverMessage.SetActive(true);
    }
    public void OnPlayerDieDoctor()
    {
        MYS_SoundManager.Instance.OnPlayerGameOverSound();
        // 플레이어를 처음 위치로
        player.transform.position = new Vector3(-30, 1, 0);
        player.GetComponent<MYS_PlayerClick>().UI.SetActive(true);
        OverUI.SetActive(true);
        DieDoctor.SetActive(true);
    }
    public void OnPlayerClear()
    {
        MYS_SoundManager.Instance.OnPlayerGameClearSound();
        // 플레이어를 처음 위치로
        player.transform.position = new Vector3(-30, 1, 0);
        player.GetComponent<MYS_PlayerClick>().UI.SetActive(true);
        ClearUI.SetActive(true);
    }
}
