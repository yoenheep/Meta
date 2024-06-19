using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
	[Header("DisconnectPanel")]
	public GameObject DisconnectPanel;
	public InputField NicknameInput;

	[Header("RoomPanel")]
	public GameObject RoomPanel;
	public GameObject startBg, RollBtn;
	public Text[] NicknameTexts;
	public GameObject[] ArrowImages;
	public Text[] MoneyTexts;
	public Text LogText;
	public Sprite[] seasonSprite;
	public int rand;
	public Image seasonImg;

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
	}

	public void Connect() 
	{
		PhotonNetwork.LocalPlayer.NickName = NicknameInput.text;
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinOrCreateRoom("MyRoom", new RoomOptions { MaxPlayers = 2 }, null);
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

	public override void OnJoinedRoom() // 닉넴적고 접속누름
	{
		ShowPanel(RoomPanel);
		if (master()) startBg.SetActive(true);
	}

	void InitRpc()
	{
        PV.RPC("InitGameRPC", RpcTarget.AllViaServer);
    }

	public void InitGame() // 시작버튼
	{
		if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;

		RollBtn.SetActive(true);
        startBg.SetActive(false);
		rand = Random.Range(0, 4);
		seasonImg.sprite = seasonSprite[rand];

		Invoke("InitRpc",5f);
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

		if (money0 <= 0 || money1 >= 300) LogText.text = NicknameTexts[1].text + "이 승리하셨습니다";
		else if (money1 <= 0 || money0 >= 300) LogText.text = NicknameTexts[0].text + "이 승리하셨습니다";
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
