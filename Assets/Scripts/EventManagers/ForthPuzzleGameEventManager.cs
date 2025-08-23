using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ForthPuzzleGameEventManager : GameEventManager
{
    public HeroinAnimator heroinAnimator;
    public MotherAnimator motherAnimator;



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

        for (int i = 0; i < 4; i++)
        {
            sw.WriteLine(isOpen[i].ToString());
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

        for (int i = 0; i < 4; i++)
        {
            isOpen[i] = bool.Parse(sr.ReadLine());

            if (isOpen[i]) { isSuccessImage[i].sprite = successSprite; }
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
        ChatManager.chatManager.OpenChat(62, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(3, true);
        KnowedManager.knowedManager.changeNowState(28);
    }


    private void StartSecondCutScene()
    {

        GameManager.canInput = false;
        StartCoroutine(SecondCutSceneCoroutine());
    }

    private IEnumerator SecondCutSceneCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        ChatManager.chatManager.OpenChat(63, EndSecondCutScene);
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

            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        doctorAnimator.See(DoctorAnimator.SeeWhere.RIGHT);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(2.5f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(19, true);
        CameraShake.cameraShake.Shake(4f, 0.15f);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);
        yield return new WaitForSeconds(4f);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
        yield return new WaitForSeconds(2.5f);
        PlayerAnimation.playerAnimation.transform.position = new Vector2(-203.5f,17.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        motherAnimator.See(MotherAnimator.SeeWhere.LEFT);
        doctorAnimator.See(DoctorAnimator.SeeWhere.RIGHT);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(2, true);
        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_1()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_2()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
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
        motherAnimator.See(MotherAnimator.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_6()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_7()
    {

        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_8()
    {

        yield return new WaitForSeconds(0.5f);
        motherAnimator.See(MotherAnimator.SeeWhere.LEFT);
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
        GameManager.gameManager.ShowTextOn("5일째 낮", 1);
        GameManager.gameManager.sceneNumber = 12;
        GameManager.gameManager.MoveScene("FifthDayScene", true);
        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
    }
























    //퍼즐부분
    private bool isStart = false;
    private bool[] isOpen = { false,false,false,false};

    public GameObject[] hintUI = new GameObject[4];
    public GameObject[] puzzleUI = new GameObject[4];

    public Image[] isSuccessImage = new Image[4];
    public Sprite successSprite;
    public GameObject successSet;

    public InputField[] inputText = new InputField[2];
    private int[] successNum = { 97133, 8 };

    public void EndUI(int i) {
        SoundManager.soundManager.PlayEffectClip(29);
        puzzleUI[i].SetActive(false);
        ChatManager.chatManager.OpenChat(227, null);
    }

    public void OnClickSuccessButton(int i) {
        SoundManager.soundManager.PlayEffectClip(28);
        puzzleUI[i].SetActive(false);
        isSuccessImage[i].sprite = successSprite;
        isOpen[i] = true;
        if (isOpen[0] && isOpen[1] && isOpen[2] && isOpen[3])
        {
            Debug.Log(isOpen[0] + " " + isOpen[1] + " " + isOpen[2] + " " + isOpen[3] + " ");
            successSet.SetActive(false);
            ChatManager.chatManager.OpenChat(228, EndPuzzle);
        }
        else
        {
            Debug.Log(isOpen[0] + " " + isOpen[1] + " " + isOpen[2] + " " + isOpen[3] + " ");
            ChatManager.chatManager.OpenChat(228, null);
        }
    }

    public void OpenPuzzleUI(int i) {
        if (!isOpen[i]) {
            puzzleUI[i].SetActive(true);
        }
    }

    public void OnClickTextCheckButton(int i) {
        SoundManager.soundManager.PlayEffectClip(18);
        string str = inputText[i-1].text;

        if (int.TryParse(str, out int parseNum))
        {
            if (parseNum == successNum[i - 1])
            {
                SoundManager.soundManager.PlayEffectClip(28);
                puzzleUI[i].SetActive(false);
                isSuccessImage[i].sprite = successSprite;
                isOpen[i] = true;

                if (isOpen[0] && isOpen[1] && isOpen[2] && isOpen[3])
                {
                    Debug.Log(isOpen[0] + " " + isOpen[1] + " " + isOpen[2] + " " + isOpen[3] + " ");
                    successSet.SetActive(false);
                    ChatManager.chatManager.OpenChat(228, EndPuzzle);
                }
                else
                {
                    Debug.Log(isOpen[0] + " " + isOpen[1] + " " + isOpen[2] + " " + isOpen[3] + " ");
                    ChatManager.chatManager.OpenChat(228, null);
                }
            }
            else
            {
                EndUI(i);
            }
        }
        else {
            EndUI(i);
        }
    }

    public void StartPuzzle() {
        successSet.SetActive(true);
        isStart = true;
    }

    public void EndPuzzle() {
        successSet.SetActive(false);
        StartSecondCutScene();
        //완료
    }


    private void Update()
    {
        if (isFirst)
        {
            isFirst = false;
            StartFirstCutScene();
        }


        if (!isStart) {
            StartPuzzle();
        }

    }

}