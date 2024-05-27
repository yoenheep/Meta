using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMenu : MonoBehaviour
{
    public Image Img;
    public TextMeshProUGUI NameTxt;
    public TextMeshProUGUI NumTxt;

    public List<Sprite> MenuSprtieList = new List<Sprite>();
    public List<string> MenuTxtList;

    private void Awake()
    {
        MenuTxtList = new List<string>() { "아이스티", "아이스 아메리카노", "아메리카노", "제로펩시" };
    }

    private void OnEnable()
    {
        this.gameObject.transform.SetParent(UIManager.Instance.padMgr.MenuBoxParent.transform);
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);

        Img.sprite = MenuSprtieList[UIManager.Instance.padMgr.MenuNum];
        NameTxt.text = MenuTxtList[UIManager.Instance.padMgr.MenuNum];
        NumTxt.text = "";

        UIManager.Instance.padMgr.PrefabList[UIManager.Instance.padMgr.MenuNum] = this.gameObject;
    }

    public void RemovePrefab()
    {
        UIManager.Instance.padMgr.RemovePrefab(this);
        Destroy(this.gameObject);
    }
}
