using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuduGManager : MonoBehaviour
{
    public bool isWin = false;



    public Image firstImage;
    public Sprite WinSprite;

    public GameObject UI;

    public DuDuG[] duDuGs = new DuDuG[6];

    public int Score = 0;
    public bool isInGame = false;

    public Text scoreText;
    public Text timeText;


    public float startTime;
    public const float gameTime = 30f;

    public void StartGame()
    {
        GameManager.canInput = false;
        UI.SetActive(true);
        startTime = Time.time;

        Score = 0;
        isInGame = true;

        for (int i = 0; i < 6; i++)
        {
            duDuGs[i].isInGame = true;
        }
    }

    public void Hit(int idx) {
        SoundManager.soundManager.PlayEffectClip(26);
        Score += duDuGs[idx].point;
        duDuGs[idx].SetState();
        scoreText.text = Score.ToString() + "점";
    }




    public void EndGame() {
        firstImage.sprite = WinSprite;
        SoundManager.soundManager.PlayEffectClip(22);

        isWin = true;
        GameManager.canInput = true;

        UI.SetActive(false);

        isInGame = false;

        for (int i = 0; i < 6; i++)
        {
            duDuGs[i].isInGame = false;
        }
        ChatManager.chatManager.OpenChat(44, null);
    }

    public void LoseGame() {
        GameManager.canInput = true;

        UI.SetActive(false);

        isInGame = false;

        for (int i = 0; i < 6; i++)
        {
            duDuGs[i].isInGame = false;
        }
        ChatManager.chatManager.OpenChat(241, null);
    }




    private void Update()
    {
        if (!isInGame) return;

        timeText.text = (Time.time - startTime).ToString("F1") + "초";
        if (Time.time >= startTime + gameTime) {
            if (Score >= 450) { EndGame(); }
            else { LoseGame(); }
        }
    }

}
