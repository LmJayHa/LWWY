using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class StartManager : MonoBehaviour
{

    public Button[] buttons; //모든 버튼들


    private int where;

    public GameObject SelectButtonUI;
    public GameObject SelectLoadUI;
    public GameObject SelectYesNoUI;

    public Text[] LoadButtonTexts = new Text[3];
    public GameObject[] LoadButtonObjs = new GameObject[3];

    public Image BlackImage;

    private bool starting;
    private float startTime;
    private float fadeValue;

    private void Awake()
    {
        OnClickReturnToReturnButton();
        starting = false;
    }

    private void Start()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(7f);
        GameManager.canInput = false;
        BlackImage.color = new Color(0, 0, 0, 0);
        BlackImage.gameObject.SetActive(false);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(20,true);
    }

    private void Update()
    {
        if (starting) {
            StartingGame();
        }


        if (SelectLoadUI.active == true && Input.GetKeyDown(KeyCode.Escape)) {
            OnClickReturnToReturnButton();
        }
    }


    private void OnEnable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
        }
    }


    public void OnClickNewStartButton() {
        SoundManager.soundManager.PlayClickSound();
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }

        BlackImage.gameObject.SetActive(true);
        startTime = Time.time;
        fadeValue = 0;
        starting = true;
    }

    public void OnClickLoadButton() {
        SoundManager.soundManager.PlayClickSound();
        DrawLoadButtons();
        SelectButtonUI.SetActive(false);
        SelectLoadUI.SetActive(true);
        SelectYesNoUI.SetActive(false);
    }

    public void OnClickToLoadWhere(int toWhere) {
        SoundManager.soundManager.PlayClickSound();
        SelectButtonUI.SetActive(false);
        SelectLoadUI.SetActive(false);
        SelectYesNoUI.SetActive(true);
        where = toWhere;
    }

    public void OnClickLoadButtonYes()
    {
        SoundManager.soundManager.PlayClickSound();
        GameManager.canInput = true;
        GameManager.gameManager.LoadData(where);
    }

    public void OnClickLoadButtonNo()
    {
        SoundManager.soundManager.PlayClickSound();
        SelectButtonUI.SetActive(false);
        SelectLoadUI.SetActive(true);
        SelectYesNoUI.SetActive(false);
    }

    public void OnClickEndButton() {
        SoundManager.soundManager.PlayClickSound();
        Application.Quit();
    }

    public void OnClickReturnToReturnButton() {
        SelectButtonUI.SetActive(true);
        SelectLoadUI.SetActive(false);
        SelectYesNoUI.SetActive(false);
    }

    public void DrawLoadButtons() {
        for (int i = 0; i < LoadButtonTexts.Length; i++)
        {
            string savePath = Application.dataPath + "/savingData/GameData" + i.ToString() + ".dat";
            if (!File.Exists(savePath)) {
                LoadButtonObjs[i].SetActive(false);
            } else {
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
                str+= "\n"+sr.ReadLine();
                LoadButtonObjs[i].SetActive(true);
                LoadButtonTexts[i].text=str;
                sr.Close();
            }
        }
    }


    public void StartingGame() {
        if (Time.time <= startTime + 2f)
        {
            fadeValue += Time.deltaTime/2f;

            BlackImage.color = new Color(0, 0, 0, fadeValue);
        }
        else if(starting) {
            starting = false;
            BlackImage.color = new Color(0, 0, 0, 1);
            GameManager.canInput = true;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].enabled = true;
            }
            GameManager.gameManager.MoveScene("FirstDayScene",true);
            GameManager.gameManager.sceneNumber = 0;
        }

    }




        private IEnumerator MoveToNextScene()
    {
        GameManager.canInput = false;


        FadeInOutBlack.fadeInOutBlack.SetColor(1);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);
        yield return new WaitForSeconds(4f);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(1.5f);
        PlayerAnimation.playerAnimation.changeState(0);
        GameManager.gameManager.ShowTextOn("마지막날 낮", 1);
        GameManager.gameManager.sceneNumber = 18;
        GameManager.gameManager.MoveScene("LastScene", true);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
    }
}
