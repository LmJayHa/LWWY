using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SixthPuzzleGameEventManager : GameEventManager
{



    public override void SaveData(int where)
    {
        base.SaveData(where);
        string filePath = Application.dataPath + "/savingData/GameEventManager" + where.ToString() + ".dat";

        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/savingData");
        if (!dir.Exists)
        {
            Directory.CreateDirectory(Application.dataPath + "/savingData");
        }

        FileInfo file = new FileInfo(filePath);
        if (!file.Exists)
        { File.Create(filePath).Close(); }

        FileStream fs = file.OpenWrite();
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < EventTriggers.Length; i++)
        {
            sw.WriteLine(EventTriggers[i].active.ToString());
        }

        sw.Close();
        fs.Close();
    }


    public override void LoadData(int where)
    {

        Debug.Log("로딩시작...");

        base.LoadData(where);
        string filePath = Application.dataPath + "/savingData/GameEventManager" + where.ToString() + ".dat";

        if (!File.Exists(filePath)) { return; }

        StreamReader sr = new StreamReader(filePath);

        for (int i = 0; i < EventTriggers.Length; i++)
        {
            EventTriggers[i].SetActive(bool.Parse(sr.ReadLine()));
        }



        sr.Close();
    }









    private void StartFirstCutScene()
    {

        GameManager.canInput = false;
        SoundManager.soundManager.PlayDoorSound(1);
        GameManager.gameManager.lastSpace = GameManager.Space.Puzzle;
        GameManager.gameManager.changeFootStepSound();
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        StartCoroutine(FirstCutSceneCoroutine());
    }

    private IEnumerator FirstCutSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.ShowTextOff();
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        ChatManager.chatManager.OpenChat(82, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(3, true);
        KnowedManager.knowedManager.changeNowState(28);
    }


    private void StartSecondCutScene()
    {
        GameManager.canInput = false;
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        ChatManager.chatManager.OpenChat(84, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        flower.SetActive(true);
        PlayerData.playerData.isOpenKnoweds[7] = true;
        StartCoroutine(MoveToNextScene());
    }

    public override void StartEvent_toNPC(int idx)
    {
        base.StartEvent_toNPC(idx);
        switch (idx)
        {
            case 0:
                break;
            default:
                break;
        }
    }


    public override void EndEvent_toNPC(int idx)
    {
        base.EndEvent_toNPC(idx);
        switch (idx)
        {
            case 0:
                break;
            default:
                break;
        }
    }

    public override void StartEventTrigger(int idx)
    {
        base.StartEventTrigger(idx);
        switch (idx)
        {
            case 0:
                break;
            default:
                break;
        }
    }




    public override void PlayAnimations(int idx)
    {
        base.PlayAnimations(idx);


        switch (idx)
        {
            case 0:
                StartCoroutine(AnimationSet_0());
                break;
            case 1:
                StartCoroutine(AnimationSet_1());
                break;
            case 2:
                StartCoroutine(AnimationSet_2());
                break;
            case 3:
                StartCoroutine(AnimationSet_3());
                break;
            case 4:
                StartCoroutine(AnimationSet_4());
                break;

            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_1()
    {
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        yield return new WaitForSeconds(1.5f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(19, true);
        CameraShake.cameraShake.Shake(4f, 0.15f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4);
        yield return new WaitForSeconds(4f);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(2.5f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(4,true);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(300,26);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2);
        yield return new WaitForSeconds(3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
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




























    //퍼즐 부분

    public GameObject flower;
    public int count = 0;

    public GameObject[] flowerPots = new GameObject[7];

    public enum State { WATER, SUNRISE, LOVE, ENERGY};

    public State nextState = State.WATER;


    public void Reset()
    {
        for (int i = 0; i < 7; i++)
        {
            flowerPots[i].SetActive(false);
        }
           count = 0;
        nextState = State.WATER;
    }

    public void CountUp() {
        if (count == 7) { EndGame();  return; }
        flowerPots[count].SetActive(true);
        count++;

        switch (count)
        {
            case 0:
            case 7:
                nextState = State.WATER;
                break;
            case 1:
            case 3:
            case 4:
                nextState = State.SUNRISE;
                break;
            case 2:
            case 5:
                nextState = State.ENERGY;
                break;
            case 6:
                nextState = State.LOVE;
                break;
            default:
                break;
        }

    }

    public void OnClickButton(int idx) {
        SoundManager.soundManager.PlayEffectClip(17);
        switch (idx)
        {
            case 0:
                if (nextState == State.WATER) { CountUp(); } else { Reset(); }
                break;
            case 1:
                if (nextState == State.SUNRISE) { CountUp(); } else { Reset(); }
                break;
            case 2:
                if (nextState == State.LOVE) { CountUp(); } else { Reset(); }
                break;
            case 3:
                if (nextState == State.ENERGY) { CountUp(); } else { Reset(); }
                break;
            default:
                break;
        }
    }


    public void EndGame()
    {
        flower.SetActive(true);
        StartSecondCutScene();
    }


    private void Update()
    {
        if (isFirst)
        {
            isFirst = false;
            StartFirstCutScene();
        }


    }

}