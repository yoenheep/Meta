using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PadManager : MonoBehaviour
{
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
    public GameObject[] PrefabList;

    public int MenuNum;
    public int SameCount;

    private void Awake()
    {
        MenuPad.SetActive(true);
        GamePad.SetActive(false);

        SelectMenuNumList = new List<int>();
        PrefabList = new GameObject[4];
    }

    private void Start()
    {
        for (int i = 0; i < MenuList.Length; i++)
        {
            int temp = i;
            MenuList[temp].onClick.AddListener(delegate { SelectMenu(temp); });
        }
    }

    public void GamePadBtn()
    {
        GamePad.SetActive(true);
        MenuPad.SetActive(false);

        GamePadImage.color = new Color(98 / 255f, 146 / 255f, 190 / 255f);
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

    public void CollectGame()
    {
        UIManager.Instance.InGameSet.SetActive(true);
    }

    public void SelectMenu(int num)
    {
        MenuNum = num;
        SelectMenuNumList.Add(num);

        bool isAlreadyInstantiated = false;

        for (int i = 0; i < SelectMenuNumList.Count - 1; i++)
        {
            if (SelectMenuNumList[i] == num)
            {
                MenuEtc();
                Debug.Log(SameCount);
                isAlreadyInstantiated = true;
                GameObject existingPrefab = PrefabList[num];
                TextMeshProUGUI[] prefabTextComponents = existingPrefab.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (TextMeshProUGUI texComponent in prefabTextComponents)
                {
                    if (texComponent.name == "orderNumTxt")
                    {
                        texComponent.text = "X" + SameCount;
                    }
                }
                break;
            }
        }

        if (!isAlreadyInstantiated)
        {
            GameObject MenuInstance = Instantiate(MenuPrefab, transform.position, Quaternion.identity);
        }
    }

    void MenuEtc()
    {
        SameCount = 0;

        for (int i = 0; i < SelectMenuNumList.Count - 1; i++)
        {
            if (SelectMenuNumList[i] == MenuNum)
            {
                SameCount++;
            }
        }
    }

    public void RemovePrefab(SelectMenu selectMenu)
    {
        int index = Array.IndexOf(PrefabList, selectMenu.gameObject);

        if (index >= 0)
        {
            PrefabList[index] = null;

            SelectMenuNumList.RemoveAll(num => num == index);
        }
    }

    public void orderBtn()
    {
        // 모든 프리펩 삭제
        foreach (GameObject prefab in PrefabList)
        {
            if (prefab != null)
            {
                Destroy(prefab);
            }
        }

        // 리스트와 배열 초기화
        SelectMenuNumList.Clear();
        PrefabList = new GameObject[4];
    }
}
