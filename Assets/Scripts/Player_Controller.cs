using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 6.0f;
    public float rotate_Speed = 60f;
    private Rigidbody rb;
    private Transform rotate;

    public float interactionRange = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotate = GetComponent<Transform>();

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 이동 방향을 현재 회전에 맞춰 조정
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        movement = rotate.TransformDirection(movement) * speed;

        // Rigidbody의 속도를 이동 방향으로 설정
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // 마우스 오른쪽 버튼이 눌려 있을 때 회전
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            rotate.Rotate(0, mouseX * rotate_Speed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                GameObject obj = hit.collider.gameObject;

                if(hit.collider.gameObject.name.Equals("Door"))
                {
                    Debug.Log("문과 상호작용");
                    obj.GetComponent<Door>().Interact_Door();
                } else if(hit.collider.gameObject.name.Equals("table_interact"))
                {
                    Debug.Log("테블릿과 상호작용");
                    obj.GetComponent<Tablet>().Interact_Tablet();
                }
                //Debug.Log("Interacted with " + hit.collider.gameObject.name);
                // 오브젝트와의 상호작용 로직 추가
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * interactionRange);
        Gizmos.DrawWireSphere(transform.position + transform.forward * interactionRange, 0.1f);
    }
}
