using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ThirdDayGameEventManager : GameEventManager
{
    public Sprite WinSprite;

    public GameObject ClearState;

    public HeroinAnimator heroinAnimator;

    public int chatCount;


    public bool isInCutScene;

    public Door doorToFirstFloor_Hospital;
    public TriggerDoor doorToOutside_Hospital;
    public TriggerDoor doorToSpecialRoom_Hospital;

    public Npc minigameNpc;

    public DuduGManager duduGManager;
    public DanceManager danceManager;
    public flagGameManager flaggameManager;

    public bool isChange=false;


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

        sw.WriteLine(doorToFirstFloor_Hospital.isLock.ToString());
        sw.WriteLine(doorToFirstFloor_Hospital.doorLockTextidx.ToString());

        sw.WriteLine(doorToOutside_Hospital.isLock.ToString());
        sw.WriteLine(doorToOutside_Hospital.doorSoundIdx.ToString());

        sw.WriteLine(doorToSpecialRoom_Hospital.isLock.ToString());
        sw.WriteLine(doorToSpecialRoom_Hospital.doorSoundIdx.ToString());

        sw.WriteLine(duduGManager.isWin.ToString());
        sw.WriteLine(danceManager.isWin.ToString());
        sw.WriteLine(flaggameManager.isWin.ToString());

        sw.WriteLine(isChange.ToString());

        sw.WriteLine(minigameNpc.talkNum.ToString());
        sw.WriteLine(minigameNpc.isEvent.ToString());
        sw.WriteLine(minigameNpc.isFixedTalk.ToString());
        sw.WriteLine(minigameNpc.eventIndex.ToString());

        sw.WriteLine(nurseAnimator.GetComponent<Npc>().talkNum.ToString());
        sw.WriteLine(nurseAnimator.GetComponent<Npc>().isEvent.ToString());
        sw.WriteLine(nurseAnimator.GetComponent<Npc>().isFixedTalk.ToString());
        sw.WriteLine(nurseAnimator.GetComponent<Npc>().eventIndex.ToString());

        sw.WriteLine(doctorAnimator.GetComponent<Npc>().talkNum.ToString());
        sw.WriteLine(doctorAnimator.GetComponent<Npc>().isEvent.ToString());
        sw.WriteLine(doctorAnimator.GetComponent<Npc>().isFixedTalk.ToString());
        sw.WriteLine(doctorAnimator.GetComponent<Npc>().eventIndex.ToString());


        sw.WriteLine(nurseAnimator.transform.position.x.ToString());
        sw.WriteLine(nurseAnimator.transform.position.y.ToString());

        sw.WriteLine(ClearState.active.ToString());

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
        doorToFirstFloor_Hospital.isLock = bool.Parse(sr.ReadLine());
        doorToFirstFloor_Hospital.doorLockTextidx = int.Parse(sr.ReadLine());

        doorToOutside_Hospital.isLock = bool.Parse(sr.ReadLine());
        doorToOutside_Hospital.doorSoundIdx = int.Parse(sr.ReadLine());

        doorToSpecialRoom_Hospital.isLock = bool.Parse(sr.ReadLine());
        doorToSpecialRoom_Hospital.doorSoundIdx = int.Parse(sr.ReadLine());

        duduGManager.isWin = bool.Parse(sr.ReadLine());
        if (duduGManager.isWin) {duduGManager.firstImage.sprite = WinSprite; }
        danceManager.isWin = bool.Parse(sr.ReadLine());
        if (danceManager.isWin) { danceManager.firstImage.sprite = WinSprite; }
        flaggameManager.isWin = bool.Parse(sr.ReadLine());
        if (flaggameManager.isWin) { flaggameManager.firstImage.sprite = WinSprite; }

        isChange = bool.Parse(sr.ReadLine());

        minigameNpc.talkNum = int.Parse(sr.ReadLine());
        minigameNpc.isEvent = bool.Parse(sr.ReadLine());
        minigameNpc.isFixedTalk = bool.Parse(sr.ReadLine()); ;
        minigameNpc.eventIndex = int.Parse(sr.ReadLine());

        nurseAnimator.GetComponent<Npc>().talkNum = int.Parse(sr.ReadLine());
        nurseAnimator.GetComponent<Npc>().isEvent = bool.Parse(sr.ReadLine());
        nurseAnimator.GetComponent<Npc>().isFixedTalk = bool.Parse(sr.ReadLine());
        nurseAnimator.GetComponent<Npc>().eventIndex = int.Parse(sr.ReadLine());

        doctorAnimator.GetComponent<Npc>().talkNum = int.Parse(sr.ReadLine());
        doctorAnimator.GetComponent<Npc>().isEvent = bool.Parse(sr.ReadLine());
        doctorAnimator.GetComponent<Npc>().isFixedTalk = bool.Parse(sr.ReadLine());
        doctorAnimator.GetComponent<Npc>().eventIndex = int.Parse(sr.ReadLine());


        float x;
        float y;
        x= float.Parse(sr.ReadLine());
        y= float.Parse(sr.ReadLine());
        nurseAnimator.transform.position = new Vector2(x, y);


        ClearState.SetActive(bool.Parse(sr.ReadLine()));

        sr.Close();
    }







    private void StartFirstCutScene()
    {
        PlayerData.playerData.addItem(3);
        PlayerData.playerData.removeItem(1);
        GameManager.canInput = false;
        GameManager.gameManager.lastSpace = GameManager.Space.HospitalSecond;
        GameManager.gameManager.changeFootStepSound();
        PlayerAnimation.playerAnimation.changeState(0);
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        StartCoroutine(FirstCutSceneCoroutine());
    }

    private IEnumerator FirstCutSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.ShowTextOff();
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        ChatManager.chatManager.OpenChat(40, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        EventTriggers[0].SetActive(true);
        doorToFirstFloor_Hospital.isLock = false;
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(1, true);
        KnowedManager.knowedManager.changeNowState(22);
    }

    private void StartSecondCutScene()
    {
        GameManager.canInput = false;
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        ChatManager.chatManager.OpenChat(49, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        PlayerData.playerData.isOpenKnoweds[2] = true;
        PlayerData.playerData.removeItem(4);
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
                doorToOutside_Hospital.isLock = false;
                KnowedManager.knowedManager.changeNowState(24);
                nurseAnimator.GetComponent<Npc>().isFixedTalk = true;
                nurseAnimator.GetComponent<Npc>().talkNum = 214;
                nurseAnimator.GetComponent<Npc>().canTalk = true;
                nurseAnimator.GetComponent<Npc>().isEvent = false;
                doctorAnimator.GetComponent<Npc>().talkNum = 231;
                doctorAnimator.GetComponent<Npc>().isEvent = false;
                doctorAnimator.GetComponent<Npc>().canTalk = true;
                doctorAnimator.GetComponent<Npc>().isFixedTalk = true;
                break;
            case 1:
                PlayerData.playerData.addItem(4);
                minigameNpc.talkNum = 218;
                minigameNpc.isEvent = false;
                minigameNpc.isFixedTalk = true;
                minigameNpc.canTalk = true;
                nurseAnimator.GetComponent<Npc>().talkNum = 250;
                KnowedManager.knowedManager.changeNowState(27);
                doorToOutside_Hospital.isLock=true;
                doorToOutside_Hospital.lockChatNumber = 219;
                EventTriggers[2].SetActive(true);
                doorToSpecialRoom_Hospital.isLock = false;

                ClearState.SetActive(false);
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
                EventTriggers[1].SetActive(true);
                ChatManager.chatManager.OpenChat(41, null);
                KnowedManager.knowedManager.changeNowState(23);
                doorToFirstFloor_Hospital.isLock = false;
                break;
            case 1:
                EventTriggers[1].SetActive(false);
                ChatManager.chatManager.OpenChat(43, OpenClearState);
                KnowedManager.knowedManager.changeNowState(25);
                break;
            case 2:
                EventTriggers[2].SetActive(false);
                EventTriggers[3].SetActive(true);
                ChatManager.chatManager.OpenChat(48, null);
                break;
            case 3:
                EventTriggers[3].SetActive(false);
                StartSecondCutScene();
                break;

            default:
                break;
        }
    }

    public void OpenClearState() { ClearState.SetActive(true); }












    private void Update()
    {
        if (isFirst)
        {
            isFirst = false;
            StartFirstCutScene();
        }


        if (!isChange && duduGManager.isWin && danceManager.isWin && flaggameManager.isWin) {
            isChange = true;
            minigameNpc.talkNum = 47;
            minigameNpc.isEvent = true;
            minigameNpc.isFixedTalk = false;
            minigameNpc.eventIndex = 1;
            KnowedManager.knowedManager.changeNowState(26);
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
            case 16:
                StartCoroutine(AnimationSet_16());
                break;
            case 17:
                StartCoroutine(AnimationSet_17());
                break;
            case 18:
                StartCoroutine(AnimationSet_18());
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
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_1()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        PlayerAnimation.playerAnimation.moveToY(1f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(-0.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        yield return new WaitForSeconds(0.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        yield return new WaitForSeconds(0.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        nurseAnimator.moveToY(0f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        nurseAnimator.moveToX(-4.5f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        nurseAnimator.moveToY(1f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_7()
    {
        nurseAnimator.moveToY(7f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        nurseAnimator.transform.position = new Vector2(-7.5f, 13f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_8()
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

    private IEnumerator AnimationSet_9()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_10()
    {
        yield return new WaitForSeconds(0.3f);
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_11()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_12()
    {
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.moveToY(0f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(10.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_13()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_14()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.moveToX(-3f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_15()
    {
        yield return new WaitForSeconds(0.3f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.3f);

        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_16()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_17()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.moveToX(-4f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_18()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.moveToX(-1f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }

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
        GameManager.gameManager.ShowTextOn("3일째 밤", 2);
        GameManager.gameManager.sceneNumber = 7;
        GameManager.gameManager.MoveScene("ThirdNightScene", true);
    }


}