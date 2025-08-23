using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class FifthDayGameEventManager : GameEventManager
{
    public Tilemap[] tileMaps = new Tilemap[5];
    public SpriteRenderer heroinSpriteRenderer;

    public GameObject Lamp;

    public HeroinAnimator heroinAnimator;

    public bool isInCutScene;

    public GameObject RememberImage;

    public Door door_to_hos1_from_hos2;
    public TriggerDoor door_to_out_from_hos1;
    public TriggerDoor door_to_spe_from_hos2;


    public Npc FlowerShopNpc;
    public Npc MarketNpc;

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
        sw.WriteLine(door_to_hos1_from_hos2.isLock.ToString());
        sw.WriteLine(door_to_out_from_hos1.isLock.ToString());
        sw.WriteLine(door_to_spe_from_hos2.isLock.ToString());

        sw.WriteLine(FlowerShopNpc.talkNum.ToString());
        sw.WriteLine(FlowerShopNpc.isEvent.ToString());
        sw.WriteLine(FlowerShopNpc.isFixedTalk.ToString());
        sw.WriteLine(FlowerShopNpc.eventIndex.ToString());

        sw.WriteLine(MarketNpc.talkNum.ToString());
        sw.WriteLine(MarketNpc.isEvent.ToString());
        sw.WriteLine(MarketNpc.isFixedTalk.ToString());
        sw.WriteLine(MarketNpc.eventIndex.ToString());

        sw.WriteLine(heroinAnimator.transform.position.x);
        sw.WriteLine(heroinAnimator.transform.position.y);

        sw.WriteLine(nurseAnimator.transform.position.x);
        sw.WriteLine(nurseAnimator.transform.position.y);

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
        door_to_hos1_from_hos2.isLock = bool.Parse(sr.ReadLine());
        door_to_out_from_hos1.isLock = bool.Parse(sr.ReadLine());
        door_to_spe_from_hos2.isLock = bool.Parse(sr.ReadLine());

        FlowerShopNpc.talkNum = int.Parse(sr.ReadLine());
        FlowerShopNpc.isEvent = bool.Parse(sr.ReadLine());
        FlowerShopNpc.isFixedTalk = bool.Parse(sr.ReadLine());
        FlowerShopNpc.eventIndex = int.Parse(sr.ReadLine());

        MarketNpc.talkNum = int.Parse(sr.ReadLine());
        MarketNpc.isEvent = bool.Parse(sr.ReadLine());
        MarketNpc.isFixedTalk = bool.Parse(sr.ReadLine());
        MarketNpc.eventIndex = int.Parse(sr.ReadLine());

        float x = float.Parse(sr.ReadLine());
        float y = float.Parse(sr.ReadLine());

        heroinAnimator.transform.position = new Vector2(x, y);


        x = float.Parse(sr.ReadLine());
        y = float.Parse(sr.ReadLine());

        nurseAnimator.transform.position = new Vector2(x, y);

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
        ChatManager.chatManager.OpenChat(66, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(1, true);
        KnowedManager.knowedManager.changeNowState(33);
        EventTriggers[0].SetActive(true);
    }

    private void StartSecondCutScene()
    {
        GameManager.canInput = false;
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        ChatManager.chatManager.OpenChat(67, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(1, true);
        KnowedManager.knowedManager.changeNowState(30);
        door_to_spe_from_hos2.isLock = true;
        door_to_hos1_from_hos2.isLock = false;
        PlayerData.playerData.isOpenKnoweds[5] = true;
    }

    private void StartThirdCutScene()
    {
        GameManager.canInput = false;
        StartCoroutine(ThirdCutSceneCoroutine());
    }

    private IEnumerator ThirdCutSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        ChatManager.chatManager.OpenChat(70, EndThirdCutScene);
    }

    public void EndThirdCutScene()
    {
        KnowedManager.knowedManager.changeNowState(27);
        door_to_spe_from_hos2.isLock = false;
    }

    private void StartForthCutScene()
    {
        GameManager.canInput = false;
        StartCoroutine(ForthCutSceneCoroutine());
    }

    private IEnumerator ForthCutSceneCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        ChatManager.chatManager.OpenChat(71, EndForthCutScene);
    }

    public void EndForthCutScene()
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
                //인벤토리 아이템 추가

                FlowerShopNpc.isEvent = false;
                FlowerShopNpc.talkNum = 221;
                FlowerShopNpc.canTalk = true;
                FlowerShopNpc.isFixedTalk = true;

                MarketNpc.isEvent = true;
                MarketNpc.talkNum = 69;
                MarketNpc.canTalk = true;
                MarketNpc.isFixedTalk = false;
                MarketNpc.eventIndex = 1;
                KnowedManager.knowedManager.changeNowState(34);
                break;
            case 1:
                //인벤토리 아이템 추가
                MarketNpc.isEvent = false;
                MarketNpc.talkNum = 209;
                MarketNpc.canTalk = false;
                MarketNpc.isFixedTalk = true;

                EventTriggers[1].SetActive(true);

                nurseAnimator.transform.position = new Vector2(-37f,17f);
                KnowedManager.knowedManager.changeNowState(35);
                door_to_out_from_hos1.isLock = true;
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
                EventTriggers[2].SetActive(true);
                heroinAnimator.transform.position = new Vector2(0, 144);
                KnowedManager.knowedManager.changeNowState(36);
                StartThirdCutScene();
                break;
            case 2:
                EventTriggers[2].SetActive(false);
                StartForthCutScene();
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
            case 19:
                StartCoroutine(AnimationSet_19());
                break;
            case 20:
                StartCoroutine(AnimationSet_20());
                break;
            case 21:
                StartCoroutine(AnimationSet_21());
                break;
            case 22:
                StartCoroutine(AnimationSet_22());
                break;
            case 23:
                StartCoroutine(AnimationSet_23());
                break;
            case 24:
                StartCoroutine(AnimationSet_24());
                break;
            case 25:
                StartCoroutine(AnimationSet_25());
                break;
            case 26:
                StartCoroutine(AnimationSet_26());
                break;
            case 27:
                StartCoroutine(AnimationSet_27());
                break;
            case 28:
                StartCoroutine(AnimationSet_28());
                break;
            case 29:
                StartCoroutine(AnimationSet_29());
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
        nurseAnimator.moveToX(2.5f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        nurseAnimator.moveToY(10.5f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        nurseAnimator.moveToY(8f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        nurseAnimator.moveToX(-5.5f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        nurseAnimator.transform.position = new Vector2(-7.5f, 13.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        heroinAnimator.moveToY(98f);
        yield return new WaitForSeconds(0.01f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinAnimator.moveToX(3.5f);
        yield return new WaitForSeconds(0.01f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinAnimator.moveToY(99f);
        yield return new WaitForSeconds(0.01f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.Sit();


        PlayerAnimation.playerAnimation.moveToY(98f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(2.5f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(99f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.Sit();
        yield return new WaitForSeconds(1f);
        heroinAnimator.SeeLeft();
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.SeeLeft();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_7()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.SeeRight();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_8()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_9()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_10()
    {
        
        yield return new WaitForSeconds(0.5f);
        float time = Time.time;
        while (true) {
            yield return new WaitForSeconds(0.001f);
            virtualCameras[0].transform.Translate(-0.75f * (Time.time - time), 0,0);
            if (virtualCameras[0].transform.position.x <= 1) { break; }
            else { time = Time.time; }
        }
        FadeInOutBlack.fadeInOutBlack.SetColor(2);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2.5f);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(0.5f);
        SoundManager.soundManager.PlayEffectClip(17);
        RememberImage.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        RememberImage.SetActive(false);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(0.5f);
        yield return new WaitForSeconds(0.7f);

        PlayerAnimation.playerAnimation.Stand();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_11()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.Stand();
        yield return new WaitForSeconds(1f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_12()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_13()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_14()
    {

        PlayerAnimation.playerAnimation.moveToY(98f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(-5.5f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(97f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(-6.75f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        SoundManager.soundManager.PlayDoorSound(0);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(10.5f, 0f);
        FadeInOutBlack.fadeInOutBlack.SetColor(1);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(3f);
        GameManager.gameManager.lastSpace = GameManager.Space.HospitalSecond;
        GameManager.gameManager.changeFootStepSound();

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_15()
    {

        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2.5f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_16()
    {

        PlayerAnimation.playerAnimation.moveToX(34f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(4f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }

        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_17()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_18()
    {

        PlayerAnimation.playerAnimation.moveToX(-136.5f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(8f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }

        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_19()
    {

        PlayerAnimation.playerAnimation.moveToX(-37f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(13f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }

        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_20()
    {
        nurseAnimator.moveToY(15f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_21()
    {

        yield return new WaitForSeconds(0.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_22()
    {

        yield return new WaitForSeconds(0.5f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(-42.5f, 12);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        nurseAnimator.transform.position = new Vector2(-7.5f, 13.5f);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_23()
    {

        PlayerAnimation.playerAnimation.moveToX(-2f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(97f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_24()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_25()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(52, 96);
        SoundManager.soundManager.PlayEffectClip(14);
        yield return new WaitForSeconds(0.5f);
        SoundManager.soundManager.PlayEffectClip(18);
        yield return new WaitForSeconds(1f);
        SoundManager.soundManager.PlayEffectClip(14);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);

        heroinAnimator.transform.position = new Vector2(52, 98);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);

        yield return new WaitForSeconds(2f);

        SoundManager.soundManager.PlayDoorSound(0);
        yield return new WaitForSeconds(1f);
        SoundManager.soundManager.PlayDoorSound(1);
        yield return new WaitForSeconds(2f);

        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2.5f);

        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_26()
    {
        heroinAnimator.moveToY(98f);
        yield return new WaitForSeconds(0.01f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinAnimator.moveToX(55.325f);
        yield return new WaitForSeconds(0.01f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinAnimator.moveToY(99f);
        yield return new WaitForSeconds(0.01f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.Sit();
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }


private IEnumerator AnimationSet_27()
    {
        //보류
        SoundManager.soundManager.PlayEffectClip(18);
         PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(95f / 255f, 95f / 255f, 95f / 255f, 255f / 255f);
         heroinSpriteRenderer.color = new Color(95f / 255f, 95f / 255f, 95f / 255f, 255f / 255f);
         for (int i = 0; i < 5; i++)
         {
              tileMaps[i].color = new Color(95f / 255f, 95f / 255f, 95f / 255f, 255f / 255f);
          }
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_28()
    {
        Lamp.gameObject.SetActive(true);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(9,true);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_29()
    {
        PlayerAnimation.playerAnimation.moveToY(98f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(54.3f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(99f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.Sit();
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
        GameManager.gameManager.ShowTextOn("5일째 밤", 2);
        GameManager.gameManager.sceneNumber = 13;
        GameManager.gameManager.MoveScene("FifthNightScene", true);
    }


}
