using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    [Header("Input Fields")]
    public InputField nameInputField;
    public InputField odaIsmiInputField;
    public InputField oyuncuSayisiInputField;
    [Header("Login Panel")]
    public GameObject loginPanel;
    public GameObject[] loginPanelGameObects;
    [Header("Game Settings Panel")]
    public GameObject gameSettingsPanel;
    public GameObject[] gameSettingsPanelGameObjects;
    [Header("Create Join Room Panel")]
    public GameObject createJoinRoomPanel;
    public GameObject[] createJoinRoomPanelGameObjects;
    [Header("Join Random Room Panel")]
    public GameObject joinRandomRoomPanel;
    public GameObject[] joinRandomRoomPanelGameObjects;
    public Text randomRoomStatus;

    void Start()
    {
        ActiveLoginPnel();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        
    }

    #region PhotonCallBacks
    public override void OnConnectedToMaster() //photona ba�land���nda bu metot �al���r.
    {
        base.OnConnectedToMaster();
        Debug.Log("Photona ba�land�.");
        Debug.Log(PhotonNetwork.NickName + "�simli oyuncu networke ba�land�...");
        ActiveGameSettingPanel();
    }
    public override void OnConnected() //internate ba�land���nda bu metot �al���r.
    {
        base.OnConnected();
        Debug.Log("�nternete ba�land�.");
    }
    public override void OnJoinedRoom() //bir odaya ba�land���nda bu metot �al���r.
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName + " adl� ki�i �u odaya ba�land�: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }
    public override void OnJoinRandomFailed(short returnCode, string message) //Bu metot e�er bir oda bulunamasza �al���r. 
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        Debug.Log("Oda Bulunamad�, Yeni Oda Kuruluyor...");
        CreateJoinRoom();
    }
    #endregion

    #region Unity Methods
    public void ActiveLoginPnel() //login panel aktif di�er paneller pasif
    {
        gameSettingsPanel.SetActive(false);
        createJoinRoomPanel.SetActive(false);
        joinRandomRoomPanel.SetActive(false);
        loginPanel.SetActive(true);
    } 
    public void ActiveGameSettingPanel() //GameSettings panel aktif di�er paneller pasif
    {
        loginPanel.SetActive(false);
        createJoinRoomPanel.SetActive(false);
        joinRandomRoomPanel.SetActive(false);
        gameSettingsPanel.SetActive(true);
    }
    public void ActiveCreateJoinRoomPanel() //Room panel aktif di�er paneller pasif
    {
        gameSettingsPanel.SetActive(false);
        loginPanel.SetActive(false);
        joinRandomRoomPanel.SetActive(false);
        createJoinRoomPanel.SetActive(true);
    }
    public void ActiveRandomJoinRoomPanel() //Join Random Panel aktif di�erleri pasif
    {
        gameSettingsPanel.SetActive(false);
        loginPanel.SetActive(false);
        createJoinRoomPanel.SetActive(false);
        joinRandomRoomPanel.SetActive(true);
        RasgeleOdayaBaglan();
    }
    public void OnGiris()  //giri� butonuna bas�ld���nda bu metot �al���r.
    {
        Debug.Log("butona bas�ld�");
        PhotonNetwork.NickName = nameInputField.text;
        PhotonNetwork.ConnectUsingSettings(); //photona ba�lanma komutu ba�lma ba�ar�l� olursa true d�ner.
    } 
    public void OdaOlusturPanelineGit() //Oda olu�tur  butonun abas�ld���nda bu metot �al���r.
    {
        ActiveCreateJoinRoomPanel();
    } 
    public void RandomRoomButtonClicked()  //random oda i�in butona bas�ld���nda bu metot �al���r.
    {
        ActiveRandomJoinRoomPanel();
    }
    public void OdaOlusturButonClicked() //oda bilgileri doldurulur ve oda olu�tur butonuna bas�ld���nda bu metot �al���r.
    {
        RoomOptions odaSettings = new RoomOptions();
        odaSettings.MaxPlayers = (byte)int.Parse(oyuncuSayisiInputField.text);
        PhotonNetwork.CreateRoom(odaIsmiInputField.text, odaSettings);
    }
    public void RasgeleOdayaBaglan()//bu metot i�erisinde Rastgele bir odaya ba�lan�l�r.
    {
        PhotonNetwork.JoinRandomRoom();
        randomRoomStatus.text = "Oda Aran�yor...";
    } 
    public void CreateJoinRoom() //Bu metot rastgele oda bulunamazsa �al��t�r�lmak i�in yaz�ld�.
    {
        string randomRoomName = "Room" + Random.Range(10, 100);
        RoomOptions randomOdaSettings = new RoomOptions();
        randomOdaSettings.MaxPlayers = 10;
        randomOdaSettings.IsOpen = true;
        randomOdaSettings.IsVisible = true;
        PhotonNetwork.CreateRoom(randomRoomName, randomOdaSettings);
    }
    #endregion
}
