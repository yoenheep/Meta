using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviourPunCallbacks
{

    [PunRPC]
    public void Interact_Tablet()
    {
        Debug.Log("테블릿 상호작용");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interact_Tablet();
        }
    }
}
