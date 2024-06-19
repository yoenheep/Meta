using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("오브젝트 충돌");
        if (collision.gameObject.CompareTag("Player"))
        {
            // 오브젝트 조작 코드 작성
            Debug.Log("Player has collided with the object.");
        }
    }
}
