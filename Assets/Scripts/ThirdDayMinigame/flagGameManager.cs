using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flagGameManager : MonoBehaviour
{
    public bool isWin = false;



    public Image firstImage;
    public Sprite WinSprite;

    public GameObject UI;


    public const float gameTime = 2.5f; //게임의 제한시간
    public float LastTime; // 

    public Image Blueflagimg;// 청기 이미지
    public Image Whiteflagimg;// 백기 이미지

    public Sprite[] BlueFlagSprites = new Sprite[2];
    public Sprite[] WhiteFlagSprites = new Sprite[2];


    public bool isOn_blue;
    public bool isOn_white;

    public bool isCorrectstate_blue; // 청기의 답
    public bool isCorrectstate_white; // 백기의 답

    public int count; // 앞으로 남은 게임 횟수

    public Text OrderText;
    public Text CountText;
    public Text TimeText;

    public int orderIndex;

    public string[] OrderSet =
    {
        "청기올려!",
        "청기내려!",
        "백기올려!",
        "백기내려!",
        "청기올리지마!",
        "백기올리지마!",
        "청기올리지말고 백기 올려!",
        "백기올리지말고 청기 올려!",
        "청기내리지말고 백기 내려!",
        "백기내리지말고 청기 내려!",
        "전부 내려!",
        "전부 올려!"
    };

    public enum State { ON,OFF,DONTCARE };

    public State[,] OrderAnswer = {
    { State.ON,State.DONTCARE },
    { State.OFF,State.DONTCARE },
    { State.DONTCARE,State.ON },
    { State.DONTCARE,State.OFF },
    { State.OFF,State.DONTCARE },
    { State.DONTCARE,State.OFF },
    { State.OFF,State.ON },
    { State.ON,State.OFF },
    { State.ON,State.OFF },
    { State.OFF,State.ON },
    { State.OFF,State.OFF },
    { State.ON,State.ON }
    };


    public bool isInGame;

    public bool isInGameTime=false;

    public void SetState() {
        System.Random rn = new System.Random();
        while (true) { 
        int num= rn.Next() % 12;
            if (orderIndex != num) {
                orderIndex = num;
                break;
            }
        }

        OrderText.text = OrderSet[orderIndex];
        CountText.text = count.ToString()+"번 남음";
        LastTime = Time.time;

        isInGameTime = true;
    }

    public void Check()
    {

        isInGameTime = false;

        State blueState = OrderAnswer[orderIndex,0];
        State whiteState= OrderAnswer[orderIndex, 1];

        isCorrectstate_blue = false;
        switch (blueState)
        {
            case State.ON:
                if (isOn_blue == true)
                    isCorrectstate_blue = true;
                break;
            case State.OFF:
                if (isOn_blue == false)
                    isCorrectstate_blue = true;
                break;
            case State.DONTCARE:
                isCorrectstate_blue = true;
                break;
            default:
                break;
        }

        isCorrectstate_white = false;
        switch (whiteState)
        {
            case State.ON:
                if (isOn_white == true)
                    isCorrectstate_white = true;
                break;
            case State.OFF:
                if (isOn_white == false)
                    isCorrectstate_white = true;
                break;
            case State.DONTCARE:
                isCorrectstate_white = true;
                break;
            default:
                break;
        }


        if (isCorrectstate_blue && isCorrectstate_white)
        {
            count--;
            if (count == 0)
            {
                EndGame();
            }
            else {
                SetState();
            }
        }
        else {
            LoseGame();
        }

    }

    public void LoseGame() {


        isInGame = false;
        UI.SetActive(false);
        GameManager.canInput = true;
        ChatManager.chatManager.OpenChat(241, null);
    }

    public void EndGame()
    {
        firstImage.sprite = WinSprite;
        SoundManager.soundManager.PlayEffectClip(22);
        isWin = true;
        isInGame = false;
        UI.SetActive(false);
        GameManager.canInput = true;
        ChatManager.chatManager.OpenChat(46, null);
    }

    public void StartGame()
    {
        GameManager.canInput = false;
        isInGame = true;
        UI.SetActive(true);
        count = 15;

        Blueflagimg.sprite = BlueFlagSprites[0];
        Whiteflagimg.sprite = WhiteFlagSprites[0];

        isOn_blue = false;
        isOn_white = false;

        SetState();
    }

    public void BlueFlagAction() {
        SoundManager.soundManager.PlayEffectClip(23);
        if (isOn_blue)
        {
            isOn_blue = false;
            Blueflagimg.sprite = BlueFlagSprites[0];
        }
        else {
            isOn_blue = true;
            Blueflagimg.sprite = BlueFlagSprites[1];
        }
    }

    public void WhiteFlagAction()
    {
        SoundManager.soundManager.PlayEffectClip(23);
        if (isOn_white)
        {
            isOn_white = false;
            Whiteflagimg.sprite = WhiteFlagSprites[0];
        }
        else {
            isOn_white = true;
            Whiteflagimg.sprite = WhiteFlagSprites[1];
        }
    }


    private void Update()
    {

        if (!isInGame)
        {
            return;
        }

        TimeText.text = (gameTime-(Time.time - LastTime)).ToString("F1");

        if (isInGameTime && Time.time >= LastTime + gameTime) {   // >= 이것의 우선순위가 &&보다 높음 , 지금 시간이 설정한 시간을 초과하고, 현재 청기백기를 올리는 시간이라면
            Check();
        }

    }


}
