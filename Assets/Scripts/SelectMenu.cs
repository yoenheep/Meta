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

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        for(int i = 0; i < UIManager.Instance.SelectMenuNumList.Count; i++)
        {
            if(UIManager.Instance.MenuNum == i)
            {

            }
            else
            {
                this.gameObject.transform.SetParent(UIManager.Instance.MenuBoxParent.transform);
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);

                Img.sprite = UIManager.Instance.MenuSprtieList[UIManager.Instance.MenuNum];
                NameTxt.text = UIManager.Instance.MenuTxtList[UIManager.Instance.MenuNum];
                NumTxt.text = "";
            }
        }  
    }
}
