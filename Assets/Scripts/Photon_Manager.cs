using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class Photon_Manager : MonoBehaviourPunCallbacks
{
    public List<GameObject> list_Photon_Prefabs;
    public Transform tf_Respawn_Point;
    public UserData m_LocalPlayer_Data;
    public GameObject obj_LocalPlayer;

    private void Start()
    {
        //���⼭ ���濡�� �����ϴ� ������Ʈ�� ���
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Clear();
        if (pool != null && list_Photon_Prefabs != null)
        {
            foreach (GameObject prefab in list_Photon_Prefabs)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
        }
        InitPhotonServer();
    }

    //���� ������ ���� ���� �� �κ� ����
    private void InitPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Photon : ConnectUsingSettings");
        }
    }

    //�� ����
    private void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom("YD_METAVERSE", roomOptions, TypedLobby.Default);
    }

    private void CreateCharacter(UserData m_data)
    {
        if (m_data.gender == 0)
        {
            Debug.Log("���� ĳ���� ����");
            obj_LocalPlayer = PhotonNetwork.Instantiate(list_Photon_Prefabs[0].name, tf_Respawn_Point.position, Quaternion.identity);
        }
        else if (m_data.gender == 1)
        {
            obj_LocalPlayer = PhotonNetwork.Instantiate(list_Photon_Prefabs[1].name, tf_Respawn_Point.position, Quaternion.identity);
            Debug.Log("���� ĳ���� ����");
        }
        else
        {
            Debug.Log("������ ������ �������� �ʽ��ϴ�.");
        }
    }

    ///////////////////���渶���ͼ��� ���� �ݹ� ����
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon : OnConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    ///////////////////���渶���ͼ��� ���� �ݹ� ��

    ///////////////////�κ� ���� �ݹ� ����
    public override void OnJoinedLobby()
    {
        Debug.Log("Photon : OnJoinedLobby");
        JoinRoom();
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Photon : OnLeftLobby");
    }

    ///////////////////�κ� ���� �ݹ� ��

    ///////////////////�� ���� �ݹ� ����

    public override void OnCreatedRoom()
    {
        Debug.Log("Photon : OnCreatedRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Photon : OnCreateRoomFailed returnCode : " + returnCode + ", message : " + message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Photon : OnJoinedRoom");
        CreateCharacter(m_LocalPlayer_Data);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Photon : OnJoinRoomFailed returnCode : " + returnCode + ", message : " + message);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Photon : OnLeftRoom");
    }

    ///////////////////�� ���� �ݹ� ��

    ///////////////////�÷��̾� ���� �ݹ� ����
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Photon : OnPlayerEnteredRoom newPlayer : " + newPlayer.UserId);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Photon : OnPlayerLeftRoom otherPlayer : " + otherPlayer.UserId);
    }

    ///////////////////�÷��̾� ���� �ݹ� ��
}

[System.Serializable]
public class UserData
{
    /// <summary>
    /// 0 : ��, 1 : ��
    /// </summary>
    public int gender;

    public string name;

    public UserData()
    {
        gender = 0;
        name = "";
    }
}