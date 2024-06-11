using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GroundScript : MonoBehaviourPun
{
    // 지형의 종류를 정의하는 열거형
    public enum GroundType { GROUND, GOLDKEY, START };
    public GroundType groundType; // 지형의 타입
    public int price, owner; // 가격과 소유자 정보

    PhotonView PV; // PhotonView 컴포넌트
    TextMesh PriceText; // 가격을 표시하는 텍스트 메시
    GameObject[] Cubes; // 지형에 대한 정보를 표시하는 큐브 오브젝트 배열
    int[] goldKeyMoneys = new int[6] { -20, -10, 0, 10, 20, 30 }; // 골드 키 이벤트에서 변동할 돈의 배열

    void Start()
    {
        PV = photonView; // PhotonView 컴포넌트 설정

        // 만약 지형이 일반 지형이라면
        if (groundType == GroundType.GROUND)
        {
            PriceText = GetComponentInChildren<TextMesh>(); // 자식 오브젝트에서 TextMesh 컴포넌트를 찾아 설정
            Cubes = new GameObject[2] { transform.GetChild(1).gameObject, transform.GetChild(2).gameObject }; // 자식 오브젝트를 설정하여 큐브 배열 초기화
        }
    }

    // 지형에 큐브를 활성화하는 RPC 함수
    [PunRPC]
    void CubeRPC(int myNum)
    {
        Cubes[myNum].SetActive(true); // 해당 번호의 큐브를 활성화
    }

    // 가격을 증가시키는 RPC 함수
    [PunRPC]
    void AddPriceRPC()
    {
        price += 10; // 가격을 10 증가
        PriceText.text = price.ToString(); // 텍스트 메시 업데이트
    }

    // 지형의 타입에 따라 동작을 수행하는 함수
    public void TypeSwitch(PlayerScript curPlayer, PlayerScript otherPlayer)
    {
        if (groundType == GroundType.GROUND) // 만약 일반 지형이라면
        {
            GroundOwner(curPlayer, otherPlayer); // 소유자를 결정하고 가격을 증가시킴
            PV.RPC("AddPriceRPC", RpcTarget.AllViaServer); // 모든 플레이어에게 가격 증가를 통보
        }
        else if (groundType == GroundType.GOLDKEY) // 만약 골드 키 지형이라면
        {
            int addmoney = goldKeyMoneys[Random.Range(0, goldKeyMoneys.Length)]; // 랜덤한 금액을 얻어서
            curPlayer.money += addmoney; // 해당 플레이어의 돈을 증가시킴
            print(curPlayer.myNum + "이 " + addmoney + "을 벌었다"); // 로그 출력
        }
    }

    // 지형의 소유자를 결정하는 함수
    void GroundOwner(PlayerScript curPlayer, PlayerScript otherPlayer)
    {
        int myNum = curPlayer.myNum; // 현재 플레이어의 번호

        // 빈 땅을 밞음
        if (owner == -1)
        {
            curPlayer.money -= price; // 가격만큼 현재 플레이어의 돈을 감소
            print(myNum + "이 " + price + "을 잃었다"); // 로그 출력

            owner = myNum; // 소유자를 현재 플레이어로 설정
            PV.RPC("CubeRPC", RpcTarget.AllViaServer, myNum); // 모든 플레이어에게 큐브를 활성화하는 RPC 호출
        }
        // 남의 땅을 밞음
        else if (owner != myNum) // 만약 소유자가 현재 플레이어가 아니라면
        {
            curPlayer.money -= price; // 가격만큼 현재 플레이어의 돈을 감소
            otherPlayer.money += price; // 다른 플레이어의 돈을 증가
            print(myNum + "이 " + price + "을 잃었다"); // 로그 출력
            print(otherPlayer.myNum + "이 " + price + "을 벌었다"); // 로그 출력
        }
    }
}
