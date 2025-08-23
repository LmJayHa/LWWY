using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LastGameEventManager : GameEventManager
{
    //크레딧 용
    public Text[] EndingText = new Text[9];
    public Text[] EndingAddingText = new Text[9];
    public bool isFadeIn = false;
    public bool isFadeOut = false;
    private float startTime;
    private float addingTime;

    float r;
    float g;
    float b;
    int idx;


    public HeroinAnimator heroinAnimator;

    public bool isInCutScene;



    //15 애니용
    public HeroinKidAnimator heroinKidAnimator_1_f;
    public GameObject heroKid_1_f;
    public GameObject heroKid_1_s;
    public GameObject heroinKid_1_s;


    public HeroinKidAnimator heroinKidAnimator_2_s;
    public GameObject heroKid_2_f;
    public GameObject heroKid_2_s;
    public GameObject heroinKid_2_f;


    public DoctorAnimator doctorAnimator_3;
    public HeroinAnimator heroinAnimator_3_f;
    public MotherAnimator motherAnimator_3;
    public GameObject hero_3_f;
    public GameObject hero_3_s;
    public GameObject heroin_3_s;


    public DoctorAnimator doctorAnimator_4;
    public GameObject hero_4_f;
    public GameObject heroin_4_s;


    public GameObject hero_5_f;
    public GameObject heroin_5_s;

    public CamiliaAnimator camilia_1;
    public CamiliaAnimator camilia_2;

    public NurseAnimator nurse_1;
    public DoctorAnimator doctor_1;

    public GameObject Hero_B;
    public GameObject Heroin_B;
    public GameObject Hero_A;
    public GameObject Heroin_A;






    public GameObject[] Fogs = new GameObject[5];


    public Door door_from_hos2_to_hos1;

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

        sw.WriteLine(nurseAnimator.GetComponent<Npc>().talkNum.ToString());
        sw.WriteLine(nurseAnimator.GetComponent<Npc>().isEvent.ToString());
        sw.WriteLine(nurseAnimator.GetComponent<Npc>().isFixedTalk.ToString());
        sw.WriteLine(nurseAnimator.GetComponent<Npc>().eventIndex.ToString());

        sw.WriteLine(door_from_hos2_to_hos1.isLock.ToString());

        sw.WriteLine(PlayerAnimation.playerAnimation.GetComponent<PlayerInput>().canRun.ToString());
        sw.WriteLine(PlayerAnimation.playerAnimation.GetComponent<PlayerMovement>().canRun.ToString());

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

        nurseAnimator.GetComponent<Npc>().talkNum = int.Parse(sr.ReadLine());
        nurseAnimator.GetComponent<Npc>().isEvent = bool.Parse(sr.ReadLine());
        nurseAnimator.GetComponent<Npc>().isFixedTalk = bool.Parse(sr.ReadLine());
        nurseAnimator.GetComponent<Npc>().eventIndex = int.Parse(sr.ReadLine());

        door_from_hos2_to_hos1.isLock = bool.Parse(sr.ReadLine());

        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);

        PlayerAnimation.playerAnimation.GetComponent<PlayerInput>().canRun = bool.Parse(sr.ReadLine());
        PlayerAnimation.playerAnimation.GetComponent<PlayerMovement>().canRun = bool.Parse(sr.ReadLine());

        sr.Close();
    }







    private void StartFirstCutScene()
    {
        KnowedManager.knowedManager.changeNowState(42);
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
        ChatManager.chatManager.OpenChat(85, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(1, true);
        EventTriggers[0].SetActive(true);
    }

    private void StartSecondCutScene()
    {
        GameManager.canInput = false;
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2);
        yield return new WaitForSeconds(2f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(300,22.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2);
        yield return new WaitForSeconds(3f);
        ChatManager.chatManager.OpenChat(88, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        KnowedManager.knowedManager.changeNowState(44);
        PlayerData.playerData.isOpenKnoweds[8] = true;
    }

    private void StartThirdCutScene()
    {
        StartCoroutine(ThirdCutSceneCoroutine());
    }

    private IEnumerator ThirdCutSceneCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        ChatManager.chatManager.OpenChat(90, EndThirdCutScene);
    }

    public void EndThirdCutScene()
    {
        Application.Quit();
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
                door_from_hos2_to_hos1.isLock = false;
                nurseAnimator.GetComponent<Npc>().isEvent = false;
                nurseAnimator.GetComponent<Npc>().canTalk = true;
                nurseAnimator.GetComponent<Npc>().talkNum = 235;
                nurseAnimator.GetComponent<Npc>().isFixedTalk = true;
                KnowedManager.knowedManager.changeNowState(43);
                break;
            case 1:
                PlayerAnimation.playerAnimation.GetComponent<PlayerInput>().canRun = false;
                PlayerAnimation.playerAnimation.GetComponent<PlayerMovement>().canRun = false;
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
                ChatManager.chatManager.OpenChat(86, null);
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

        if (isFadeIn)
        {
            FadeIn();

        }
        else if (isFadeOut)
        {
            FadeOut();
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
            case 30:
                StartCoroutine(AnimationSet_30());
                break;
            case 31:
                StartCoroutine(AnimationSet_31());
                break;
            case 32:
                StartCoroutine(AnimationSet_32());
                break;
            case 33:
                StartCoroutine(AnimationSet_33());
                break;
            case 34:
                StartCoroutine(AnimationSet_34());
                break;
            case 35:
                StartCoroutine(AnimationSet_35());
                break;
            case 36:
                StartCoroutine(AnimationSet_36());
                break;
            case 37:
                StartCoroutine(AnimationSet_37());
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
        yield return new WaitForSeconds(1f);
        nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4() 
    {
        if (PlayerData.playerData.transform.position.x < -6.5)
        {
            PlayerAnimation.playerAnimation.moveToX(-9f);
            yield return new WaitForSeconds(0.001f);
            while (PlayerAnimation.playerAnimation.isMoving)
            {
                SoundManager.soundManager.PlayFootStepSounds();
                yield return new WaitForSeconds(0.01f);
            }
            PlayerAnimation.playerAnimation.moveToY(13.5f);
            yield return new WaitForSeconds(0.001f);
            while (PlayerAnimation.playerAnimation.isMoving)
            {
                SoundManager.soundManager.PlayFootStepSounds();
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.001f);
            nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        }
        else
        {
            PlayerAnimation.playerAnimation.moveToX(-5.5f);
            yield return new WaitForSeconds(0.001f);
            while (PlayerAnimation.playerAnimation.isMoving)
            {
                SoundManager.soundManager.PlayFootStepSounds();
                yield return new WaitForSeconds(0.01f);
            }
            PlayerAnimation.playerAnimation.moveToY(13.5f);
            yield return new WaitForSeconds(0.001f);
            while (PlayerAnimation.playerAnimation.isMoving)
            {
                SoundManager.soundManager.PlayFootStepSounds();
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.001f);
            nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        }
        yield return new WaitForSeconds(0.7f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        if (PlayerData.playerData.transform.position.x < -6.5)
        {
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        }
        else {
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        }
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
        if (PlayerData.playerData.transform.position.x < -6.5)
        {
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        }
        else
        {
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        }
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_8()
    {
        yield return new WaitForSeconds(0.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_9()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_10()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2);
        yield return new WaitForSeconds(2f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(300.5f,26.5f );
        heroinAnimator.transform.position = new Vector2(300.5f, 18.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2);
        yield return new WaitForSeconds(2.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_11()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.moveToY(24.5f);
        yield return new WaitForSeconds(0.001f);
        while (heroinAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_12()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_13()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_14()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_15()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2);

        
        yield return new WaitForSeconds(2f);
        PlayerVcam.playerVcam.VCam.SetActive(false);
        virtualCameras[1].SetActive(true);
        heroinKidAnimator_1_f.Sit();
        yield return new WaitForSeconds(3f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2);
        yield return new WaitForSeconds(2f);

        StartCoroutine(CameraMove(1, 246));
        yield return new WaitForSeconds(1f);
        heroinKidAnimator_1_f.Click();
        yield return new WaitForSeconds(1.5f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(0.5f);
        SoundManager.soundManager.PlayEffectClip(20);
        yield return new WaitForSeconds(0.5f);
        heroinKid_1_s.SetActive(true);
        heroKid_1_s.SetActive(true);
        heroinKidAnimator_1_f.gameObject.SetActive(false);
        heroKid_1_f.SetActive(false);
        Fogs[0].SetActive(false);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(0.5f);
        yield return new WaitForSeconds(3f);
        //1 끝

        FadeInOutBlack.fadeInOutBlack.SetFadeIn(1);


        yield return new WaitForSeconds(1f);
        virtualCameras[1].SetActive(false);
        virtualCameras[2].SetActive(true);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(1);
        yield return new WaitForSeconds(1f);

        StartCoroutine(CameraMove(2, 300));
        heroinKidAnimator_2_s.Sit();
        yield return new WaitForSeconds(2.5f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(0.5f);
        SoundManager.soundManager.PlayEffectClip(20);
        yield return new WaitForSeconds(0.5f);
        heroinKid_2_f.SetActive(false);
        heroKid_2_f.SetActive(false);
        heroinKidAnimator_2_s.transform.position =new Vector2(298,105.33f);
        heroKid_2_s.SetActive(true);
        Fogs[1].SetActive(false);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(0.5f);
        yield return new WaitForSeconds(1.5f);
        heroinKidAnimator_2_s.Stand();
        yield return new WaitForSeconds(1f);
        heroinKidAnimator_2_s.See(HeroinKidAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        //2 끝

        FadeInOutBlack.fadeInOutBlack.SetFadeIn(1);


        yield return new WaitForSeconds(1f);
        virtualCameras[2].SetActive(false);
        virtualCameras[3].SetActive(true);
        heroinAnimator_3_f.See(HeroinAnimator.SeeWhere.LEFT);
        motherAnimator_3.See(MotherAnimator.SeeWhere.LEFT);
        doctorAnimator_3.See(DoctorAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(1);
        yield return new WaitForSeconds(1f);

        StartCoroutine(CameraMove(3, 343.5f));
        yield return new WaitForSeconds(1f);
        heroinAnimator_3_f.See(HeroinAnimator.SeeWhere.FRONT);
        motherAnimator_3.See(MotherAnimator.SeeWhere.BACK);
        yield return new WaitForSeconds(1.5f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(0.5f);
        SoundManager.soundManager.PlayEffectClip(20);
        yield return new WaitForSeconds(0.5f);
        heroinAnimator_3_f.gameObject.SetActive(false);
        hero_3_f.SetActive(false);
        hero_3_s.SetActive(true);
        heroin_3_s.SetActive(true);
        Fogs[2].SetActive(false);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(0.5f);
        yield return new WaitForSeconds(3f);
        //3끝

        FadeInOutBlack.fadeInOutBlack.SetFadeIn(1);


        yield return new WaitForSeconds(1f);
        virtualCameras[3].SetActive(false);
        virtualCameras[4].SetActive(true);
        doctorAnimator_4.See(DoctorAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(1);
        yield return new WaitForSeconds(1f);

        StartCoroutine(CameraMove(4, 392f));
        yield return new WaitForSeconds(2.5f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(0.5f);
        SoundManager.soundManager.PlayEffectClip(20);
        yield return new WaitForSeconds(0.5f);
        hero_4_f.SetActive(false);
        heroin_4_s.SetActive(true);
        Fogs[3].SetActive(false);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(0.5f);
        yield return new WaitForSeconds(3f);
        //4끝

        FadeInOutBlack.fadeInOutBlack.SetFadeIn(1);


        yield return new WaitForSeconds(1f);
        virtualCameras[4].SetActive(false);
        virtualCameras[5].SetActive(true);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(1);
        yield return new WaitForSeconds(1f);

        StartCoroutine(CameraMove(5, 460f));
        yield return new WaitForSeconds(2.5f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(0.5f);
        SoundManager.soundManager.PlayEffectClip(20);
        yield return new WaitForSeconds(0.5f);
        hero_5_f.SetActive(false);
        heroin_5_s.SetActive(true);
        Fogs[4].SetActive(false);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(0.5f);
        yield return new WaitForSeconds(3f);
        //5 끝

        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2);
        yield return new WaitForSeconds(2f);
        virtualCameras[5].SetActive(false);
        virtualCameras[7].SetActive(true);
        PlayerVcam.playerVcam.VCam.SetActive(true);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(301.5f, 26);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        heroinAnimator.transform.position = new Vector2(299.5f, 26);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2);
        yield return new WaitForSeconds(2f);


        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_16()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_17()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_18()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_19()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2);
        yield return new WaitForSeconds(2f);
        virtualCameras[7].SetActive(false);
        virtualCameras[8].SetActive(true);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2);
        yield return new WaitForSeconds(2.5f);

        camilia_1.moveToY(111.34f);
        yield return new WaitForSeconds(0.001f);
        while (camilia_1.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        camilia_1.See(CamiliaAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        camilia_1.See(CamiliaAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);

        camilia_1.moveToX(526.15f);
        while (camilia_1.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        camilia_1.See(CamiliaAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        camilia_1.See(CamiliaAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        camilia_1.See(CamiliaAnimator.SeeWhere.BACK);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_20()
    {
        FadeInOutBlack.fadeInOutBlack.SetColor(3);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2);
        yield return new WaitForSeconds(2f);
        virtualCameras[8].SetActive(false);
        virtualCameras[9].SetActive(true);
        nurse_1.See(NurseAnimator.SeeWhere.FRONT);
        doctor_1.See(DoctorAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_21() {
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2);
        yield return new WaitForSeconds(3f);
        nurse_1.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_22()
    {
        yield return new WaitForSeconds(0.5f);
        nurse_1.See(NurseAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        nurse_1.See(NurseAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_23()
    {
        yield return new WaitForSeconds(2f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_24()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(0.5f);
        SoundManager.soundManager.PlayEffectClip(20);
        yield return new WaitForSeconds(0.5f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(0.5f);
        yield return new WaitForSeconds(1.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_25()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(3f);
        yield return new WaitForSeconds(17f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_26()
    {
        camilia_2.See(CamiliaAnimator.SeeWhere.BACK);
        virtualCameras[9].SetActive(false);
        virtualCameras[11].SetActive(true);
        yield return new WaitForSeconds(3f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2.5f);
        camilia_2.See(CamiliaAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        camilia_2.See(CamiliaAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        camilia_2.See(CamiliaAnimator.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_27()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2f);
        virtualCameras[11].SetActive(false);
        virtualCameras[10].SetActive(true);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_28()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_29()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(0.5f);
        SoundManager.soundManager.PlayEffectClip(20);
        yield return new WaitForSeconds(0.5f);
        Hero_B.SetActive(false);
        Heroin_B.SetActive(false);
        Hero_A.SetActive(true);
        Heroin_A.SetActive(true);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(0.5f);
        yield return new WaitForSeconds(3f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2f);
        virtualCameras[10].SetActive(false);
        virtualCameras[7].SetActive(true);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_30()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_31()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_32()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_33()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_34()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.moveToX(300.25f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_35()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.Hug();
        heroinAnimator.Hug();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_36()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(5f);
        yield return new WaitForSeconds(10f);
        EventTriggers[2].SetActive(true);
        virtualCameras[7].SetActive(false);
        PlayerAnimation.playerAnimation.UnHug();
        PlayerAnimation.playerAnimation.transform.position = new Vector2(-3.97f,103.72f);
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(243f / 255f, 172f / 255f, 107f / 255f, 255f / 255f);
        GameManager.gameManager.lastSpace = GameManager.Space.SpecialRoom;
        GameManager.gameManager.changeFootStepSound();
        yield return new WaitForSeconds(2f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        PlayerAnimation.playerAnimation.changeState(2);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_37()
    {
        FadeInOutBlack.fadeInOutBlack.SetColor(4);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(3f);
        SoundManager.soundManager.PlayDoorSound(0);
        yield return new WaitForSeconds(4f);



        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(9, true);
        yield return new WaitForSeconds(2f);
        FadeInText(0, 2f);
        yield return new WaitForSeconds(4f);
        FadeOutText(0, 2f);
        yield return new WaitForSeconds(5f);


        FadeInText(1, 2f);
        yield return new WaitForSeconds(4f);
        FadeOutText(1, 2f);
        yield return new WaitForSeconds(5f);

        FadeInText(2, 2f);
        yield return new WaitForSeconds(4f);
        FadeOutText(2, 2f);
        yield return new WaitForSeconds(5f);

        FadeInText(3, 2f);
        yield return new WaitForSeconds(4f);
        FadeOutText(3, 2f);
        yield return new WaitForSeconds(5f);

        FadeInText(4, 2f);
        yield return new WaitForSeconds(4f);
        FadeOutText(4, 2f);
        yield return new WaitForSeconds(5f);

        FadeInText(5, 2f);
        yield return new WaitForSeconds(4f);
        FadeOutText(5, 2f);
        yield return new WaitForSeconds(5f);

        FadeInText(6, 2f);
        yield return new WaitForSeconds(4f);
        FadeOutText(6, 2f);
        yield return new WaitForSeconds(5f);

        FadeInText(7, 2f);
        yield return new WaitForSeconds(8f);
        FadeOutText(7, 2f);
        yield return new WaitForSeconds(8f);

        FadeInText(8, 1f);
        yield return new WaitForSeconds(1f);
        FadeOutText(8, 1f);
        yield return new WaitForSeconds(1f);

        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
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
    private IEnumerator CameraMove(int idx, float endX)
    {
        float time=Time.time;
        while (virtualCameras[idx].transform.position.x < endX)
        {
            virtualCameras[idx].transform.Translate(new Vector2((Time.time-time) * 0.5f, 0f));
            time = Time.time;
            yield return new WaitForSeconds(0.01f);
        }
    }


    private void FadeInText(int _idx,float adding) {
        idx = _idx;
        r = 255f/255f;
        g = 255f / 255f;
        b = 255f / 255f;
        EndingText[idx].gameObject.SetActive(true);
        EndingAddingText[idx].gameObject.SetActive(true);
        EndingText[idx].color = new Color(r, g, b, 0);
        EndingAddingText[idx].color = new Color(r, g, b, 0);
        addingTime = adding;
        startTime = Time.time;
        isFadeIn = true;
    }

    private void FadeOutText(int _idx, float adding) {
        idx = _idx;
        r = 255f / 255f;
        g = 255f / 255f;
        b = 255f / 255f;
        EndingText[idx].color = new Color(r, g, b, 1);
        EndingAddingText[idx].color = new Color(r, g, b, 1);
        addingTime = adding;
        startTime = Time.time;
        isFadeOut = true;

    }

    private void FadeIn()
    {
        if (startTime + addingTime > Time.time)
        {
            float Setting = EndingText[idx].color.a + Time.deltaTime / addingTime;
            EndingText[idx].color = new Color(r, g, b, Setting);
            float _Setting = EndingAddingText[idx].color.a + Time.deltaTime / addingTime;
            EndingAddingText[idx].color = new Color(r, g, b, _Setting);
        }
        else
        {
            EndingText[idx].color = new Color(r, g, b, 1);
            EndingAddingText[idx].color = new Color(r, g, b, 1);
            isFadeIn = false;
        }
    }

    private void FadeOut()
    {
        if (startTime + addingTime > Time.time)
        {
            float Setting = EndingText[idx].color.a - Time.deltaTime / addingTime;
            EndingText[idx].color = new Color(r, g, b, Setting);
            float _Setting = EndingAddingText[idx].color.a - Time.deltaTime / addingTime;
            EndingAddingText[idx].color = new Color(r, g, b, _Setting);
        }
        else
        {
            EndingText[idx].color = new Color(r, g, b, 0);
            EndingAddingText[idx].color = new Color(r, g, b, 0);
            EndingText[idx].gameObject.SetActive(false);
            EndingAddingText[idx].gameObject.SetActive(false);
            isFadeOut = false;
        }
    }



}
