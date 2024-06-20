using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviourPunCallbacks
{
    public GameObject obj_Player_Prefab;
    public Transform tf_PlayerList_Parent;
    public string str_NickName;

    private void Start()
    {
        DefaultPool pool = new DefaultPool();
        pool.ResourceCache.Clear();
        pool.ResourceCache.Add(obj_Player_Prefab.name, obj_Player_Prefab);

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.NickName = obj_Player_Prefab.name;
        PhotonNetwork.JoinOrCreateRoom("Boardgame", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Create_Player();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }

    public void Create_Player()
    {
        GameObject player = PhotonNetwork.Instantiate(obj_Player_Prefab.name, Vector3.zero, Quaternion.identity);

    }
}
