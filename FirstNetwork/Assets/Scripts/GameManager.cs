using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject gameCarecterPrefab;  
    public GameObject plane;
    [Header("Game UI Objects")]
    public GameObject exitGame;
    public GameObject settingsPanel;
    public GameObject infoPanel;
    public Text numberOfPlayer;
    public Text numberOfMaxPlayer;
    void Start()
    {
        UpdateInfoPlane();
        Vector3 planeScale = plane.transform.localScale;
        float x = Random.Range(-planeScale.x * 5, planeScale.x * 5);
        float z = Random.Range(-planeScale.z * 5, planeScale.z * 5);
        if (PhotonNetwork.IsConnectedAndReady) //oyunu oynamak için herþey hazýrsa if içerine girilir.
        {
            PhotonNetwork.Instantiate(gameCarecterPrefab.name, new Vector3(x, planeScale.y, z), Quaternion.identity);
        }
    }
    void Update()
    {
        OnKeyUpEscapeControl();
        OnKeyTabControl();
    }
    public void LogOutGameRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("LobiScene");
    }
    public void OnKeyUpEscapeControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf == true)
            {
                settingsPanel.SetActive(false);
            }
            else
            {
                settingsPanel.SetActive(true);
            }
        }
    }
    public void OnKeyTabControl()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            infoPanel.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            infoPanel.SetActive(false);
        }
    }
    public void UpdateInfoPlane()
    {
        numberOfPlayer.text = numberOfPlayer.text + PhotonNetwork.CurrentRoom.PlayerCount;
        numberOfMaxPlayer.text = numberOfMaxPlayer.text + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    #region
    public override void OnPlayerEnteredRoom(Player newPlayer) //odaya biri girerse bu metot çalýþýr.
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdateInfoPlane();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer) //odaya biri çýkarsa bu metot çalýþýr.
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdateInfoPlane();
    }
    #endregion
}
