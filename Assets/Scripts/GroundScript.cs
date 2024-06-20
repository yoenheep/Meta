using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GroundScript : MonoBehaviourPun
{
    public enum GroundType { GROUND, GOLDKEY, START };
    public GroundType groundType;
    public int price, owner;

    PhotonView PV;
    TextMesh PriceText;
    GameObject[] Cubes;
    int[] goldKeyMoneys = new int[6] { -20, -10, 0, 10, 20, 30 };


    void Start()
    {
        PV = photonView;
        if (groundType == GroundType.GROUND)
        {
            PriceText = GetComponentInChildren<TextMesh>();
            Cubes = new GameObject[2] { transform.GetChild(1).gameObject, transform.GetChild(2).gameObject };
        }
    }

    [PunRPC]
    void CubeRPC(int myNum)
    {
        Cubes[myNum].SetActive(true);
    }

    [PunRPC]
    void AddPriceRPC()
    {
        price += 10;
        PriceText.text = price.ToString();
    }


    public void TypeSwitch(PlayerScript curPlayer, PlayerScript otherPlayer)
    {
        if (groundType == GroundType.GROUND)
        {
            GroundOwner(curPlayer, otherPlayer);
            PV.RPC("AddPriceRPC", RpcTarget.AllViaServer);
        }

        else if (groundType == GroundType.GOLDKEY)
        {
            int addmoney = goldKeyMoneys[Random.Range(0, goldKeyMoneys.Length)];
            curPlayer.money += addmoney;
            print(curPlayer.myNum + "이 " + addmoney + "을 벌었다");
        }
    }


    void GroundOwner(PlayerScript curPlayer, PlayerScript otherPlayer)
    {
        int myNum = curPlayer.myNum;

        // 빈 땅을 밞음
        if (owner == -1)
        {
            curPlayer.money -= price;
            print(myNum + "이 " + price + "을 잃었다");

            owner = myNum;
            PV.RPC("CubeRPC", RpcTarget.AllViaServer, myNum);
        }

        // 남의 땅을 밞음
        else if (owner != myNum)
        {
            curPlayer.money -= price;
            otherPlayer.money += price;
            print(myNum + "이 " + price + "을 잃었다");
            print(otherPlayer.myNum + "이 " + price + "을 벌었다");
        }
    }
}
