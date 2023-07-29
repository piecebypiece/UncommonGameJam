using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text StatusText;

    [Header("Menu")]
    public InputField roomInput, NickNameInput;

    [Header("WaitingRoom")]
    public GameObject waitingRoomPanel;
    public Text waitingRoomInfoText;
    public Button startButton;
    public Button readyButton;

    public Text ListText;
    public Text[] ChatText;
    //public InputField ChatInput;


    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Screen.SetResolution(960, 540, false);
    }

    void Start() => Connect();

    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        //waitingRoomInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 / " + PhotonNetwork.CountOfPlayers + "접속";
    }


    public void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("서버접속완료");
        //PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }



    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause) => print("연결끊김");



    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby() => print("로비접속완료");



    public void CreateRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.CreateRoom(roomInput.text == "" ? "Room" + Random.Range(0, 100) : roomInput.text, new RoomOptions { MaxPlayers = 10 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinRoom(roomInput.text);
    }

    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 2 }, null);

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnCreatedRoom() => print("방만들기완료");

    public override void OnJoinedRoom()
    {
        print("방참가완료");
        waitingRoomPanel.SetActive(true);
        // 마스터 클라이언트인지 확인
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.gameObject.SetActive(true);
            readyButton.gameObject.SetActive(false);
            SetReadyStatus(true);
        }
        else
        {
            startButton.gameObject.SetActive(false);
            readyButton.gameObject.SetActive(true);
        }
        RoomRenewal();
        //ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";
        //PhotonNetwork.LoadLevel("GameScene");
        // 테스트용
        // PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        // Debug.Log("플레이어 생성");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기실패");

    public override void OnJoinRoomFailed(short returnCode, string message) => print("방참가실패");

    public override void OnJoinRandomFailed(short returnCode, string message) => print("방랜덤참가실패");

    #region waitingRoom

    public void SetReadyStatus(bool isReady)
    {
        // 플레이어의 준비 상태를 설정
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "IsReady", isReady } });
    }

    public bool CheckAllPlayersReady()
    {
        // 모든 플레이어가 준비 상태인지 확인
        foreach (var player in PhotonNetwork.PlayerList)
        {
            object isReady;
            if (player.CustomProperties.TryGetValue("IsReady", out isReady))
            {
                if (!(bool)isReady)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void StartGame()
    {
        //if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        //{
        //    // 2명 이하 일 때 단순 리턴, 추후 시각적으로 정보 제공 필요
        //    Debug.Log("인원 부족");
        //    return;
        //}

        if (CheckAllPlayersReady())
        {
            PhotonNetwork.LoadLevel("Meetiing");
            // PhotonNetwork.LoadLevel("Test GameScene");
        }
    }

    public void StartTest()
    {
        PhotonNetwork.LoadLevel("TestGameScene");
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        //ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        //ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        waitingRoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
    }
    #endregion


    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        {
            print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            print("방 개수 : " + PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? : " + PhotonNetwork.InLobby);
            print("연결됐는지? : " + PhotonNetwork.IsConnected);
        }
    }
}