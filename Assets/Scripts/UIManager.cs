using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class UIManager : MonoBehaviour
{
    [Header("#Pad")]
    public GameObject Pad;
    public PadManager padMgr;

    [Header("#MenuPan")]
    public GameObject MenuPan;

    [Header("#GamePan")]
    public GameObject GamePan;

    [Header("InGameSet")]
    public GameObject InGameSet;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;

        InGameSet.SetActive(false);
    }
    public void Back()
    {
        if (Pad.activeSelf == true && InGameSet.activeSelf == false)
        {
            Pad.SetActive(false);
        }
        else if (MenuPan.activeSelf == true)
        {
            MenuPan.SetActive(false);
        }
        else if (GamePan.activeSelf == true)
        {
            GamePan.SetActive(false);
        }
        else if (InGameSet.activeSelf == true)
        {
            InGameSet.SetActive(false);
        }
    }

}
