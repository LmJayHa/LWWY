using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FifthPuzzleGameEventManager : GameEventManager
{



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



        sr.Close();
    }









    private void StartFirstCutScene()
    {

        GameManager.canInput = false;
        SoundManager.soundManager.PlayDoorSound(1);
        GameManager.gameManager.lastSpace = GameManager.Space.Puzzle;
        GameManager.gameManager.changeFootStepSound();
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        StartCoroutine(FirstCutSceneCoroutine());
    }

    private IEnumerator FirstCutSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.ShowTextOff();
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        ChatManager.chatManager.OpenChat(73, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(3, true);
        KnowedManager.knowedManager.changeNowState(28);
    }


    private void StartSecondCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        GameManager.canInput = false;
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        ChatManager.chatManager.OpenChat(74, EndSecondCutScene);
    }

    public void EndSecondCutScene()
    {
        PlayerData.playerData.isOpenKnoweds[6] = true;
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

            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_1()
    {
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(2.5f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(19, true);
        CameraShake.cameraShake.Shake(4f, 0.15f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);
        yield return new WaitForSeconds(4f);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(1f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(-206.75f, 15);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(2f);
        doctorAnimator.See(DoctorAnimator.SeeWhere.RIGHT);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_3()
    {
        yield return new WaitForSeconds(0.5f);
        doctorAnimator.See(DoctorAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        doctorAnimator.See(DoctorAnimator.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
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
        GameManager.gameManager.ShowTextOn("6일째 낮", 1);
        GameManager.gameManager.sceneNumber = 15;
        GameManager.gameManager.MoveScene("SixthDayScene", true);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
    }




























    //퍼즐 부분
    public int[] correctAnswer = { 10, 48, 25 };
    public GameObject[] failObjs = new GameObject[3];
    public GameObject[] successObjs = new GameObject[3];

    public int answerSum;
    public Text answerText;

    public SpriteRenderer[] answerObjsImage = new SpriteRenderer[3];
    public GameObject[] answerObjs = new GameObject[3];

    public GameObject[] pickableObjs = new GameObject[10];

    public int playerpickNum = -1;

    public Sprite[] spriteSet = new Sprite[5];

    public int[] answerIdx = { -1, -1, -1 };

    public bool[] isEnd = { false, false, false };


    public void Pick(int idx) {
        SoundManager.soundManager.PlayEffectClip(18);
        if (playerpickNum == -1)
        {
            playerpickNum = idx;
            PlayerPickObj.playerPickObj._object.SetActive(true);
            pickableObjs[idx].SetActive(false);
            switch (idx)
            {
                case 0:
                case 1:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[0];
                    break;
                case 2:
                case 3:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[1];
                    break;
                case 4:
                case 5:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[2];
                    break;
                case 6:
                case 7:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[3];
                    break;
                case 8:
                case 9:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[4];
                    break;
                default:
                    break;
            }
        }
        else {
            pickableObjs[playerpickNum].SetActive(true);
            playerpickNum = idx;
            pickableObjs[idx].SetActive(false);
            switch (idx)
            {
                case 0:
                case 1:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[0];
                    break;
                case 2:
                case 3:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[1];
                    break;
                case 4:
                case 5:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[2];
                    break;
                case 6:
                case 7:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[3];
                    break;
                case 8:
                case 9:
                    PlayerPickObj.playerPickObj._image.sprite = spriteSet[4];
                    break;
                default:
                    break;
            }
        }
    }

    public void Release() {
        if (playerpickNum == -1) return;

        SoundManager.soundManager.PlayEffectClip(18);
        PlayerPickObj.playerPickObj._object.SetActive(false);
        pickableObjs[playerpickNum].SetActive(true);
        playerpickNum = -1;
    }

    public void PutToAnswer(int idx) {
        if (playerpickNum == -1)
        {
            if (answerIdx[idx] == -1) { return; }
            else
            {
                SoundManager.soundManager.PlayEffectClip(18);
                playerpickNum = answerIdx[idx];
                answerIdx[idx] = -1;

                answerObjs[idx].SetActive(false);

                PlayerPickObj.playerPickObj._object.SetActive(true);
                switch (playerpickNum)
                {
                    case 0:
                    case 1:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[0];
                        break;
                    case 2:
                    case 3:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[1];
                        break;
                    case 4:
                    case 5:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[2];
                        break;
                    case 6:
                    case 7:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[3];
                        break;
                    case 8:
                    case 9:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[4];
                        break;
                    default:
                        break;
                }
            }
        }
        else {
            if (answerIdx[idx] == -1)
            {
                SoundManager.soundManager.PlayEffectClip(18);
                answerIdx[idx] = playerpickNum;
                playerpickNum = -1;

                PlayerPickObj.playerPickObj._object.SetActive(false);

                answerObjs[idx].SetActive(true);
                switch (answerIdx[idx])
                {
                    case 0:
                    case 1:
                        answerObjsImage[idx].sprite = spriteSet[0];
                        break;
                    case 2:
                    case 3:
                        answerObjsImage[idx].sprite = spriteSet[1];
                        break;
                    case 4:
                    case 5:
                        answerObjsImage[idx].sprite = spriteSet[2];
                        break;
                    case 6:
                    case 7:
                        answerObjsImage[idx].sprite = spriteSet[3];
                        break;
                    case 8:
                    case 9:
                        answerObjsImage[idx].sprite = spriteSet[4];
                        break;
                    default:
                        break;
                }
            }
            else {
                SoundManager.soundManager.PlayEffectClip(18);
                int tmp = playerpickNum;
                playerpickNum = answerIdx[idx];
                answerIdx[idx] = tmp;

                PlayerPickObj.playerPickObj._object.SetActive(true);
                switch (playerpickNum)
                {
                    case 0:
                    case 1:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[0];
                        break;
                    case 2:
                    case 3:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[1];
                        break;
                    case 4:
                    case 5:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[2];
                        break;
                    case 6:
                    case 7:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[3];
                        break;
                    case 8:
                    case 9:
                        PlayerPickObj.playerPickObj._image.sprite = spriteSet[4];
                        break;
                    default:
                        break;
                }

                answerObjs[idx].SetActive(true);
                switch (answerIdx[idx])
                {
                    case 0:
                    case 1:
                        answerObjsImage[idx].sprite = spriteSet[0];
                        break;
                    case 2:
                    case 3:
                        answerObjsImage[idx].sprite = spriteSet[1];
                        break;
                    case 4:
                    case 5:
                        answerObjsImage[idx].sprite = spriteSet[2];
                        break;
                    case 6:
                    case 7:
                        answerObjsImage[idx].sprite = spriteSet[3];
                        break;
                    case 8:
                    case 9:
                        answerObjsImage[idx].sprite = spriteSet[4];
                        break;
                    default:
                        break;
                }

            }


        }

        CheckState();

    }


    public void CheckState() {
        answerSum = 0;
        for (int i = 0; i < 3; i++)
        {
            if (answerIdx[i] == -1)
            {
                answerText.text = "";
                return;
            }
            else {
                switch (answerIdx[i])
                {
                    case 0:
                    case 1:
                        answerSum += 3;
                        break;
                    case 2:
                    case 3:
                        answerSum += 7;
                        break;
                    case 4:
                    case 5:
                        answerSum += 11;
                        break;
                    case 6:
                    case 7:
                        answerSum += 34;
                        break;
                    case 8:
                    case 9:
                        answerSum += 0;
                        break;
                    default:
                        break;
                }
            }
        }

        answerText.text = answerSum.ToString();
        for (int i = 0; i < 3; i++)
        {
            if (answerSum == correctAnswer[i]) {
                failObjs[i].SetActive(false);
                successObjs[i].SetActive(true);
                isEnd[i] = true;
                if (isEnd[0] && isEnd[1] && isEnd[2]) {
                    EndGame();
                    return;
                }
                break;
            }
        }

    }

    public void EndGame() {
        PlayerPickObj.playerPickObj._object.SetActive(false);
        StartSecondCutScene();
    }


    private void Update()
    {
        if (isFirst)
        {
            isFirst = false;
            StartFirstCutScene();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Release();
        }

    }

}