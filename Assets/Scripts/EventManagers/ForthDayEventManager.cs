using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class ForthDayEventManager : GameEventManager
{

    public HeroinAnimator heroinAnimator;
    public AlexAnimator alexAnimator;

    public Npc floweShopNpc;
    public Npc Alex;

    public TriggerDoor door_from_out_to_forest;
    public Door door_from_out_to_alex;
    public TriggerDoor door_from_hos_to_specialRoom;
    public bool isInCutScene;

   



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
        sw.WriteLine(floweShopNpc.talkNum.ToString());
        sw.WriteLine(floweShopNpc.isEvent.ToString());
        sw.WriteLine(floweShopNpc.isFixedTalk.ToString());
        sw.WriteLine(floweShopNpc.eventIndex.ToString());

        sw.WriteLine(Alex.talkNum.ToString());
        sw.WriteLine(Alex.isEvent.ToString());
        sw.WriteLine(Alex.isFixedTalk.ToString());
        sw.WriteLine(Alex.eventIndex.ToString());

        sw.WriteLine(door_from_out_to_forest.isLock.ToString());
        sw.WriteLine(door_from_out_to_alex.isLock.ToString());
        sw.WriteLine(door_from_hos_to_specialRoom.isLock.ToString());

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
        floweShopNpc.talkNum = int.Parse(sr.ReadLine());
        floweShopNpc.isEvent = bool.Parse(sr.ReadLine());
        floweShopNpc.isFixedTalk = bool.Parse(sr.ReadLine()); 
        floweShopNpc.eventIndex = int.Parse(sr.ReadLine());

        Alex.talkNum = int.Parse(sr.ReadLine());
        Alex.isEvent = bool.Parse(sr.ReadLine());
        Alex.isFixedTalk = bool.Parse(sr.ReadLine()); 
        Alex.eventIndex = int.Parse(sr.ReadLine());

        door_from_out_to_forest.isLock= bool.Parse(sr.ReadLine());
        door_from_out_to_alex.isLock = bool.Parse(sr.ReadLine());
        door_from_hos_to_specialRoom.isLock=bool.Parse(sr.ReadLine());

        sr.Close();
    }







    private void StartFirstCutScene()
    {
        GameManager.canInput = false;
        GameManager.gameManager.lastSpace = GameManager.Space.HospitalSecond;
        GameManager.gameManager.changeFootStepSound();
        PlayerAnimation.playerAnimation.changeState(0);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
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
        ChatManager.chatManager.OpenChat(54, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(1, true);
        KnowedManager.knowedManager.changeNowState(30);
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
        ChatManager.chatManager.OpenChat(60, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
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
                EventTriggers[0].SetActive(true);
                floweShopNpc.canTalk = true;
                floweShopNpc.talkNum =221;
                floweShopNpc.isFixedTalk = true;
                floweShopNpc.isEvent = false;
                KnowedManager.knowedManager.changeNowState(31);
                break;
            case 1:
                Alex.canTalk = true;
                Alex.talkNum = 222;
                Alex.isFixedTalk = true;
                Alex.isEvent = false;
                KnowedManager.knowedManager.changeNowState(32);
                door_from_out_to_forest.isLock = false;
                EventTriggers[4].SetActive(true);
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
                door_from_out_to_alex.isLock = false;
                ChatManager.chatManager.OpenChat(56, null);
                break;
            case 1:
                EventTriggers[1].SetActive(false);
                EventTriggers[2].SetActive(true);
                ChatManager.chatManager.OpenChat(57, null);
                break;
            case 2:
                EventTriggers[2].SetActive(false);
                EventTriggers[3].SetActive(true);
                Alex.talkNum = 225;
                KnowedManager.knowedManager.changeNowState(33);
                ChatManager.chatManager.OpenChat(59, null);
                door_from_out_to_forest.isLock = true;
                door_from_hos_to_specialRoom.isLock = false;
                break;
            case 3:
                EventTriggers[3].SetActive(false);
                StartSecondCutScene();
                break;
            case 4:
                EventTriggers[4].SetActive(false);
                ChatManager.chatManager.OpenChat(242, null);
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
            case 100:
                StartCoroutine(AnimationSet_100());
                break;
            case 101:
                StartCoroutine(AnimationSet_101());
                break;
            case 102:
                StartCoroutine(AnimationSet_102());
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
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        PlayerAnimation.playerAnimation.moveToY(4f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        PlayerAnimation.playerAnimation.moveToX(34f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(1.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        PlayerAnimation.playerAnimation.moveToX(270.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(281f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {

        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        PlayerAnimation.playerAnimation.moveToX(76f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(-43f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_7()
    {
        yield return new WaitForSeconds(0.5f);
        alexAnimator.See(AlexAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_8()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_9()
    {
        yield return new WaitForSeconds(0.5f);
        alexAnimator.See(AlexAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        alexAnimator.See(AlexAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_10()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_11()
    {
        PlayerAnimation.playerAnimation.moveToX(1.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(525.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_12()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.lastSpace = GameManager.Space.Library;
        GameManager.gameManager.changeFootStepSound();

        PlayerAnimation.playerAnimation.transform.position = new Vector2(76,-43);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        alexAnimator.See(AlexAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(2f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(16, true);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_13()
    {
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.001f);
        PlayerAnimation.playerAnimation.moveToX(-2f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_14()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_15()
    {
        PlayerAnimation.playerAnimation.moveToX(-1f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_16()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
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

    private IEnumerator AnimationSet_102()
    {
        PlayerAnimation.playerAnimation.moveToX(17.0f);
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

    private IEnumerator MoveToNextScene()
    {
        GameManager.canInput = false;

        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();

        FadeInOutBlack.fadeInOutBlack.SetColor(2);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);

        yield return new WaitForSeconds(5f);
        GameManager.gameManager.ShowTextOn("4일째 밤", 2);
        GameManager.gameManager.sceneNumber = 10;
        GameManager.gameManager.MoveScene("ForthNightScene", true);
    }


}