using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator door_ani;
    
    bool is_Opened;

    private void Start()
    {
        door_ani = GetComponent<Animator>();
        is_Opened = false;
    }

    public void Interact_Door()
    {
        if (!is_Opened)
        {
            is_Opened = true;
            door_ani.SetInteger("door", 1);
        }
        else
        {
            is_Opened = false;
            door_ani.SetInteger("door", -1);
        }
    }
}
