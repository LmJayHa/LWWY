using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SixthDayGameEventManager : GameEventManager
{

    public HeroinAnimator heroinAnimator;
    public AlexAnimator alexAnimator; 

    public bool isInCutScene;

    public Npc alexNpc;

    public TriggerDoor door_from_hos2_to_spe;

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

        sw.WriteLine(door_from_hos2_to_spe.isLock.ToString());

        sw.WriteLine(doctorAnimator.GetComponent<Npc>().talkNum.ToString());
        sw.WriteLine(doctorAnimator.GetComponent<Npc>().isEvent.ToString());
        sw.WriteLine(doctorAnimator.GetComponent<Npc>().isFixedTalk.ToString());
        sw.WriteLine(doctorAnimator.GetComponent<Npc>().eventIndex.ToString());

        sw.WriteLine(alexNpc.talkNum.ToString());
        sw.WriteLine(alexNpc.isEvent.ToString());
        sw.WriteLine(alexNpc.isFixedTalk.ToString());
        sw.WriteLine(alexNpc.eventIndex.ToString());

        sw.WriteLine(nurseAnimator.transform.position.x.ToString());
        sw.WriteLine(nurseAnimator.transform.position.y.ToString());

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

        door_from_hos2_to_spe.isLock = bool.Parse(sr.ReadLine());

        doctorAnimator.GetComponent<Npc>().talkNum = int.Parse(sr.ReadLine());
        doctorAnimator.GetComponent<Npc>().isEvent = bool.Parse(sr.ReadLine());
        doctorAnimator.GetComponent<Npc>().isFixedTalk = bool.Parse(sr.ReadLine());
        doctorAnimator.GetComponent<Npc>().eventIndex = int.Parse(sr.ReadLine());

        alexNpc.talkNum = int.Parse(sr.ReadLine());
        alexNpc.isEvent = bool.Parse(sr.ReadLine());
        alexNpc.isFixedTalk = bool.Parse(sr.ReadLine());
        alexNpc.eventIndex = int.Parse(sr.ReadLine());

        float x = float.Parse(sr.ReadLine());
        float y=float.Parse(sr.ReadLine());

        nurseAnimator.transform.position = new Vector2(x,y);

        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);
        alexAnimator.See(AlexAnimator.SeeWhere.FRONT);


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
        alexAnimator.See(AlexAnimator.SeeWhere.FRONT);
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
        ChatManager.chatManager.OpenChat(75, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(1, true);
        KnowedManager.knowedManager.changeNowState(37);
        EventTriggers[0].SetActive(true);
    }

    private void StartSecondCutScene()
    {
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        ChatManager.chatManager.OpenChat(78, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        KnowedManager.knowedManager.changeNowState(40);
        doctorAnimator.GetComponent<Npc>().talkNum = 79;
        doctorAnimator.GetComponent<Npc>().isEvent = true;
        doctorAnimator.GetComponent<Npc>().eventIndex = 1;
        doctorAnimator.GetComponent<Npc>().isFixedTalk = false;
    }

    private void StartThirdCutScene()
    {
        StartCoroutine(ThirdCutSceneCoroutine());
    }

    private IEnumerator ThirdCutSceneCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        ChatManager.chatManager.OpenChat(80, EndThirdCutScene);
    }

    public void EndThirdCutScene()
    {
        KnowedManager.knowedManager.changeNowState(37);
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
                nurseAnimator.transform.position = new Vector2(-40, 3.5f);
                nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
                EventTriggers[1].SetActive(true);
                alexNpc.canTalk = true;
                alexNpc.isEvent = false;
                alexNpc.isFixedTalk = true;
                alexNpc.talkNum = 233;
                KnowedManager.knowedManager.changeNowState(39);
                door_from_hos2_to_spe.isLock = false;
                break;
            case 1:
                KnowedManager.knowedManager.changeNowState(41);
                doctorAnimator.GetComponent<Npc>().talkNum = 234;
                doctorAnimator.GetComponent<Npc>().isEvent = false;
                doctorAnimator.GetComponent<Npc>().isFixedTalk = true;
                doctorAnimator.GetComponent<Npc>().canTalk = true;
                EventTriggers[2].SetActive(true);
                door_from_hos2_to_spe.isLock = false;
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
                ChatManager.chatManager.OpenChat(76, null);
                break;
            case 1:
                EventTriggers[1].SetActive(false);
                StartSecondCutScene();
                break;
            case 2:
                EventTriggers[2].SetActive(false);
                StartThirdCutScene();
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
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        PlayerAnimation.playerAnimation.moveToY(290.35f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(298.64f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(291.12f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.Sit();
        yield return new WaitForSeconds(1.5f);
        PlayerAnimation.playerAnimation.SeeLeft();
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.SeeLeft();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.Stand();
        yield return new WaitForSeconds(1f);
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
        nurseAnimator.moveToY(2f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_8()
    {
        yield return new WaitForSeconds(0.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_9()
    {
        yield return new WaitForSeconds(0.5f);
        nurseAnimator.moveToX(-42.5f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        nurseAnimator.moveToY(9.5f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        nurseAnimator.transform.position = new Vector2(-7.5f, 13.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_10()
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

    private IEnumerator AnimationSet_11()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_12()
    {
        yield return new WaitForSeconds(0.3f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_13()
    {
        heroinAnimator.moveToX(-2f);
        yield return new WaitForSeconds(0.001f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinAnimator.moveToY(101.5f);
        yield return new WaitForSeconds(0.001f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }

        heroinAnimator.See(HeroinAnimator.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_14()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_15()
    {
        yield return new WaitForSeconds(0.3f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_16()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_17()
    {
        PlayerAnimation.playerAnimation.moveToX(-1f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(101.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_18()
    {
        PlayerAnimation.playerAnimation.moveToX(-1.3f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.Hug();
        heroinAnimator.Hug();
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

    private IEnumerator MoveToNextScene()
    {
        GameManager.canInput = false;

        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();

        FadeInOutBlack.fadeInOutBlack.SetColor(2);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);

        yield return new WaitForSeconds(5f);
        GameManager.gameManager.ShowTextOn("6일째 밤", 2);
        GameManager.gameManager.sceneNumber = 16;
        GameManager.gameManager.MoveScene("SixthNightScene", true);
    }


}
