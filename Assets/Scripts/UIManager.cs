using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
    public GameObject MenuBoxParent;
    public GameObject MenuPrefab;
    public List<int> SelectMenuNumList;

    public Button[] MenuList;
    public List<Sprite> MenuSprtieList;
    public List<string> MenuTxtList;
    public int MenuNum;

    [Header ("#MenuPan")]
    public GameObject MenuPan;

    [Header ("#GamePan")]
    public GameObject GamePan;

    [Header("InGameSet")]
    public GameObject InGameSet;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;

        MenuPad.SetActive(true);
        GamePad.SetActive(false);
        InGameSet.SetActive(false);

        SelectMenuNumList = new List<int> ();
        MenuSprtieList = new List<Sprite>();
        MenuTxtList = new List<string>() { "아이스티", "아이스 아메리카노", "아메리카노", "제로펩시"};
    }

    private void Start()
    {
        for(int i = 0; i < MenuList.Length; i++)
        {
            int temp = i;
            MenuList[temp].onClick.AddListener(() => SelectMenu(temp));
        }
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
        if(Pad.activeSelf == true && InGameSet.activeSelf == false)
        {
            Pad.SetActive(false);
        }
        else if(MenuPan.activeSelf == true)
        {
            MenuPan.SetActive(false);
        }
        else if(GamePan.activeSelf == true)
        {
            GamePan.SetActive(false);
        }
        else if(InGameSet.activeSelf == true)
        {
            InGameSet.SetActive(false);
        }
    }

    public void CollectGame()
    {
        InGameSet.SetActive(true);
    }

    public void SelectMenu(int num)
    {
        MenuNum = num;
        SelectMenuNumList.Add(num);
        //Instantiate(MenuPrefab.name, transform.position, Quaternion.identity);
    }
}
