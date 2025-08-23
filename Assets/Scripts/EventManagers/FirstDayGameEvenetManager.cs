using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// 08/19 GameObject에 버츄얼 카메라 추가(용도 : 컷씬진행시 카메라 전환용)

public class FirstDayGameEvenetManager : GameEventManager
{

    public bool isInCutScene;

    public Image BlackUI;
    public GameObject MovingTutorialUI;
    public GameObject InventoryTutorialUI;

    public TriggerDoor DoorToOutSide;

    private float startTime;
    private float fadeValue;

    private void Awake()
    {
        isInCutScene = false;
        isFadingOutBlackUI = false;
    }

    private void Start()
    {
        FadeInOutBlack.fadeInOutBlack.SetColor(1);
    }








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

        sw.WriteLine(nurseAnimator.transform.position.x);
        sw.WriteLine(nurseAnimator.transform.position.y);

        sw.WriteLine(nurseAnimator.transform.GetComponent<Npc>().canTalk.ToString());
        sw.WriteLine(nurseAnimator.transform.GetComponent<Npc>().isFixedTalk.ToString());
        sw.WriteLine(nurseAnimator.transform.GetComponent<Npc>().talkNum.ToString());
        sw.WriteLine(nurseAnimator.transform.GetComponent<Npc>().eventIndex.ToString());
        sw.WriteLine(nurseAnimator.transform.GetComponent<Npc>().isEvent.ToString());

        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().canTalk.ToString());
        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().isFixedTalk.ToString());
        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().talkNum.ToString());
        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().eventIndex.ToString());
        sw.WriteLine(doctorAnimator.transform.GetComponent<Npc>().isEvent.ToString());
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

        float x = float.Parse(sr.ReadLine());
        float y = float.Parse(sr.ReadLine());

        nurseAnimator.transform.position = new Vector3(x, y, 0f);
        Debug.Log("간호사 이동" + x + " " + y);

        nurseAnimator.transform.GetComponent<Npc>().canTalk = bool.Parse(sr.ReadLine());
        nurseAnimator.transform.GetComponent<Npc>().isFixedTalk = bool.Parse(sr.ReadLine());
        nurseAnimator.transform.GetComponent<Npc>().talkNum = int.Parse(sr.ReadLine());
        nurseAnimator.transform.GetComponent<Npc>().eventIndex = int.Parse(sr.ReadLine());
        nurseAnimator.transform.GetComponent<Npc>().isEvent = bool.Parse(sr.ReadLine());
        Debug.Log("간호사 대화 변경" + nurseAnimator.transform.GetComponent<Npc>().talkNum);

        doctorAnimator.transform.GetComponent<Npc>().canTalk = bool.Parse(sr.ReadLine());
        doctorAnimator.transform.GetComponent<Npc>().isFixedTalk = bool.Parse(sr.ReadLine());
        doctorAnimator.transform.GetComponent<Npc>().talkNum = int.Parse(sr.ReadLine());
        doctorAnimator.transform.GetComponent<Npc>().eventIndex = int.Parse(sr.ReadLine());
        doctorAnimator.transform.GetComponent<Npc>().isEvent = bool.Parse(sr.ReadLine());
        Debug.Log("의사 대화 변경" + doctorAnimator.transform.GetComponent<Npc>().talkNum);

        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        Debug.Log("간호사,의사 보는 곳 변경");
        sr.Close();
    }








    public void firstCutScene()
    {
        isInCutScene = true;
        GameManager.canInput = false;
        BlackUI.gameObject.SetActive(true);
        ChatManager.chatManager.OpenChat(0, firstCutSceneEnd);
    }


    private bool isFadingOutBlackUI;
    public void firstCutSceneEnd()
    {
        isFirst = false;

        SoundManager.soundManager.FootStepIdx = 0;

        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);

        isFadingOutBlackUI = true;
        startTime = Time.time;
        fadeValue = 255f;
        isInCutScene = false;
    }

    private void FadeOutBlackUI()
    {
        if (Time.time <= startTime + 2f)
        {
            fadeValue -= 255f * Time.deltaTime / 2f;
            BlackUI.color = new Color(0, 0, 0, fadeValue / 255f);
        }
        else
        {
            isFadingOutBlackUI = false;
            BlackUI.gameObject.SetActive(false);
            ChatManager.chatManager.OpenChat(1, first_CutSceneEnd);
            isInCutScene = true;
        }
    }

    public void first_CutSceneEnd()
    {
        nurseAnimator.transform.position = new Vector2(-7.5f, 13.5f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);

        isInCutScene = false;
        GameManager.canInput = true;
        StartCoroutine(ShowTutorial());
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(1, true);
        KnowedManager.knowedManager.changeNowState(0);
    }


    public void secondCutScene()
    {
        isInCutScene = true;
    }

    public void secondCutSceneEnd()
    {

        DoorToOutSide.lockChatNumber = 204;

        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);
        doctorAnimator.transform.GetComponent<Npc>().canTalk = true;
        doctorAnimator.transform.GetComponent<Npc>().talkNum = 4;
        doctorAnimator.transform.GetComponent<Npc>().isFixedTalk = true;
        doctorAnimator.transform.GetComponent<Npc>().isEvent = false;

        nurseAnimator.transform.GetComponent<Npc>().talkNum = 5;
        nurseAnimator.transform.GetComponent<Npc>().isFixedTalk = true;
        nurseAnimator.transform.GetComponent<Npc>().isEvent = false;

        EventTriggers[0].SetActive(true);

        isInCutScene = false;
        KnowedManager.knowedManager.changeNowState(1);
    }





    public override void StartEvent_toNPC(int idx)
    {
        base.StartEvent_toNPC(idx);
        switch (idx)
        {
            case 0:
                secondCutScene();
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
                secondCutSceneEnd();
                break;
            case 1:
                PlayerData.playerData.addItem(0);
                StartCoroutine(ShowTutorial_Inven());
                KnowedManager.knowedManager.changeNowState(2);
                break;
            case 2:
                nurseAnimator.GetComponent<Npc>().talkNum = 11;
                nurseAnimator.GetComponent<Npc>().isFixedTalk = false;
                nurseAnimator.GetComponent<Npc>().canTalk = true;
                nurseAnimator.GetComponent<Npc>().isEvent = true;
                nurseAnimator.GetComponent<Npc>().eventIndex = 3;
                nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
                KnowedManager.knowedManager.changeNowState(3);
                break;
            case 3:
                nurseAnimator.GetComponent<Npc>().talkNum = 12;
                nurseAnimator.GetComponent<Npc>().isFixedTalk = true;
                nurseAnimator.GetComponent<Npc>().canTalk = true;
                nurseAnimator.GetComponent<Npc>().isEvent = false;
                EventTriggers[3].SetActive(true);
                nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
                KnowedManager.knowedManager.changeNowState(4);
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
                ChatManager.chatManager.OpenChat(6, null);
                EventTriggers[1].SetActive(true);
                break;
            case 1:
                ChatManager.chatManager.OpenChat(7, null);
                EventTriggers[2].SetActive(true);
                break;
            case 2:
                ChatManager.chatManager.OpenChat(8, null);
                nurseAnimator.GetComponent<Npc>().talkNum = 9;
                nurseAnimator.GetComponent<Npc>().isFixedTalk = false;
                nurseAnimator.GetComponent<Npc>().canTalk = true;
                nurseAnimator.GetComponent<Npc>().isEvent = true;
                nurseAnimator.GetComponent<Npc>().eventIndex = 1;

                doctorAnimator.GetComponent<Npc>().talkNum = 10;
                doctorAnimator.GetComponent<Npc>().isFixedTalk = true;
                doctorAnimator.GetComponent<Npc>().canTalk = true;
                break;
            case 3:
                GameManager.gameManager.sceneNumber = 1;
                StartCoroutine(MoveToNextScene());
                //1일차 낮 끝
                break;
            default:
                break;
        }
    }







    private void Update()
    {
        if (isInCutScene) return;


        if (isFirst)
        {
            firstCutScene();
        }

        if (isFadingOutBlackUI)
        {
            FadeOutBlackUI();
        }


    }










    IEnumerator ShowTutorial()
    {
        MovingTutorialUI.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        MovingTutorialUI.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        MovingTutorialUI.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        MovingTutorialUI.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        MovingTutorialUI.SetActive(true);
        yield return new WaitForSeconds(4f);
        MovingTutorialUI.SetActive(false);

    }

    IEnumerator ShowTutorial_Inven()
    {
        InventoryTutorialUI.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        InventoryTutorialUI.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        InventoryTutorialUI.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        InventoryTutorialUI.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        InventoryTutorialUI.SetActive(true);
        yield return new WaitForSeconds(4f);
        InventoryTutorialUI.SetActive(false);

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
            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_1()
    {
        nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(2f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
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
        yield return new WaitForSeconds(0.01f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
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

    private IEnumerator AnimationSet_4()
    {
        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(2f);
        doctorAnimator.See(DoctorAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        PlayerAnimation.playerAnimation.moveToX(8.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(2f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_7() // 간호사가 약 다됐다고 해서 간호사 비춰주는 장면
    {
        nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(2f);
        CallEndSignalToChat();
    }



    private IEnumerator AnimationSet_8() // 약먹고 말을걸때 쓸 씬
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

    private IEnumerator AnimationSet_9() // 약먹음
    {
        if (PlayerData.playerData.transform.position.x < -6.5)
        {
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
            yield return new WaitForSeconds(2f);
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        }
        else
        {
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
            yield return new WaitForSeconds(2f);
            PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        }
        yield return new WaitForSeconds(0.7f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_10() //잠에서 깨고난뒤에 쓸것
    {
        PlayerAnimation.playerAnimation.moveToY(8f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(-4.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToY(0f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.moveToX(-6f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(2f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_11() // 밤에 특실 처음 들어갔을때
    {
        PlayerAnimation.playerAnimation.moveToX(-5.4f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
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
        GameManager.gameManager.ShowTextOn("1일째 밤", 2);
        GameManager.gameManager.sceneNumber = 1;
        GameManager.gameManager.MoveScene("FirstNightScene", true);
    }
}


