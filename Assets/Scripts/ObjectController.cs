using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("������Ʈ �浹");
        if (collision.gameObject.CompareTag("Player"))
        {
            // ������Ʈ ���� �ڵ� �ۼ�
            Debug.Log("Player has collided with the object.");
        }
    }
}
