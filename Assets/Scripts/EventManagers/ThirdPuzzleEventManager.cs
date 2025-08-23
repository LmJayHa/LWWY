using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class ThirdPuzzleEventManager : GameEventManager
{
    public HeroinKidAnimator heroinKidAnimator;



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
        ChatManager.chatManager.OpenChat(51, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(3, true);
        KnowedManager.knowedManager.changeNowState(28);
        EventTriggers[0].SetActive(true);
        EventTriggers[1].SetActive(true);
    }



    private void StartSecondCutScene()
    {

        GameManager.canInput = false;
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        ChatManager.chatManager.OpenChat(52, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        KnowedManager.knowedManager.changeNowState(29);
       
    }


    private void StartThirdCutScene()
    {

        GameManager.canInput = false;
        StartCoroutine(ThirdCutSceneCoroutine());
    }

    private IEnumerator ThirdCutSceneCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        ChatManager.chatManager.OpenChat(53, EndThirdCutScene);
    }

    public void EndThirdCutScene()
    {
        KnowedManager.knowedManager.changeNowState(29);
        PlayerData.playerData.isOpenKnoweds[3] = true;
        PlayerData.playerData.isOpenKnoweds[4] = true;
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
                StartThirdCutScene();
                break;
            case 1:
                ChatManager.chatManager.OpenChat(220, null);
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

            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
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
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(2.5f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(19, true);
        CameraShake.cameraShake.Shake(4f, 0.15f);
        FadeInOutBlack.fadeInOutBlack.SetColor(2);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);
        yield return new WaitForSeconds(4f);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(2.5f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(10, true);
        PlayerAnimation.playerAnimation.changeState(1);//꼬마로 변경
        PlayerData.playerData.GetComponent<PlayerStar>().starObj.SetActive(false);
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(95f / 255f, 95f / 255f, 95f / 255f, 255f / 255f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(325, 313);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);

        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
       //페이드 인 시작
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(300, 24);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        heroinKidAnimator.moveToX(301);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        heroinKidAnimator.moveToY(27);
        yield return new WaitForSeconds(0.001f);
        while (heroinKidAnimator.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.3f);
        heroinKidAnimator.Sit();

        PlayerAnimation.playerAnimation.moveToX(300f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.001f);
        PlayerAnimation.playerAnimation.moveToY(27f);
        yield return new WaitForSeconds(0.001f);
        while (PlayerAnimation.playerAnimation.isMoving)
        {
            SoundManager.soundManager.PlayFootStepSounds();
            yield return new WaitForSeconds(0.01f);
        }
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.3f);
        PlayerAnimation.playerAnimation.Sit();
        yield return new WaitForSeconds(1.5f);
        PlayerAnimation.playerAnimation.SeeRight();
        yield return new WaitForSeconds(0.4f);
        heroinKidAnimator.SeeLeft();
        yield return new WaitForSeconds(1.5f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_7()
    {
        yield return new WaitForSeconds(0.4f);
        heroinKidAnimator.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_8()
    {
        yield return new WaitForSeconds(0.4f);
        PlayerAnimation.playerAnimation.SeeFront();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_9()
    {
        yield return new WaitForSeconds(0.4f);
        heroinKidAnimator.SeeLeft();
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.SeeRight();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }


    private IEnumerator AnimationSet_10()
    {
        yield return new WaitForSeconds(0.4f);
        PlayerAnimation.playerAnimation.SeeRight();
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_11()
    {
        yield return new WaitForSeconds(0.4f);
        PlayerAnimation.playerAnimation.Stand();
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_12()
    {
        
        heroinKidAnimator.SeeFront();
        yield return new WaitForSeconds(0.5f);
        heroinKidAnimator.Stand();
        yield return new WaitForSeconds(0.5f);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        heroinKidAnimator.See(HeroinKidAnimator.SeeWhere.LEFT);
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
        GameManager.gameManager.ShowTextOn("4일째 낮", 1);
        GameManager.gameManager.sceneNumber = 9;
        GameManager.gameManager.MoveScene("ForthDayScene", true);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
    }
























    //퍼즐부분
    public bool isInGame = true;


    public enum StarState { RED, RED_LIGHT, BLUE, YELLOW, GREEN, PUPPLE, PINK, NONE }

    public StarState[] starState = { StarState.RED, StarState.RED, StarState.RED_LIGHT, StarState.RED_LIGHT, StarState.RED_LIGHT, StarState.BLUE, StarState.YELLOW, StarState.GREEN, StarState.PUPPLE, StarState.PINK };

    public StarState[] caseState = { StarState.NONE, StarState.NONE, StarState.NONE };
    public int[] caseIndex = { -1, -1, -1 };

    public StarState nowHave = StarState.NONE;
    public int nowHaveIndex = -1;

    public enum TimeState { DAY, NIGHT};
    public TimeState timeState = TimeState.DAY;

    public SpriteRenderer[] starImgObjs = new SpriteRenderer[10];
    public GameObject[] starObjs = new GameObject[10];
    public bool[] isStarGone = { false, false, false, false, false, false, false, false, false, false };
    

    public SpriteRenderer[] CaseStarImage = new SpriteRenderer[3];
    public GameObject[] CaseStarImageObjs = new GameObject[3];

    public Sprite[] StarSprites = new Sprite[9];


    public GameObject DayFog;
    public GameObject NightFog;

    public void CaseInOut(int idx)
    {
        if (!isInGame) { return; }

        SoundManager.soundManager.PlayEffectClip(18);
        if (nowHave == StarState.NONE)
        {
            if (caseState[idx] == StarState.NONE)
            {
                return;
            }
            else
            {
                nowHave = caseState[idx];
                caseState[idx] = StarState.NONE;

                nowHaveIndex = caseIndex[idx];
                caseIndex[idx] = -1;

            }
        }
        else
        {
            if (caseState[idx] == StarState.NONE)
            {
                caseState[idx] = nowHave;
                nowHave = StarState.NONE;

                caseIndex[idx]=nowHaveIndex;
                nowHaveIndex = -1;
            }
            else
            {
                StarState tmp = caseState[idx];
                caseState[idx] = nowHave;
                nowHave = tmp;

                int tmp_ = caseIndex[idx];
                caseIndex[idx] = nowHaveIndex;
                nowHaveIndex = tmp_;

            }
        }
        check();
        changeAllImage();
    }

    public void changeAllImage() {

        for (int i = 0; i < 10; i++)
        {
            if (isStarGone[i])
            {
                starObjs[i].SetActive(false);
            }
            else {
                starObjs[i].SetActive(true);
                if (timeState == TimeState.DAY)
                {
                    switch (i)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            starImgObjs[i].sprite = StarSprites[0];
                            break;
                        case 5:
                            starImgObjs[i].sprite = StarSprites[1];
                            break;
                        case 6:
                            starImgObjs[i].sprite = StarSprites[2];
                            break;
                        case 7:
                            starImgObjs[i].sprite = StarSprites[3];
                            break;
                        case 8:
                            starImgObjs[i].sprite = StarSprites[4];
                            break;
                        case 9:
                            starImgObjs[i].sprite = StarSprites[5];
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 0:
                        case 1:
                        case 5:
                        case 6:
                            starImgObjs[i].sprite = StarSprites[6];
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 7:
                        case 8:
                        case 9:
                            starImgObjs[i].sprite = StarSprites[7];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (caseState[i] == StarState.NONE)
            {
                CaseStarImageObjs[i].SetActive(false);
            }
            else {
                CaseStarImageObjs[i].SetActive(true);
                if (timeState == TimeState.DAY)
                {
                    switch (caseState[i])
                    {
                        case StarState.RED:
                            CaseStarImage[i].sprite = StarSprites[0];
                            break;
                        case StarState.RED_LIGHT:
                            CaseStarImage[i].sprite = StarSprites[0];
                            break;
                        case StarState.BLUE:
                            CaseStarImage[i].sprite = StarSprites[1];
                            break;
                        case StarState.YELLOW:
                            CaseStarImage[i].sprite = StarSprites[2];
                            break;
                        case StarState.GREEN:
                            CaseStarImage[i].sprite = StarSprites[3];
                            break;
                        case StarState.PUPPLE:
                            CaseStarImage[i].sprite = StarSprites[4];
                            break;
                        case StarState.PINK:
                            CaseStarImage[i].sprite = StarSprites[5];
                            break;
                        default:
                            break;
                    }
                }
                else {
                    switch (caseState[i])
                    {
                        case StarState.RED:
                            CaseStarImage[i].sprite = StarSprites[6];
                            break;
                        case StarState.RED_LIGHT:
                            CaseStarImage[i].sprite = StarSprites[7];
                            break;
                        case StarState.BLUE:
                            CaseStarImage[i].sprite = StarSprites[6];
                            break;
                        case StarState.YELLOW:
                            CaseStarImage[i].sprite = StarSprites[6];
                            break;
                        case StarState.GREEN:
                            CaseStarImage[i].sprite = StarSprites[7];
                            break;
                        case StarState.PUPPLE:
                            CaseStarImage[i].sprite = StarSprites[7];
                            break;
                        case StarState.PINK:
                            CaseStarImage[i].sprite = StarSprites[7];
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        if (nowHave == StarState.NONE) {
            PlayerData.playerData.GetComponent<PlayerStar>().starObj.SetActive(false);
        }
        else
        {
            PlayerData.playerData.GetComponent<PlayerStar>().starObj.SetActive(true);
            if (timeState == TimeState.DAY)
            {
                switch (nowHave)
                {
                    case StarState.RED:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[0];
                        break;
                    case StarState.RED_LIGHT:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[0];
                        break;
                    case StarState.BLUE:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[1];
                        break;
                    case StarState.YELLOW:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[2];
                        break;
                    case StarState.GREEN:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[3];
                        break;
                    case StarState.PUPPLE:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[4];
                        break;
                    case StarState.PINK:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[5];
                        break;
                    default:
                        break;
                }
            }
            else {
                switch (nowHave)
                {
                    case StarState.RED:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[6];
                        break;
                    case StarState.RED_LIGHT:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[7];
                        break;
                    case StarState.BLUE:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[6];
                        break;
                    case StarState.YELLOW:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[6];
                        break;
                    case StarState.GREEN:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[7];
                        break;
                    case StarState.PUPPLE:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[7];
                        break;
                    case StarState.PINK:
                        PlayerData.playerData.GetComponent<PlayerStar>().spriteRenderer.sprite = StarSprites[7];
                        break;
                    default:
                        break;
                }
            }
        }



    }

    public void pickUp(int idx) {
        if (!isInGame) { return; }

        SoundManager.soundManager.PlayEffectClip(18);
        if (nowHave != StarState.NONE) { return; }

        isStarGone[idx] = true;
        nowHave = starState[idx];
        nowHaveIndex = idx;
        changeAllImage();
    }

    public void letStarGo() {
        if (nowHave == StarState.NONE) { return; }

        SoundManager.soundManager.PlayEffectClip(18);
        isStarGone[nowHaveIndex] = false;
        nowHaveIndex = -1;
        nowHave = StarState.NONE;
        changeAllImage();
    }

    public void check() {
        if (!isInGame) { return; }

        if (caseState[0] == StarState.RED_LIGHT && caseState[1] == StarState.RED_LIGHT && caseState[2] == StarState.RED_LIGHT)
        {
            EndPuzzle();//퍼즐 풂
        }
        else if ((caseState[0] == StarState.RED || caseState[0] == StarState.RED_LIGHT) && caseState[1] == StarState.BLUE && caseState[2] == StarState.YELLOW)
        {
            SoundManager.soundManager.PlayEffectClip(28);
            timeState = TimeState.NIGHT;
            NightFog.SetActive(true);
            DayFog.SetActive(false);
        }
        else {
            timeState = TimeState.DAY;
            NightFog.SetActive(false);
            DayFog.SetActive(true);
        }
    }

    public void EndPuzzle() {
        PlayerData.playerData.GetComponent<PlayerStar>().starObj.SetActive(false);
        StartSecondCutScene();
    }







    private void Update()
    {
        if (isFirst)
        {
            isFirst = false;
            StartFirstCutScene();
        }




        //퍼즐부분
        if (isInGame) {
            if (Input.GetKeyDown(KeyCode.F)) {
                letStarGo();
            }
        }

    }

}
