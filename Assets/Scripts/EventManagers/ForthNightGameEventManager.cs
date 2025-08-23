using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ForthNightGameEventManager : GameEventManager
{

    public int chatCount;


    public bool isInCutScene;

    public HeroinAnimator heroinAnimator;


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
        GameManager.gameManager.lastSpace = GameManager.Space.HospitalSecond;
        GameManager.gameManager.changeFootStepSound();
        GameManager.canInput = false;
        EventTriggers[0].SetActive(true);
        PlayerData.playerData.GetComponent<SpriteRenderer>().color = new Color(95f / 255f, 95f / 255f, 95f / 255f, 255f / 255f);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(10, true);

        PlayerData.playerData.transform.position = new Vector2(300, 26.5f);

        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        heroinAnimator.See(HeroinAnimator.SeeWhere.BACK);

        StartCoroutine(FirstCutSceneCoroutine());
    }

    private IEnumerator FirstCutSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.ShowTextOff();
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(3f);
        ChatManager.chatManager.OpenChat(61, EndFirstCutScene);
    }

    public void EndFirstCutScene()
    {
        KnowedManager.knowedManager.changeNowState(16);
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
                SoundManager.soundManager.PlayDoorSound(0);
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
            default:
                break;
        }


    }


    private IEnumerator AnimationSet_0()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_1()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.BACK);
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
        heroinAnimator.See(HeroinAnimator.SeeWhere.BACK);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.BACK);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_4()
    {
        yield return new WaitForSeconds(0.5f);
        heroinAnimator.See(HeroinAnimator.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(1f);
        CallEndSignalToChat();
    }

    private IEnumerator AnimationSet_5()
    {
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);

        yield return new WaitForSeconds(2f);

        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(2, true);
        PlayerData.playerData.transform.position = new Vector2(4, 10.5f);

        yield return new WaitForSeconds(2f);
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);

        yield return new WaitForSeconds(2.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.RIGHT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.LEFT);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimation.playerAnimation.seeAnimationWhere(PlayerAnimation.SeeWhere.FRONT);
        yield return new WaitForSeconds(0.5f);
        CallEndSignalToChat();
    }


    private IEnumerator MoveToNextScene()
    {
        GameManager.canInput = false;

        BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();

        FadeInOutBlack.fadeInOutBlack.SetColor(3);
        FadeInOutBlack.fadeInOutBlack.SetFadeIn(4f);

        yield return new WaitForSeconds(5f);
        GameManager.gameManager.ShowTextOn("4일째 어딘가", 3);
        GameManager.gameManager.sceneNumber = 11;
        GameManager.gameManager.MoveScene("ForthPuzzleScene", true);
    }


}

