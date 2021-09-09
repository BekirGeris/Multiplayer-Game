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
    public override void OnConnectedToMaster() //photona baðlandýðýnda bu metot çalýþýr.
    {
        base.OnConnectedToMaster();
        Debug.Log("Photona baðlandý.");
        Debug.Log(PhotonNetwork.NickName + "Ýsimli oyuncu networke baðlandý...");
        ActiveGameSettingPanel();
    }
    public override void OnConnected() //internate baðlandýðýnda bu metot çalýþýr.
    {
        base.OnConnected();
        Debug.Log("Ýnternete baðlandý.");
    }
    public override void OnJoinedRoom() //bir odaya baðlandýðýnda bu metot çalýþýr.
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName + " adlý kiþi þu odaya baðlandý: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }
    public override void OnJoinRandomFailed(short returnCode, string message) //Bu metot eðer bir oda bulunamasza çalýþýr. 
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        Debug.Log("Oda Bulunamadý, Yeni Oda Kuruluyor...");
        CreateJoinRoom();
    }
    #endregion

    #region Unity Methods
    public void ActiveLoginPnel() //login panel aktif diðer paneller pasif
    {
        gameSettingsPanel.SetActive(false);
        createJoinRoomPanel.SetActive(false);
        joinRandomRoomPanel.SetActive(false);
        loginPanel.SetActive(true);
    } 
    public void ActiveGameSettingPanel() //GameSettings panel aktif diðer paneller pasif
    {
        loginPanel.SetActive(false);
        createJoinRoomPanel.SetActive(false);
        joinRandomRoomPanel.SetActive(false);
        gameSettingsPanel.SetActive(true);
    }
    public void ActiveCreateJoinRoomPanel() //Room panel aktif diðer paneller pasif
    {
        gameSettingsPanel.SetActive(false);
        loginPanel.SetActive(false);
        joinRandomRoomPanel.SetActive(false);
        createJoinRoomPanel.SetActive(true);
    }
    public void ActiveRandomJoinRoomPanel() //Join Random Panel aktif diðerleri pasif
    {
        gameSettingsPanel.SetActive(false);
        loginPanel.SetActive(false);
        createJoinRoomPanel.SetActive(false);
        joinRandomRoomPanel.SetActive(true);
        RasgeleOdayaBaglan();
    }
    public void OnGiris()  //giriþ butonuna basýldýðýnda bu metot çalýþýr.
    {
        Debug.Log("butona basýldý");
        PhotonNetwork.NickName = nameInputField.text;
        PhotonNetwork.ConnectUsingSettings(); //photona baðlanma komutu baðlma baþarýlý olursa true döner.
    } 
    public void OdaOlusturPanelineGit() //Oda oluþtur  butonun abasýldýðýnda bu metot çalýþýr.
    {
        ActiveCreateJoinRoomPanel();
    } 
    public void RandomRoomButtonClicked()  //random oda için butona basýldýðýnda bu metot çalýþýr.
    {
        ActiveRandomJoinRoomPanel();
    }
    public void OdaOlusturButonClicked() //oda bilgileri doldurulur ve oda oluþtur butonuna basýldýðýnda bu metot çalýþýr.
    {
        RoomOptions odaSettings = new RoomOptions();
        odaSettings.MaxPlayers = (byte)int.Parse(oyuncuSayisiInputField.text);
        PhotonNetwork.CreateRoom(odaIsmiInputField.text, odaSettings);
    }
    public void RasgeleOdayaBaglan()//bu metot içerisinde Rastgele bir odaya baðlanýlýr.
    {
        PhotonNetwork.JoinRandomRoom();
        randomRoomStatus.text = "Oda Aranýyor...";
    } 
    public void CreateJoinRoom() //Bu metot rastgele oda bulunamazsa çalýþtýrýlmak için yazýldý.
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
