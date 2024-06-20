using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("DisconnectPanel")]
    public GameObject DisconnectPanel;
    public InputField NicknameInput;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public GameObject InitGameBtn, RollBtn;
    public Text[] NicknameTexts;
    public GameObject[] ArrowImages;
    public Text[] MoneyTexts;
    public Text LogText;
    public GameObject overBtn;

    [Header("Board")]
    public DiceScript diceScript;
    public PlayerScript[] Players;
    public Transform[] Pos;

    int myNum, turn;
    PhotonView PV;

    void Start()
    {
#if (!UNITY_ANDROID)
        Screen.SetResolution(960, 540, false);
#endif
        PV = photonView;
        ShowPanel(DisconnectPanel);
    }

    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = NicknameInput.text;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // 랜덤 방에 접속을 시도하고 실패하면 새로운 방을 생성합니다.
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 랜덤 방에 접속을 실패하면 새로운 방을 생성합니다.
        CreateRoom();
    }

    void CreateRoom()
    {
        string roomName = "Room_" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    void ShowPanel(GameObject CurPanel)
    {
        DisconnectPanel.SetActive(false);
        RoomPanel.SetActive(false);

        CurPanel.SetActive(true);
    }

    bool master()
    {
        return PhotonNetwork.LocalPlayer.IsMasterClient;
    }

    public override void OnJoinedRoom()
    {
        ShowPanel(RoomPanel);

        if (PhotonNetwork.CurrentRoom.PlayerCount > 2)
        {
            // 방에 두 명 이상이면 새로운 방을 생성합니다.
            CreateRoom();
            return;
        }

        if (master() && PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            InitGameBtn.SetActive(true);
        }
    }

    public void InitGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;

        turn = 0; // 첫 번째 플레이어의 턴으로 설정
        InitGameBtn.SetActive(false);
        PV.RPC("InitGameRPC", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void InitGameRPC()
    {
        print("게임시작");
        for (int i = 0; i < 2; i++)
        {
            NicknameTexts[i].text = PhotonNetwork.PlayerList[i].NickName;

            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
                myNum = i;
        }

        // 턴에 따라 Roll 버튼과 화살표 이미지의 초기 상태를 설정
        RollBtn.SetActive(myNum == turn);
        for (int i = 0; i < 2; i++)
        {
            ArrowImages[i].SetActive(i == turn);
        }
    }

    public void Roll()
    {
        PV.RPC("RollRPC", RpcTarget.MasterClient);
    }

    [PunRPC]
    void RollRPC()
    {
        StartCoroutine(RollCo());
    }

    [PunRPC]
    void EndRollRPC(int money0, int money1)
    {
        turn = turn == 0 ? 1 : 0;

        for (int i = 0; i < 2; i++)
            ArrowImages[i].SetActive(i == turn);

        RollBtn.SetActive(myNum == turn);

        MoneyTexts[0].text = money0.ToString();
        MoneyTexts[1].text = money1.ToString();

        if (money0 <= 0 || money1 >= 300)
        {
            LogText.text = NicknameTexts[1].text + "이 승리하셨습니다";
            overBtn.SetActive(true);
        }
        else if (money1 <= 0 || money0 >= 300)
        {
            LogText.text = NicknameTexts[0].text + "이 승리하셨습니다";
            overBtn.SetActive(true);
        }
    }

    public void overbtn()
    {
        SceneManager.LoadScene("Ex");
    }

    IEnumerator RollCo()
    {
        // 방장만 함수 호출
        yield return StartCoroutine(diceScript.Roll());
        yield return StartCoroutine(Players[turn].Move(diceScript.num));
        yield return new WaitForSeconds(0.2f);

        PV.RPC("EndRollRPC", RpcTarget.AllViaServer, Players[0].money, Players[1].money);
    }
}
