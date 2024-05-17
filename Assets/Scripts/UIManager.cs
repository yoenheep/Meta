using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header ("#Pad")]
    public GameObject Pad;
    public Image GamePadImage;
    public Image MenuPadImage;
    public TextMeshProUGUI GamePadTxt;
    public TextMeshProUGUI MenuPadTxt;
    public GameObject GamePad;
    public GameObject MenuPad;
    public List<GameObject> SelectList;

    [Header ("#MenuPan")]
    public GameObject MenuPan;

    [Header ("#GamePan")]
    public GameObject GamePan;

    [Header("InGameSet")]
    public GameObject InGameSet;

    private void Awake()
    {
        GamePad.SetActive(true);
        MenuPad.SetActive(false);
        SelectList = new List<GameObject>();
    }

    public void GamePadBtn()
    {
        GamePad.SetActive(true);
        MenuPad.SetActive(false);

        GamePadImage.color = new Color(98 / 255f, 146 / 255f,190 / 255f);
        MenuPadImage.color = Color.white;
        GamePadTxt.color = Color.white;
        MenuPadTxt.color = Color.black;
    }

    public void MenuPadBtn()
    {
        MenuPad.SetActive(true);
        GamePad.SetActive(false);

        GamePadImage.color = Color.white;
        MenuPadImage.color = new Color(98 / 255f, 146 / 255f, 190 / 255f);
        GamePadTxt.color = Color.black;
        MenuPadTxt.color = Color.white;
    }

    public void Back()
    {
        if(Pad.activeSelf == true)
        {
            Pad.SetActive(false);
        }
        else if(MenuPad.activeSelf == true)
        {
            MenuPad.SetActive(false);
        }
        else if(GamePad.activeSelf == true)
        {
            GamePad.SetActive(false);
        }
        else if(InGameSet.activeSelf == true)
        {
            InGameSet.SetActive(false);
        }
    }
}
