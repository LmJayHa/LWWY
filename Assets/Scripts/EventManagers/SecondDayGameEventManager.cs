using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SecondDayGameEventManager : GameEventManager
{

    public HeroinAnimator heroinAnimator;

    public int chatCount;

    public bool isInSmallCutScene = false;

    public bool isInCutScene;

    public TriggerDoor ToOutDoor;

    private float startTime;
    private float fadeValue;

    private int bookCount = 0;
    public GameObject canReadFlower;

    public GameObject[] canBuy;

    public SecondDayMiniPuzzle Puzzle_0;
    public SecondDayMiniPuzzle Puzzle_1;

    public Npc cleakNpc;
    public TriggerDoor doorToSpecialRoom;
    public TriggerDoor doorToOut_Lib;

    private bool isEnd_0 = false;
    private bool isEnd_1 = false;

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

        sw.WriteLine(ToOutDoor.isLock.ToString());
        sw.WriteLine(doorToSpecialRoom.isLock.ToString());
        sw.WriteLine(doorToOut_Lib.isLock.ToString());

        sw.WriteLine(ToOutDoor.lockChatNumber.ToString());
        sw.WriteLine(doorToSpecialRoom.lockChatNumber.ToString());
        sw.WriteLine(doorToOut_Lib.lockChatNumber.ToString());

        sw.WriteLine(bookCount.ToString());

        sw.WriteLine(canReadFlower.active.ToString());

        sw.WriteLine(canBuy[0].active.ToString());
        sw.WriteLine(canBuy[1].active.ToString());

        sw.WriteLine(cleakNpc.canTalk.ToString());
        sw.WriteLine(cleakNpc.isFixedTalk.ToString());
        sw.WriteLine(cleakNpc.talkNum.ToString());
        sw.WriteLine(cleakNpc.eventIndex.ToString());
        sw.WriteLine(cleakNpc.isEvent.ToString());

        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().canTalk.ToString());
        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().isFixedTalk.ToString());
        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().talkNum.ToString());
        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().eventIndex.ToString());
        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().isEvent.ToString());

        sw.WriteLine(isEnd_0.ToString());
        sw.WriteLine(isEnd_1.ToString());

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

        ToOutDoor.isLock = bool.Parse(sr.ReadLine());
        doorToSpecialRoom.isLock = bool.Parse(sr.ReadLine());
        doorToOut_Lib.isLock = bool.Parse(sr.ReadLine());

        ToOutDoor.lockChatNumber = int.Parse(sr.ReadLine());
        doorToSpecialRoom.lockChatNumber = int.Parse(sr.ReadLine());
        doorToOut_Lib.lockChatNumber = int.Parse(sr.ReadLine());

        bookCount = int.Parse(sr.ReadLine());

        canReadFlower.SetActive(bool.Parse(sr.ReadLine()));

        canBuy[0].SetActive(bool.Parse(sr.ReadLine()));
        canBuy[1].SetActive(bool.Parse(sr.ReadLine()));

        cleakNpc.canTalk = bool.Parse(sr.ReadLine());
        cleakNpc.isFixedTalk = bool.Parse(sr.ReadLine());
        cleakNpc.talkNum = int.Parse(sr.ReadLine());
        cleakNpc.eventIndex = int.Parse(sr.ReadLine());
        cleakNpc.isEvent = bool.Parse(sr.ReadLine());

        doctorAnimator.transform.GetComponent<Npc>().canTalk = bool.Parse(sr.ReadLine());
        doctorAnimator.transform.GetComponent<Npc>().isFixedTalk = bool.Parse(sr.ReadLine());
        doctorAnimator.transform.GetComponent<Npc>().talkNum = int.Parse(sr.ReadLine());
        doctorAnimator.transform.GetComponent<Npc>().eventIndex = int.Parse(sr.ReadLine());
        doctorAnimator.transform.GetComponent<Npc>().isEvent = bool.Parse(sr.ReadLine());

        isEnd_0 = bool.Parse(sr.ReadLine());
        isEnd_1 = bool.Parse(sr.ReadLine());

        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);

        sr.Close();
    }







    private void StartFirstCutScene()
    {
        GameManager.canInput = false;
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        StartCoroutine(FirstCutSceneCoroutine());
    }

    private IEnumerator FirstCutSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.ShowTextOff();
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        ChatManager.chatManager.OpenChat(18, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(1, true);
        KnowedManager.knowedManager.changeNowState(9);
    }

    private void StartSecondCutScene()
    {
        GameManager.canInput = false;
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        ChatManager.chatManager.OpenChat(22, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        KnowedManager.knowedManager.changeNowState(11);
    }

    private void StartThirdCutScene()
    {
        GameManager.canInput = false;
        doctorAnimator.transform.GetComponent<Npc>().talkNum = 34;
        StartCoroutine(ThirdCutSceneCoroutine());
    }

    private IEnumerator ThirdCutSceneCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        ChatManager.chatManager.OpenChat(27, EndThirdCutScene);
    }

    public void EndThirdCutScene()
    {
        KnowedManager.knowedManager.changeNowState(11);
        for (int i = 0; i < canBuy.Length; i++)
        {
            canBuy[i].SetActive(true);
        }
        KnowedManager.knowedManager.changeNowState(13);
    }


    private void StartForthCutScene()
    {
        GameManager.canInput = false;
        doorToSpecialRoom.lockChatNumber = 206;
        doorToSpecialRoom.isLock = true;
        ToOutDoor.isLock = true;
        ToOutDoor.lockChatNumber = 211;
        StartCoroutine(ForthCutSceneCoroutine());
    }

    private IEnumerator ForthCutSceneCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        ChatManager.chatManager.OpenChat(33, EndForthCutScene);
    }

    public void EndForthCutScene()
    {
        GameManager.gameManager.lastSpace = GameManager.Space.HospitalSecond;
        GameManager.gameManager.changeFootStepSound();
        KnowedManager.knowedManager.changeNowState(17);
    }




    //==퍼즐 끝==

    public void EndMiniGame_0()
    {
        canBuy[0].SetActive(false);
        isEnd_0 = true;

        if (isEnd_0 && isEnd_1) EndMiniGame();
    }
    public void EndMiniGame_1()
    {
        canBuy[1].SetActive(false);
        isEnd_1 = true;

        if (isEnd_0 && isEnd_1) EndMiniGame();
    }

    private void EndMiniGame()
    {
        StartCoroutine(EndMiniGameCoroutine());
    }


    private IEnumerator EndMiniGameCoroutine()
    {
        GameManager.canInput = false;
        yield return new WaitForSeconds(1.5f);
        ChatManager.chatManager.OpenChat(30, EndMiniCutScene);
    }

    public void EndMiniCutScene()
    {
        KnowedManager.knowedManager.changeNowState(14);
        cleakNpc.isEvent = true;
        cleakNpc.talkNum = 31;
        //아주머니 대화 변경
    }






    public override void StartEvent_toNPC(int idx)
    {
        base.StartEvent_toNPC(idx);
        switch (idx)
        {
            case 0:
                ToOutDoor.isLock = false;
                break;
            case 1:
                if (bookCount != -1)
                {
                    bookCount++;
                    Debug.Log("카운트 늘어남");
                }
                break;
            case 2:
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
                doctorAnimator.GetComponent<Npc>().talkNum = 21;
                doctorAnimator.GetComponent<Npc>().isFixedTalk = true;
                doctorAnimator.GetComponent<Npc>().canTalk = true;
                doctorAnimator.GetComponent<Npc>().isEvent = false;
                doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);
                KnowedManager.knowedManager.changeNowState(10);
                EventTriggers[0].SetActive(true);
                EventTriggers[5].SetActive(true);
                break;
            case 1:
                if(canReadFlower.active == true)
                isInSmallCutScene = true;
                break;
            case 2:
                EventTriggers[1].SetActive(true);
                doorToOut_Lib.isLock = false;
                KnowedManager.knowedManager.changeNowState(12);
                break;
            case 3:
                Puzzle_0.OpenPuzzle();
                break;
            case 4:
                Puzzle_1.OpenPuzzle();
                break;
            case 5:
                cleakNpc.isEvent = false;
                cleakNpc.talkNum = 209;
                EventTriggers[2].SetActive(true);
                doorToSpecialRoom.isLock = false;
                KnowedManager.knowedManager.changeNowState(15);
                PlayerData.playerData.addItem(1);
                PlayerData.playerData.addItem(2);
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
                EventTriggers[0].SetActive(false);
                StartSecondCutScene();
                break;
            case 1:
                EventTriggers[1].SetActive(false);
                StartThirdCutScene();
                break;
            case 2:
                EventTriggers[2].SetActive(false);
                EventTriggers[3].SetActive(true);
                ChatManager.chatManager.OpenChat(32, null);
                KnowedManager.knowedManager.changeNowState(16);
                break;
            case 3:
                EventTriggers[3].SetActive(false);
                EventTriggers[4].SetActive(true);
                StartForthCutScene();
                break;
            case 4:
                EventTriggers[4].SetActive(false);
                StartCoroutine(MoveToNextScene());
                break;
            case 5:
                EventTriggers[5].SetActive(false);
                ChatManager.chatManager.OpenChat(64, null);
                break;
            default:
                break;
        }
    }













    private void Update()
    {
        if (isFirst)
        {
            isFirst = false;
            StartFirstCutScene();
        }

        if (bookCount >= 3) { canReadFlower.SetActive(true); bookCount = -100000; }
        if (isInSmallCutScene) { isInSmallCutScene = false; ChatManager.chatManager.OpenChat(65, null); }

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
            case 5:
                StartCoroutine(AnimationSet_5());
                break;
            case 6:
                StartCoroutine(AnimationSet_6());
                break;
            case 7:
                StartCoroutine(AnimationSet_7());
                break;
            case 8:
                StartCoroutine(AnimationSet_8());
                break;
            case 9:
                StartCoroutine(AnimationSet_9());
                break;
            case 10:
                StartCoroutine(AnimationSet_10());
                break;
            case 11:
                StartCoroutine(AnimationSet_11());
                break;
            case 12:
                StartCoroutine(AnimationSet_12());
                break;
            case 13:
                StartCoroutine(AnimationSet_13());
                break;
            case 14:
                StartCoroutine(AnimationSet_14());
                break;
            case 15:
                StartCoroutine(AnimationSet_15());
                break;
            case 100:
                StartCoroutine(AnimationSet_100());
                break;
            case 101:
                StartCoroutine(AnimationSet_101());
                break;
            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_1()
    {
        PlayerAnimation.playerAnimation.moveToX(-47.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(15f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        doctorAnimator.See(DoctorAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        PlayerAnimation.playerAnimation.moveToY(-58f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_7()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_8()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.moveToY(0f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        PlayerAnimation.playerAnimation.moveToX(10.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_9()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_10()
    {
        PlayerAnimation.playerAnimation.moveToX(0f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_11()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.8f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.8f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.8f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_12()
    {
        SoundManager.soundManager.PlayDoorSound(0);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        heroinAnimator.transform.position = new Vector2(-7, 97);
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.moveToX(-4.5f);
        yield return new WaitForSeconds(0.01f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.8f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_13()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.8f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_14()
    {
        PlayerAnimation.playerAnimation.runToY(95f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);

        PlayerAnimation.playerAnimation.runToX(-6f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);

        PlayerAnimation.playerAnimation.runToY(97f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);

        PlayerAnimation.playerAnimation.runToX(-6.75f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);

        SoundManager.soundManager.PlayDoorSound(0);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(10.5f, 0f);

        yield return new WaitForSeconds(1.5f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(1f);
        yield return new WaitForSeconds(1.1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_15()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1.1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_100()
    {
        PlayerAnimation.playerAnimation.moveToX(255.25f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_101()
    {
        PlayerAnimation.playerAnimation.moveToY(311.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }


    private IEnumerator MoveToNextScene()
    {
        GameManager.canInput = false;

        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();

        FadeInOutBlack.fadeInOutBlack.SetColor(2);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);

        yield return new WaitForSeconds(5f);
        GameManager.gameManager.ShowTextOn("2일째 밤", 2);
        GameManager.gameManager.sceneNumber = 4;
        GameManager.gameManager.MoveScene("SecondNightScene", true);
    }


}

