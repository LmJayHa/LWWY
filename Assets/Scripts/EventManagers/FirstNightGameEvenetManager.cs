using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// 08/19 GameObject에 버츄얼 카메라 추가(용도 : 컷씬진행시 카메라 전환용)

public class FirstNightGameEvenetManager : GameEventManager
{

    public int chatCount;


    public bool isInCutScene;

    public TriggerDoor SpecialRoomDoor;

    public GameObject KnowedTuto;
    public GameObject Light;

    private float startTime;
    private float fadeValue;

    private void Awake()
    {

    }

    private void Start()
    {

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

        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);

        sr.Close();
    }







    private void StartFirstCutScene()
    {
        GameManager.canInput = false;
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(95f / 255f, 95f / 255f, 95f / 255f, 255f / 255f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(2, true);
        StartCoroutine(FirstCutSceneCoroutine());
    }

    private IEnumerator FirstCutSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.ShowTextOff();
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        ChatManager.chatManager.OpenChat(13, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        EventTriggers[0].SetActive(true);
        KnowedManager.knowedManager.changeNowState(5);
    }


    private void StartSecondCutScene()
    {
        GameManager.canInput = false;
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        ChatManager.chatManager.OpenChat(15, EndSecondCutScene);
        Light.SetActive(false);
    }

    public void EndSecondCutScene()
    {
        KnowedManager.knowedManager.changeNowState(7);
    }


    private void StartThirdCutScene()
    {
        GameManager.canInput = false;

        StartCoroutine(ThirdCutSceneCoroutine());
    }

    private IEnumerator ThirdCutSceneCoroutine()
    {
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();

        FadeInOutBlack.fadeInOutBlack.SetFadeIn(1f);
        yield return new WaitForSeconds(1.01f);
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(95f / 255f, 95f / 255f, 95f / 255f, 255f / 255f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        PlayerData.playerData.transform.position = new Vector2(6f, 0);
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        nurseAnimator.transform.position = new Vector2(9.5f, 0);
        GameManager.gameManager.lastSpace = GameManager.Space.HospitalSecond;
        GameManager.gameManager.changeFootStepSound();
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(1f);
        yield return new WaitForSeconds(1.01f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(2, true);
        ChatManager.chatManager.OpenChat(16, EndThirdCutScene);
    }

    public void EndThirdCutScene()
    {
        SpecialRoomDoor.toGo = GameManager.Space.SpecialRoom;
        SpecialRoomDoor.toGoX = -6.5f;
        SpecialRoomDoor.toGoY = 97f;
        SpecialRoomDoor.toSee = PlayerAnimation.SeeWhere.RIGHT;
        SpecialRoomDoor.isLock = false;

        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        KnowedManager.knowedManager.changeNowState(6);
        EventTriggers[2].SetActive(true);
    }

    public void StartFourthCutScene() { StartCoroutine(FourthCutSceneCoroutine()); }

    private IEnumerator FourthCutSceneCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        ChatManager.chatManager.OpenChat(17, EndFourthCutScene);
    }

    public void EndFourthCutScene()
    {
        EventTriggers[3].SetActive(true);
        SpecialRoomDoor.isLock = true;
        SpecialRoomDoor.lockChatNumber = 206;
        KnowedManager.knowedManager.changeNowState(8);
        PlayerData.playerData.isOpenKnoweds[0] = true;
        StartCoroutine(ShowTutorial_Tuto());
    }








    IEnumerator ShowTutorial_Tuto()
    {
        KnowedTuto.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        KnowedTuto.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        KnowedTuto.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        KnowedTuto.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        KnowedTuto.SetActive(true);
        yield return new WaitForSeconds(4f);
        KnowedTuto.SetActive(false);

    }









    public override void StartEvent_toNPC(int idx)
    {
        base.StartEvent_toNPC(idx);
        switch (idx)
        {
            case 0:
                StartThirdCutScene();
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
                EventTriggers[0].SetActive(false);
                EventTriggers[1].SetActive(true);
                ChatManager.chatManager.OpenChat(14, null);
                KnowedManager.knowedManager.changeNowState(6);
                break;
            case 1:
                EventTriggers[1].SetActive(false);
                StartSecondCutScene();
                break;
            case 2:
                EventTriggers[2].SetActive(false);
                StartFourthCutScene();
                break;
            case 3:
                StartCoroutine(MoveToNextScene());
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
            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_1()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        PlayerAnimation.playerAnimation.moveToY(5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.33f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.33f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        yield return new WaitForSeconds(0.33f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_7()
    {
        yield return new WaitForSeconds(0.33f);
        nurseAnimator.See(NurseAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_8()
    {
        yield return new WaitForSeconds(0.33f);
        nurseAnimator.See(NurseAnimator.SeeWhere.RIGHT);
        nurseAnimator.moveToX(11.5f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        SoundManager.soundManager.PlayDoorSound(0);
        nurseAnimator.transform.position = new Vector2(-2, 97);
        yield return new WaitForSeconds(1f);
        SoundManager.soundManager.PlayDoorSound(1);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_9()
    {
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.45f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.45f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_10()
    {
        yield return new WaitForSeconds(0.33f);
        nurseAnimator.See(NurseAnimator.SeeWhere.LEFT);
        nurseAnimator.moveToX(-5.5f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_11()
    {
        yield return new WaitForSeconds(0.33f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        PlayerAnimation.playerAnimation.moveToY(99.5f);
        yield return new WaitForSeconds(0.01f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.33f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        nurseAnimator.moveToX(-7.5f);
        yield return new WaitForSeconds(0.01f);
        while (nurseAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        SoundManager.soundManager.PlayDoorSound(0);
        nurseAnimator.transform.position = new Vector2(-100f, -100f);
        yield return new WaitForSeconds(1f);
        SoundManager.soundManager.PlayDoorSound(1);
        yield return new WaitForSeconds(1f);

        CallEndSignalToChat();
    }

    private IEnumerator MoveToNextScene()
    {
        GameManager.canInput = false;

        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();

        FadeInOutBlack.fadeInOutBlack.SetColor(1);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);

        yield return new WaitForSeconds(5f);
        GameManager.gameManager.ShowTextOn("2일째 낮", 1);
        GameManager.gameManager.sceneNumber = 3;
        GameManager.gameManager.MoveScene("SecondDayScene", true);
    }


}


