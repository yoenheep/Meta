using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviourPunCallbacks
{

    [PunRPC]
    public void Interact_Tablet()
    {
        Debug.Log("�׺� ��ȣ�ۿ�");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interact_Tablet();
        }
    }
}
