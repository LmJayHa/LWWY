using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceManager : MonoBehaviour
{
    public bool isWin = false;

    public Image firstImage;
    public Sprite WinSprite;



    public GameObject UI;

    public enum State { LEFT,RIGHT,UP,DOWN,SPACE};

    State[] buttonState = new State[21];

    public Image[] buttonImage = new Image[21];
    public GameObject[] buttonObj = new GameObject[21];

    public Sprite[] buttonSprite = new Sprite[5];

    public Text timeText;
    public Text levelText;

    int level;

    float lastTime;
    float[] gameTime = { 25, 23, 21, 19, 17 };

    bool isInGame = false;
    bool isStartStage = false;

    int nowIndex;

    System.Random rn = new System.Random();


    public void StartGame()
    {
        UI.SetActive(true);
        GameManager.canInput = false;
        level = 1;
        isInGame=true;
        SetState();
    }


    public void SetState() {
        for (int i = 0; i < 21; i++)
        {
            int idx = rn.Next() % 5;
            buttonImage[i].sprite = buttonSprite[idx];

            switch (idx)
            {
                case 0:
                    buttonState[i] = State.UP;
                    break;
                case 1:
                    buttonState[i] = State.DOWN;
                    break;
                case 2:
                    buttonState[i] = State.LEFT;
                    break;
                case 3:
                    buttonState[i] = State.RIGHT;
                    break;
                case 4:
                    buttonState[i] = State.SPACE;
                    break;
                default:
                    break;
            }

            buttonObj[i].SetActive(true);
        }
        nowIndex = 0;
        isStartStage = true;
        lastTime = Time.time;

    }

    public void Hit() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.soundManager.PlayEffectClip(23);
            if (buttonState[nowIndex] != State.UP)
            {
                LoseGame();
            }
            else {
                buttonObj[nowIndex].SetActive(false);
                nowIndex++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            SoundManager.soundManager.PlayEffectClip(23);
            if (buttonState[nowIndex] != State.DOWN)
            {
                LoseGame();
            }
            else
            {
                buttonObj[nowIndex].SetActive(false);
                nowIndex++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SoundManager.soundManager.PlayEffectClip(23);
            if (buttonState[nowIndex] != State.LEFT)
            {
                LoseGame();
            }
            else
            {
                buttonObj[nowIndex].SetActive(false);
                nowIndex++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SoundManager.soundManager.PlayEffectClip(23);
            if (buttonState[nowIndex] != State.RIGHT)
            {
                LoseGame();
            }
            else
            {
                buttonObj[nowIndex].SetActive(false);
                nowIndex++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.soundManager.PlayEffectClip(23);
            if (buttonState[nowIndex] != State.SPACE)
            {
                LoseGame();
            }
            else
            {
                buttonObj[nowIndex].SetActive(false);
                nowIndex++;
            }
        }

        if (nowIndex == 21)
        {
            NextRound();
        }

    }



    void LoseGame() {
        UI.SetActive(false);
        isInGame = false;
        isStartStage = false;
        GameManager.canInput = true;
        ChatManager.chatManager.OpenChat(241, null);
    }


    void EndGame()
    {
        SoundManager.soundManager.PlayEffectClip(22);
        firstImage.sprite = WinSprite;

        isWin = true;
    UI.SetActive(false);
        isInGame = false;
        isStartStage = false;
        GameManager.canInput = true;
        ChatManager.chatManager.OpenChat(45, null);
    }

    void NextRound() {
        isStartStage = false;

        if (level == 5)
        {
            EndGame();
        }
        else {
            level++;
            SetState();
        }

    }







    private void Update()
    {
        if (!isInGame) return;

        GameManager.canInput = false;


        if (lastTime + gameTime[level-1] <= Time.time)
            LoseGame();


        timeText.text = (gameTime[level - 1] - (Time.time - lastTime)).ToString("F1") +"초 남음";
        levelText.text = level.ToString() + "단계";
        if (isStartStage) { Hit(); }

    }

}
