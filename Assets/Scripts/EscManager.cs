using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EscManager : MonoBehaviour
{
    public static EscManager escManager
    {
        get
        {
            if (esc_instance == null)
            {
                esc_instance = FindObjectOfType<EscManager>();
            }

            return esc_instance;
        }
    }//GameManager를 싱글턴으로 설정

    private static EscManager esc_instance; //싱글턴에 이용된 인스턴스




    public Text[] SaveButtonTexts = new Text[3];
    public GameObject[] SaveButtonObjs = new GameObject[3];
    public Text[] LoadButtonTexts = new Text[3];
    public GameObject[] LoadButtonObjs = new GameObject[3];


    public GameObject ButtonsUI;

    public GameObject ESCUI;
    public GameObject LoadUI;
    public GameObject SaveUI;
    public GameObject EndUI;

    public GameObject SaveCheckUI;
    public GameObject LoadCheckUI;

    private bool isOpenLoad;
    private bool isOpenSave;
    private bool isOpenEnd;



    public int where; //어디에 누른 것인지

    public PlayerInput playerInput;
    public PlayerInput playerMovement;

    private bool isOpen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ESCUI.SetActive(false);
        ButtonsUI.SetActive(true);

        LoadUI.SetActive(false);
        SaveUI.SetActive(false);
        EndUI.SetActive(false);

        SaveCheckUI.SetActive(false);
        LoadCheckUI.SetActive(false);



        isOpen = false;
        isOpenLoad=false;
        isOpenSave=false;
        isOpenEnd=false;

        DrawButtons();
    }

    private void Update()
    {
        if (GameManager.canInput == false&&!isOpen) return;


        if (playerInput.pause) {
            SoundManager.soundManager.PlayClickSound();
            if (isOpen)
            {
                if (isOpenLoad)
                {
                    LoadUI.SetActive(false);
                    LoadCheckUI.SetActive(false);
                    isOpenLoad = false;
                    ButtonsUI.SetActive(true);
                }
                else if (isOpenSave)
                {
                    SaveUI.SetActive(false);
                    SaveCheckUI.SetActive(false);
                    isOpenSave = false;
                    ButtonsUI.SetActive(true);
                }
                else if (isOpenEnd)
                {
                    EndUI.SetActive(false);
                    isOpenEnd = false;
                    ButtonsUI.SetActive(true);
                }
                else
                { 
                    ESCUI.SetActive(false);
                    GameManager.canInput = true;
                    isOpen = false;
                }
            }
            else {
                ESCUI.SetActive(true);
                ButtonsUI.SetActive(true);
                GameManager.canInput = false;
                isOpen = true;
            }
        }



    }

    public void OnClickSaveButton() {
        SoundManager.soundManager.PlayClickSound();
        ButtonsUI.SetActive(false);
        SaveUI.SetActive(true);
        isOpenSave = true;
    }

    public void OnClickLoadButton() {
        SoundManager.soundManager.PlayClickSound();
        ButtonsUI.SetActive(false);
        LoadUI.SetActive(true);
        isOpenLoad = true;
    }

    public void OnClickEndButton() {
        SoundManager.soundManager.PlayClickSound();
        ButtonsUI.SetActive(false);
        EndUI.SetActive(true);
        isOpenEnd = true;
    }

    public void SaveAt(int idx) {
        SoundManager.soundManager.PlayClickSound();
        where = idx;
        SaveCheckUI.SetActive(true);
        SaveUI.SetActive(false);
    }

    public void LoadAt(int idx) {
        SoundManager.soundManager.PlayClickSound();
        where = idx;
        LoadCheckUI.SetActive(true);
        LoadUI.SetActive(false);
    }

    public void SaveYes() {
        SoundManager.soundManager.PlayClickSound();
        SaveUI.SetActive(true);
        SaveCheckUI.SetActive(false);
        GameManager.gameManager.SaveData(where);
        DrawButtons();
    }

    public void LoadYes() {
        SoundManager.soundManager.PlayClickSound();
        LoadCheckUI.SetActive(false);
        LoadUI.SetActive(false);
        ESCUI.SetActive(false);
        GameManager.gameManager.LoadData(where);
    }

    public void EndYes() {
        SoundManager.soundManager.PlayClickSound();
        Application.Quit();
    }

    public void SaveNo()
    {
        SoundManager.soundManager.PlayClickSound();
        SaveCheckUI.SetActive(false);
        SaveUI.SetActive(true);
    }

    public void LoaNo()
    {
        SoundManager.soundManager.PlayClickSound();
        LoadCheckUI.SetActive(false);
        LoadUI.SetActive(true);
    }

    public void EndNo()
    {
        SoundManager.soundManager.PlayClickSound();
        ButtonsUI.SetActive(true);
        EndUI.SetActive(false);
        isOpenEnd = false;
    }

    public void DrawButtons()
    {
        for (int i = 0; i < LoadButtonTexts.Length; i++)
        {
            string savePath = Application.dataPath + "/savingData/GameData" + i.ToString() + ".dat";
            if (!File.Exists(savePath))
            {
                SaveButtonTexts[i].text = "빈 저장데이터";
                LoadButtonObjs[i].SetActive(false);
            }
            else
            {
                StreamReader sr = new StreamReader(savePath);
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                string str = sr.ReadLine();
                str += "\n" + sr.ReadLine();
                SaveButtonTexts[i].text = str;
                LoadButtonObjs[i].SetActive(true);
                LoadButtonTexts[i].text = str;
                sr.Close();
            }
        }
    }
}
