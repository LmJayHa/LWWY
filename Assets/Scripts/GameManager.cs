using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager
    {
        get
        {
            if (gameM_instance == null)
            {
                gameM_instance = FindObjectOfType<GameManager>();
            }

            return gameM_instance;
        }
    }//GameManager를 싱글턴으로 설정

    private static GameManager gameM_instance; //싱글턴에 이용된 인스턴스

    private bool isFadeIn;

    private bool isFadeOut;

    private float startTime;
    private float addingTime;

    float r;
    float g;
    float b;




    public PlayerData playerData; //플레이어 데이터 ; 외부에서 적용
    public GameEventManager thisSceneEventManager; //이번 씬의 이벤트 매니저

    public ScreenShot screenShot;

    public enum Space
    {
        HospitalFirst,
        HospitalSecond,
        Market,
        Library,
        FlowerShop,
        SpecialRoom,
        Outside,
        Puzzle
    }//장소에 대한 열거형 클래스

    public Space lastSpace; //이동시 자신이 머물었던 장소 

    public int sceneNumber;//현재 있는 씬의 넘버

    public static bool canInput; //입력이 가능한 지

    public bool isFirst = false;

    public Text ShowText; //?일쨰 ? 텍스트
    public GameObject SaveUI; //저장완료시 텍스트

    private bool isFindGameEventManager;


    private void Awake()
    {
        lastSpace = Space.HospitalSecond;
        DontDestroyOnLoad(this);
        ShowTextOff();
        SaveUI.SetActive(false);
        SceneManager.sceneLoaded += GetEventManager;//씬로드델리게이트에 이벤트 메니저 불러오기 추가
    }



    public void SaveData(int where)
    {
        canInput = false;

        thisSceneEventManager.SaveData(where);
        playerData.saveData(where);
        Debug.Log("세이브 시작 : 게임 메니저 ");
        string filePath = Application.dataPath + "/savingData/GameData" + where.ToString() + ".dat";

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

        Debug.Log("스트림 작성중");
        //작성되는 문구들

        sw.WriteLine(playerData.GetComponent<SpriteRenderer>().color.r);
        sw.WriteLine(playerData.GetComponent<SpriteRenderer>().color.g);
        sw.WriteLine(playerData.GetComponent<SpriteRenderer>().color.b);

        sw.WriteLine(KnowedManager.knowedManager.nowState.ToString());

        sw.WriteLine(BackgroundSoundManager.backgroundSoundManager.nowBackgroundSoundNum);

        int lastSpaceNum;
        switch (lastSpace)
        {
            case Space.HospitalFirst:
                lastSpaceNum = 0;
                break;
            case Space.HospitalSecond:
                lastSpaceNum = 1;
                break;
            case Space.Market:
                lastSpaceNum = 2;
                break;
            case Space.Library:
                lastSpaceNum = 3;
                break;
            case Space.FlowerShop:
                lastSpaceNum = 4;
                break;
            case Space.SpecialRoom:
                lastSpaceNum = 5;
                break;
            case Space.Outside:
                lastSpaceNum = 6;
                break;
            case Space.Puzzle:
                lastSpaceNum = 7;
                break;
            default:
                lastSpaceNum = 0;
                break;
        }

        sw.WriteLine(lastSpaceNum.ToString());

        sw.WriteLine(playerData.transform.position.x.ToString());
        sw.WriteLine(playerData.transform.position.y.ToString());

        sw.WriteLine(sceneNumber.ToString());

        sw.WriteLine(System.DateTime.Now.ToString("yyyy - MM - dd"));

        int toScene = (sceneNumber / 3) + 1;
        switch (sceneNumber % 3)
        {
            case 0:
                sw.WriteLine(toScene.ToString() + "일째 낮");
                break;
            case 1:
                sw.WriteLine(toScene.ToString() + "일째 밤");
                break;
            case 2:
                sw.WriteLine(toScene.ToString() + "일째 어딘가");
                break;
            default:
                break;
        }

        sw.WriteLine(PlayerAnimation.playerAnimation.state.ToString());

        sw.Close();
        fs.Close();

        canInput = true;
        Debug.Log("세이브 완료");
        StartCoroutine(onSaveText());
        Debug.Log("텍스트 킴");
    }

    public void LoadData(int where)
    {
        StartCoroutine(OnLoad(where));
    }

    public void GetEventManager(Scene scene, LoadSceneMode mode)
    {
        thisSceneEventManager = GameObject.FindWithTag("GameEventManager").GetComponent<GameEventManager>();
        isFindGameEventManager = true;
    }










    public void MoveScene(string SceneName, bool _isFirst)
    {
        if (_isFirst) { isFirst = true; } else { isFirst = false; }
        SceneManager.LoadScene(SceneName);
        playerData.transform.position = new Vector2(4f, 10.75f);
        screenShot.FindCamera();
        thisSceneEventManager.isFirst = true;
    }








    private IEnumerator OnLoad(int where)
    {
        Debug.Log("로딩 시작");
        canInput = false;

        isFindGameEventManager = false;

        string filePath = Application.dataPath + "/savingData/GameData" + where.ToString() + ".dat";

        StreamReader sr = new StreamReader(filePath);

        playerData.GetComponent<SpriteRenderer>().color = new Color(float.Parse(sr.ReadLine()), float.Parse(sr.ReadLine()), float.Parse(sr.ReadLine()), 255f / 255f);


        KnowedManager.knowedManager.changeNowState(int.Parse(sr.ReadLine()));

        int backgroundNum = int.Parse(sr.ReadLine());

        if (backgroundNum == -1)
        {
            if (!(BackgroundSoundManager.backgroundSoundManager.nowBackgroundSoundNum == -1))
            {
                BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
            }
        }
        else
        {
            if (BackgroundSoundManager.backgroundSoundManager.nowBackgroundSoundNum == -1)
            {
                BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(backgroundNum, true);
            }
            else if (BackgroundSoundManager.backgroundSoundManager.nowBackgroundSoundNum != backgroundNum)
            {
                BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(backgroundNum, true);
            }
        }

        int lastSpaceNum = int.Parse(sr.ReadLine());
        switch (lastSpaceNum)
        {
            case 0:
                lastSpace = Space.HospitalFirst;
                break;
            case 1:
                lastSpace = Space.HospitalSecond;
                break;
            case 2:
                lastSpace = Space.Market;
                break;
            case 3:
                lastSpace = Space.Library;
                break;
            case 4:
                lastSpace = Space.FlowerShop;
                break;
            case 5:
                lastSpace = Space.SpecialRoom;
                break;
            case 6:
                lastSpace = Space.Outside;
                break;
            case 7:
                lastSpace = Space.Puzzle;
                break;
            default:
                lastSpace = Space.HospitalFirst;
                break;
        }
        changeFootStepSound();

        Vector2 toPlace = new Vector2(0, 0);
        toPlace.x = float.Parse(sr.ReadLine());
        toPlace.y = float.Parse(sr.ReadLine());

        sceneNumber = int.Parse(sr.ReadLine());
        sr.ReadLine();
        sr.ReadLine();
        int stateNum = int.Parse(sr.ReadLine());
        PlayerAnimation.playerAnimation.changeState(stateNum);

        switch (sceneNumber % 3)
        {
            case 0:
                Debug.Log("색 바꿀 것");
                FadeInOutBlack.fadeInOutBlack.SetColor(1);
                break;
            case 1:
                FadeInOutBlack.fadeInOutBlack.SetColor(2);
                break;
            case 2:
                FadeInOutBlack.fadeInOutBlack.SetColor(3);
                break;
            default:
                break;
        }


        FadeInOutBlack.fadeInOutBlack.SetFadeIn(2f);
        yield return new WaitForSeconds(2.001f);


        int toScene = (sceneNumber / 3) + 1;
        if (sceneNumber == 18)
        {
            ShowTextOn("마지막날 낮", 1);
        }
        else
        {
            switch (sceneNumber % 3)
            {
                case 0:
                    Debug.Log("텍스트 올릴 것");
                    ShowTextOn(toScene.ToString() + "일째 낮", 1);
                    break;
                case 1:
                    ShowTextOn(toScene.ToString() + "일째 밤", 2);
                    break;
                case 2:
                    ShowTextOn(toScene.ToString() + "일째 어딘가", 3);
                    break;
                default:
                    break;
            }
        }
        yield return new WaitForSeconds(2);

        switch (sceneNumber)
        {
            case 0:
                MoveScene("FirstDayScene", false);
                break;
            case 1:
                MoveScene("FirstNightScene", false);
                break;
            case 3:
                MoveScene("SecondDayScene", false);
                break;
            case 4:
                MoveScene("SecondNightScene", false);
                break;
            case 5:
                MoveScene("SecondPuzzleScene", false);
                break;
            case 6:
                MoveScene("ThirdDayScene", false);
                break;
            case 7:
                MoveScene("ThirdNightScene", false);
                break;
            case 8:
                MoveScene("ThirdPuzzleScene", false);
                break;
            case 9:
                MoveScene("ForthDayScene", false);
                break;
            case 10:
                MoveScene("ForthNightScene", false);
                break;
            case 11:
                MoveScene("ForthPuzzleScene", false);
                break;
            case 12:
                MoveScene("FifthDayScene", false);
                break;
            case 13:
                MoveScene("FifthNightScene", false);
                break;
            case 14:
                MoveScene("FifthPuzzleScene", false);
                break;
            case 15:
                MoveScene("SixthDayScene", false);
                break;
            case 16:
                MoveScene("SixthNightScene", false);
                break;
            case 17:
                MoveScene("SixthPuzzleScene", false);
                break;
            case 18:
                MoveScene("LastScene", false);
                break;
            default:
                break;
        }


        playerData.transform.position = toPlace; //위치 이동
        

        playerData.loadData(where);

        sr.Close();

        while (!isFindGameEventManager || thisSceneEventManager.isLoad == false)
        {
            Debug.Log("찾는 중");
            yield return new WaitForSeconds(0.01f);
        }

        thisSceneEventManager.LoadData(where);


        ShowTextOff();
        yield return new WaitForSeconds(2.001f);

        FadeInOutBlack.fadeInOutBlack.SetFadeOut(2f);
        yield return new WaitForSeconds(2.001f);
        canInput = true;

    }




    public void ShowTextOn(string text, int where)
    {
        ShowText.text = text;

        switch (where)
        {
            case 1:
                ShowText.color = Color.black;
                break;
            case 3:
            case 2:
                ShowText.color = Color.white;
                break;
            default:
                break;
        }
        FadeInText(1);
    }


    public void ShowTextOff()
    {
        FadeOutText(1);
    }


    public IEnumerator onSaveText()
    {
        SaveUI.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        SaveUI.SetActive(false);
    }


    public void changeFootStepSound()
    {
        switch (lastSpace)
        {
            case GameManager.Space.HospitalFirst:
            case GameManager.Space.HospitalSecond:
                SoundManager.soundManager.FootStepIdx = 0;
                break;
            case GameManager.Space.Market:
                break;
            case GameManager.Space.Library:
            case GameManager.Space.FlowerShop:
            case GameManager.Space.SpecialRoom:
                SoundManager.soundManager.FootStepIdx = 1;
                break;
            case GameManager.Space.Outside:
                SoundManager.soundManager.FootStepIdx = 2;
                break;
            case GameManager.Space.Puzzle:
                SoundManager.soundManager.FootStepIdx = 2;
                break;
            default:
                break;
        }
    }





    private void Update()
    {
        if (isFadeIn)
        {
            FadeIn();

        }
        else if (isFadeOut)
        {
            FadeOut();
        }

    }

    private void FadeInText(float adding)
    {
        r = ShowText.color.r;
        g = ShowText.color.g;
        b = ShowText.color.b;
        ShowText.gameObject.SetActive(true);
        ShowText.color = new Color(r, g, b, 0);
        addingTime = adding;
        startTime = Time.time;
        isFadeIn = true;
    }

    private void FadeOutText(float adding)
    {
        r = ShowText.color.r;
        g = ShowText.color.g;
        b = ShowText.color.b;
        ShowText.color = new Color(r, g, b, 1);
        addingTime = adding;
        startTime = Time.time;
        isFadeOut = true;

    }

    private void FadeIn()
    {
        if (startTime + addingTime > Time.time)
        {
            float Setting = ShowText.color.a + Time.deltaTime / addingTime;
            ShowText.color = new Color(r, g, b, Setting);
        }
        else
        {
            ShowText.color = new Color(r, g, b, 1);
            isFadeIn = false;
        }
    }

    private void FadeOut()
    {
        if (startTime + addingTime > Time.time)
        {
            float Setting = ShowText.color.a - Time.deltaTime / addingTime;
            ShowText.color = new Color(r, g, b, Setting);
        }
        else
        {
            ShowText.color = new Color(r, g, b, 0);
            ShowText.gameObject.SetActive(false);
            isFadeOut = false;
        }
    }








}
