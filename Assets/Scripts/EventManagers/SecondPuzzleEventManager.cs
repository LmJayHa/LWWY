using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class SecondPuzzleEventManager : GameEventManager
{

    public HeroinKidAnimator heroinKidAnimator;
    public MotherAnimator motherAnimator;


    public UIOpener LightPan;

    public GameObject MusicBox;


    private void Update()
    {
        if (isFirst)
        {
            isFirst = false;
            StartFirstCutScene();
        }


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

        sw.WriteLine(heroinKidAnimator.transform.position.x.ToString());
        sw.WriteLine(heroinKidAnimator.transform.position.y.ToString());

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

        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);

        float x = float.Parse(sr.ReadLine());
        float y = float.Parse(sr.ReadLine());

        heroinKidAnimator.transform.position = new Vector2(x, y);

        sr.Close();
    }









    private void StartFirstCutScene()
    {
        
        GameManager.canInput = false;
        SoundManager.soundManager.PlayDoorSound(1);
        GameManager.gameManager.lastSpace = GameManager.Space.Puzzle;
        GameManager.gameManager.changeFootStepSound();
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        StartCoroutine(FirstCutSceneCoroutine());
    }

    private IEnumerator FirstCutSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.ShowTextOff();
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        ChatManager.chatManager.OpenChat(37, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(3, true);
        KnowedManager.knowedManager.changeNowState(20);
        EventTriggers[0].SetActive(true);
    }



    private void StartSecondCutScene()
    {

        LightPan.CloseUI();
        GameManager.canInput = false;
        SoundManager.soundManager.PlayEffectClip(17);
        GameManager.gameManager.lastSpace = GameManager.Space.SpecialRoom;
        GameManager.gameManager.changeFootStepSound();
        Debug.Log("성공 컷신 시작");
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("성공 컷신 시작 오픈");
        ChatManager.chatManager.OpenChat(38, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        heroinKidAnimator.transform.position = new Vector2(8,51);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.LEFT);
        KnowedManager.knowedManager.changeNowState(21);
    }


    private void StarThirdCutScene()
    {

        GameManager.canInput = false;
        StartCoroutine(ThirdCutSceneCoroutine());
    }

    private IEnumerator ThirdCutSceneCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("성공 컷신 시작 오픈");
        ChatManager.chatManager.OpenChat(39, EndThirdCutScene);
    }

    public void EndThirdCutScene()
    {
        GameManager.gameManager.lastSpace = GameManager.Space.HospitalSecond;
        GameManager.gameManager.changeFootStepSound();
        PlayerData.playerData.isOpenKnoweds[1] = true;
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
                EventTriggers[0].SetActive(false);
                StarThirdCutScene();
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
            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        PlayerAnimation.playerAnimation.moveToY(13f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_1()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_2()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        CameraShake.cameraShake.Shake(200f, 0.15f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(19, true);
        //지진 소리 + 화면움직임
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2f);
        CameraShake.cameraShake.ShakeTime = 0;
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        PlayerAnimation.playerAnimation.changeState(1);//꼬마로 변경
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(130,0);
        motherAnimator.See(MotherAnimator.SeeWhere.LEFT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(2f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_7()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        motherAnimator.See(MotherAnimator.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_8()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(6, true);
        heroinKidAnimator.runToY(2f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);

        heroinKidAnimator.runToX(131f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);

        heroinKidAnimator.runToY(0f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.LEFT);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        motherAnimator.See(MotherAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_9()
    {
        heroinKidAnimator.runToY(2f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        motherAnimator.See(MotherAnimator.SeeWhere.BACK);
        heroinKidAnimator.runToX(129f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);

        heroinKidAnimator.runToY(7f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        motherAnimator.See(MotherAnimator.SeeWhere.LEFT);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_10()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_11()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_12()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_13()
    {
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.LEFT);
        PlayerAnimation.playerAnimation.moveToY(48.5f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        PlayerAnimation.playerAnimation.moveToX(7f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_14()
    {
        heroinKidAnimator.runToX(1.5f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_15()
    {
        heroinKidAnimator.runToX(8f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        heroinKidAnimator.runToY(51f);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isRunning)
        {
            SoundManager.soundManager.PlayFootStepRunSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);
        heroinKidAnimator.Sit();
        yield return new WaitForSeconds(0.5f);

        PlayerAnimation.playerAnimation.moveToY(51f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
       
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        PlayerAnimation.playerAnimation.Sit();
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_16()
    {
        MusicBox.SetActive(true);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_17()
    {
        PlayerAnimation.playerAnimation.SeeRight();
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.Blink();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_18()
    {
        PlayerAnimation.playerAnimation.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_19()
    {
        PlayerAnimation.playerAnimation.SeeRight();
        yield return new WaitForSeconds(1f);
        heroinKidAnimator.Click();
        SoundManager.soundManager.PlayEffectClip(10);
        yield return new WaitForSeconds(1f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(7, true);
        yield return new WaitForSeconds(5f);
        heroinKidAnimator.Click();
        SoundManager.soundManager.PlayEffectClip(10);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(1f);
        heroinKidAnimator.SeeLeft();
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_20()
    {
        heroinKidAnimator.SeeFront();
        yield return new WaitForSeconds(1f);
        heroinKidAnimator.Click();
        SoundManager.soundManager.PlayEffectClip(10);
        yield return new WaitForSeconds(1f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(8, true);
        yield return new WaitForSeconds(5f);
        heroinKidAnimator.Click();
        SoundManager.soundManager.PlayEffectClip(10);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(1f);
        heroinKidAnimator.SeeLeft();
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_21()
    {
        heroinKidAnimator.SeeLeft();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_22()
    {
        heroinKidAnimator.SeeFront();
        yield return new WaitForSeconds(1f);
        heroinKidAnimator.Click();
        SoundManager.soundManager.PlayEffectClip(10);
        yield return new WaitForSeconds(1f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(9, true);
        yield return new WaitForSeconds(15f);
        PlayerAnimation.playerAnimation.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_23()
    {
        heroinKidAnimator.Click();
        SoundManager.soundManager.PlayEffectClip(10);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(1.5f);
        heroinKidAnimator.SeeLeft();
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.SeeRight();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_24()
    {
        heroinKidAnimator.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }
    private IEnumerator AnimationSet_25()
    {
        heroinKidAnimator.SeeFront();
        yield return new WaitForSeconds(1f);
        heroinKidAnimator.Stand();
        yield return new WaitForSeconds(1.2f);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_26()
    {
        yield return new WaitForSeconds(0.5f);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_27()
    {
        PlayerAnimation.playerAnimation.SeeFront();
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.Stand();
        yield return new WaitForSeconds(1.2f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_28()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_29()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_30()
    {
        yield return new WaitForSeconds(0.5f);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator MoveToNextScene()
    {
        GameManager.canInput = false;

        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();

        FadeInOutBlack.fadeInOutBlack.SetColor(1);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);

        yield return new WaitForSeconds(5f);
        PlayerAnimation.playerAnimation.changeState(0);
        GameManager.gameManager.ShowTextOn("3일째 낮", 1);
        GameManager.gameManager.sceneNumber = 6;
        GameManager.gameManager.MoveScene("ThirdDayScene", true);
    }
























//퍼즐부분

public enum SwitchState {NONE,DAY,NIGHT}

    public enum Where { N, E, W, S,X }

    public Where[,] toWhere = new Where[5, 4];
    public Where[,] toGo = new Where[5, 4];

    public Image[] ButtonImages = new Image[20]; // 외부에서 지정


    public Sprite[] ButtonSprites = new Sprite[5]; // 외부에서 지정

    public Button[] Buttons = new Button[20];


    public GameObject[] Flowers = new GameObject[6]; // 외부에서 지정
    public GameObject[] FlowerPots = new GameObject[6]; // 외부에서 지정
    public GameObject[] OnLights = new GameObject[6]; // 외부에서 지정
    public GameObject[] UnLights = new GameObject[6]; // 외부에서 지정




    public GameObject[] DayObjs = new GameObject[6]; // 외부에서 지정
    public GameObject[] NightObjs = new GameObject[6]; // 외부에서 지정


    private bool[] SolveKey = { true, false, false, true, false, true };//true==낮, false==밤

    private bool[] UserSolve = { true, true, true, true, true, true };

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                toWhere[i, j] = Where.X;
                toGo[i, j] = Where.X;
            }
        }
        addButtonFunction();
    }



    public void OnClickSwitchButton(int idx) {
        SoundManager.soundManager.PlayEffectClip(24);

        UserSolve[idx] = !UserSolve[idx];
        RedrawSwitches();

        for (int i = 0; i < 6; i++)
        {
            if (SolveKey[i] != UserSolve[i]) { return; }
        }

        Debug.Log("성공");
        StartSecondCutScene();
        //+)성공시 함수 추가
    }

    public void RedrawSwitches() {
        for (int i = 0; i < 6; i++)
        {
            if (UserSolve[i])
            {
                DayObjs[i].SetActive(true);
                NightObjs[i].SetActive(false);
            }
            else {
                DayObjs[i].SetActive(false);
                NightObjs[i].SetActive(true);
            }
        }
    }

    public void OnClickChangeButtonState(int row, int culumn) {
        SoundManager.soundManager.PlayEffectClip(23);
        //+)사운드
        switch (toWhere[row,culumn])
        {
            case Where.N:
                toWhere[row, culumn] = Where.E;
                toGo[row, culumn] = Where.S;
                ButtonImages[row+ culumn*5].sprite = ButtonSprites[2];
                break;
            case Where.E:
                toWhere[row, culumn] = Where.S;
                toGo[row, culumn] = Where.W;
                ButtonImages[row + culumn * 5].sprite = ButtonSprites[3];
                break;
            case Where.W:
                toWhere[row, culumn] = Where.X;
                toGo[row, culumn] = Where.X;
                ButtonImages[row + culumn * 5].sprite = ButtonSprites[0];
                break;
            case Where.S:
                toWhere[row, culumn] = Where.W;
                toGo[row, culumn] = Where.N;
                ButtonImages[row + culumn * 5].sprite = ButtonSprites[4];
                break;
            case Where.X:
                toWhere[row, culumn] = Where.N;
                toGo[row, culumn] = Where.E;
                ButtonImages[row + culumn * 5].sprite = ButtonSprites[1];
                break;
            default:
                break;
        }

        CheckFlow();
    }


    public void CheckFlow() {
        int row = 2;
        int culumn = -1;
        Debug.Log("체킹 시작");

        Where LastToWhere = Where.N;
        while (true)
        {
            switch (LastToWhere)
            {
                case Where.N:
                    Debug.Log("N에서 접근");

                    if (culumn == 3) return;

                    if (toWhere[row, culumn + 1] == Where.N)
                    {
                        culumn++;

                        if (CheckLightOn(row, culumn, toGo[row, culumn])) return;
                        LastToWhere = changeToGo(toGo[row, culumn]);
                    }
                    else if (toGo[row, culumn + 1] == Where.N)
                    {
                        culumn++;

                        if (CheckLightOn(row, culumn, toWhere[row, culumn])) return;
                        LastToWhere = changeToGo(toWhere[row, culumn]);
                    }
                    else { return; }
                    break;



                case Where.E:
                    Debug.Log("E에서 접근");

                    if (row == 0) return;

                    if (toWhere[row-1, culumn] == Where.E)
                    {
                        row--;

                        if (CheckLightOn(row, culumn, toGo[row, culumn])) return;
                        LastToWhere = changeToGo(toGo[row, culumn]);
                    }
                    else if (toGo[row-1, culumn] == Where.E)
                    {
                        row--;

                        if (CheckLightOn(row, culumn, toWhere[row, culumn])) return;
                        LastToWhere = changeToGo(toWhere[row, culumn]);
                    }
                    else { return; }
                    break;



                case Where.W:
                    Debug.Log("W에서 접근");

                    if (row == 4) return;

                    if (toWhere[row+1, culumn] == Where.W)
                    {
                        row++;

                        if (CheckLightOn(row, culumn, toGo[row, culumn])) return;
                        LastToWhere = changeToGo(toGo[row, culumn]);
                    }
                    else if (toGo[row+1, culumn] == Where.W)
                    {
                        row++;

                        if (CheckLightOn(row, culumn, toWhere[row, culumn])) return;
                        LastToWhere = changeToGo(toWhere[row, culumn]);
                    }
                    else { return; }
                    break;



                case Where.S:
                    Debug.Log("S에서 접근");

                    if (culumn == 0) return;

                    if (toWhere[row, culumn - 1] == Where.S)
                    {
                        culumn--;

                        if (CheckLightOn(row, culumn, toGo[row, culumn])) return;
                        LastToWhere = changeToGo(toGo[row, culumn]);
                    }
                    else if (toGo[row, culumn - 1] == Where.S)
                    {
                        culumn--;

                        if (CheckLightOn(row, culumn, toWhere[row, culumn])) return;
                        LastToWhere = changeToGo(toWhere[row, culumn]);
                    }
                    else { return; }
                    break;



                default:
                    return;
                    break;
            }
        }
    }

    public bool CheckLightOn(int row, int culumn, Where where)
    {
        if (row == 1 && culumn == 1 && where == Where.E)
        {
            for (int i = 0; i < FlowerPots.Length; i++)
            {
                FlowerPots[i].SetActive(false);
                Flowers[i].SetActive(true);
            }
            for (int i = 0; i < OnLights.Length; i++)
            {
                OnLights[i].SetActive(false);
                UnLights[i].SetActive(true);
            }

            OnLights[0].SetActive(true);
            UnLights[0].SetActive(false);
            Debug.Log("1오픈");
            return true;
            //1
        }
        else if (row == 2 && culumn == 1 && where == Where.W)
        {
            for (int i = 0; i < FlowerPots.Length; i++)
            {
                FlowerPots[i].SetActive(false);
                Flowers[i].SetActive(true);
            }
            for (int i = 0; i < OnLights.Length; i++)
            {
                OnLights[i].SetActive(false);
                UnLights[i].SetActive(true);
            }

            OnLights[0].SetActive(true);
            UnLights[0].SetActive(false);
            SoundManager.soundManager.PlayEffectClip(25);
            Debug.Log("1오픈");
            return true;
            //1
        }
        else if (row == 0 && culumn == 2 && where == Where.W)
        {
            for (int i = 0; i < FlowerPots.Length; i++)
            {
                FlowerPots[i].SetActive(false);
                Flowers[i].SetActive(true);
            }
            for (int i = 0; i < OnLights.Length; i++)
            {
                OnLights[i].SetActive(false);
                UnLights[i].SetActive(true);
            }

            OnLights[1].SetActive(true);
            UnLights[1].SetActive(false);

            FlowerPots[1].SetActive(true);
            Flowers[1].SetActive(false);
            SoundManager.soundManager.PlayEffectClip(25);
            Debug.Log("2오픈");
            return true;
            //2
        }
        else if (row == 0 && culumn == 3 && where == Where.S)
        {
            for (int i = 0; i < FlowerPots.Length; i++)
            {
                FlowerPots[i].SetActive(false);
                Flowers[i].SetActive(true);
            }
            for (int i = 0; i < OnLights.Length; i++)
            {
                OnLights[i].SetActive(false);
                UnLights[i].SetActive(true);
            }

            OnLights[2].SetActive(true);
            UnLights[2].SetActive(false);

            FlowerPots[2].SetActive(true);
            Flowers[2].SetActive(false);
            SoundManager.soundManager.PlayEffectClip(25);
            Debug.Log("3오픈");
            return true;
            //3
        }
        else if (row == 2 && culumn == 3 && where == Where.S)
        {
            for (int i = 0; i < FlowerPots.Length; i++)
            {
                FlowerPots[i].SetActive(false);
                Flowers[i].SetActive(true);
            }
            for (int i = 0; i < OnLights.Length; i++)
            {
                OnLights[i].SetActive(false);
                UnLights[i].SetActive(true);
            }

            OnLights[3].SetActive(true);
            UnLights[3].SetActive(false);
            SoundManager.soundManager.PlayEffectClip(25);
            Debug.Log("4오픈");
            return true;
            //4
        }
        else if (row == 3 && culumn == 3 && where == Where.E)
        {
            for (int i = 0; i < FlowerPots.Length; i++)
            {
                FlowerPots[i].SetActive(false);
                Flowers[i].SetActive(true);
            }
            for (int i = 0; i < OnLights.Length; i++)
            {
                OnLights[i].SetActive(false);
                UnLights[i].SetActive(true);
            }

            OnLights[4].SetActive(true);
            UnLights[4].SetActive(false);

            FlowerPots[4].SetActive(true);
            Flowers[4].SetActive(false);
            SoundManager.soundManager.PlayEffectClip(25);
            Debug.Log("5오픈");
            return true;
            //5
        }
        else if (row == 4 && culumn == 3 && where == Where.W)
        {
            for (int i = 0; i < FlowerPots.Length; i++)
            {
                FlowerPots[i].SetActive(false);
                Flowers[i].SetActive(true);
            }
            for (int i = 0; i < OnLights.Length; i++)
            {
                OnLights[i].SetActive(false);
                UnLights[i].SetActive(true);
            }

            OnLights[4].SetActive(true);
            UnLights[4].SetActive(false);

            FlowerPots[4].SetActive(true);
            Flowers[4].SetActive(false);
            SoundManager.soundManager.PlayEffectClip(25);
            Debug.Log("5오픈");
            return true;
            //5
        }
        else if (row == 4 && culumn == 0 && where == Where.E)
        {
            for (int i = 0; i < FlowerPots.Length; i++)
            {
                FlowerPots[i].SetActive(false);
                Flowers[i].SetActive(true);
            }
            for (int i = 0; i < OnLights.Length; i++)
            {
                OnLights[i].SetActive(false);
                UnLights[i].SetActive(true);
            }

            OnLights[5].SetActive(true);
            UnLights[5].SetActive(false);
            SoundManager.soundManager.PlayEffectClip(25);
            Debug.Log("6오픈");
            return true;
            //6
        }
        else {

            return false;
        }
    }

    private Where changeToGo(Where toWhere) {
        switch (toWhere)
        {
            case Where.N:
                return Where.S;
            case Where.E:
                return Where.W;
            case Where.W:
                return Where.E;
            case Where.S:
                return Where.N;
            case Where.X:
                return Where.X;
            default:
                return Where.X;
        }

    }

    private void addButtonFunction() {
        Buttons[0].onClick.AddListener(delegate { OnClickChangeButtonState(0, 0); });
        Buttons[1].onClick.AddListener(delegate { OnClickChangeButtonState(1, 0); });
        Buttons[2].onClick.AddListener(delegate { OnClickChangeButtonState(2, 0); });
        Buttons[3].onClick.AddListener(delegate { OnClickChangeButtonState(3, 0); });
        Buttons[4].onClick.AddListener(delegate { OnClickChangeButtonState(4, 0); });
        Buttons[5].onClick.AddListener(delegate { OnClickChangeButtonState(0, 1); });
        Buttons[6].onClick.AddListener(delegate { OnClickChangeButtonState(1, 1); });
        Buttons[7].onClick.AddListener(delegate { OnClickChangeButtonState(2, 1); });
        Buttons[8].onClick.AddListener(delegate { OnClickChangeButtonState(3, 1); });
        Buttons[9].onClick.AddListener(delegate { OnClickChangeButtonState(4, 1); });
        Buttons[10].onClick.AddListener(delegate { OnClickChangeButtonState(0, 2); });
        Buttons[11].onClick.AddListener(delegate { OnClickChangeButtonState(1, 2); });
        Buttons[12].onClick.AddListener(delegate { OnClickChangeButtonState(2, 2); });
        Buttons[13].onClick.AddListener(delegate { OnClickChangeButtonState(3, 2); });
        Buttons[14].onClick.AddListener(delegate { OnClickChangeButtonState(4, 2); });
        Buttons[15].onClick.AddListener(delegate { OnClickChangeButtonState(0, 3); });
        Buttons[16].onClick.AddListener(delegate { OnClickChangeButtonState(1, 3); });
        Buttons[17].onClick.AddListener(delegate { OnClickChangeButtonState(2, 3); });
        Buttons[18].onClick.AddListener(delegate { OnClickChangeButtonState(3, 3); });
        Buttons[19].onClick.AddListener(delegate { OnClickChangeButtonState(4, 3); });
    }

    public void SuccessPuzzle() {

    }

}
