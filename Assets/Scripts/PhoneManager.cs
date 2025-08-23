using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





/*
    ==========================================================
     phone : 핸드폰 기믹에관한 전반적인 부분을 다루는 스크립트

    (---필수 컴포넌트---)
    1. PlayerInput
    2. PlayerData

    (---수정사항---)

    21.07.20 : 임재하 : 기본적인 틀 제작
    21.07.23 : 임재하 : 갤러리와 스크린샷 구현
    ==========================================================
     */





public class PhoneManager : MonoBehaviour
{
    public GameObject phoneUI; //핸드폰 전체 UI ; 외부에서 지정

    public GameObject applicationUI; //앱을 선택하는 메인 UI ; 외부에서 지정
    public GameObject callScreenUI; //전화거는 스크린의 UI ; 외부에서 지정
    public GameObject screenshotScreenUI; //사진찍는 스크린의 UI ; 외부에서 지정
    public GameObject galleryScreenUI; //갤러리 스크린의 UI ; 외부에서 지정
    public GameObject noteScreenUI; //노트 스크린의 UI ; 외부에서 지정

    public PlayerInput playerInput; //플레이어의 입력정보 ; 외부에서 지정 -> 플레이어로부터
    public PlayerData playerData; //플레이어의 데이터 ; 외부에서 지정 -> 플레이어로부터



    //================노트에 관련된 데이터들=======================
    public GameObject seeNoteSceneUI; //노트 내용을 보여주는 UI세트 ; 외부에서 지정
    public InputField noteInputField; //노트에서 나타날 문자열의 텍스트


    private int isWhere_Note; //어떤 노트를 눌렀는지에 대한 변수 ; -1은 아무것도 선택하지 않음
    private bool isFirstTimeNote; //노트를 킨 직후인지
    //=============================================================

    //================스크린샷에 관련된 데이터들===================
    public GameObject cantScreenShotUI; //사진을 찍을 수 없습니다의 UI세트; 외부에서 지정

    private ScreenShot screenShot; //스크린샷 컴포넌트
    //=============================================================

    //================갤러리에 관련된 데이터들===================
    public GameObject checkPictureUI; //사진을 보여주는 UI세트; 외부에서 지정
    public GameObject[] galleyButtonObjs = new GameObject[16]; //갤러리의 버튼들 ; 외부에서 지정 ; 총 16개
    public Image showImage; //갤러리의 확대 이미지 ; 외부에서 지정

    private int isWhere_Gallery; //어떤 사진을 눌렀는지에 대한 변수 ; -1은 아무것도 선택하지 않음
    //=============================================================


    public enum PhoneApplication {
        CALL,SCREENSHOT,GALLERY,NOTE,NONE
    }

    public PhoneApplication nowPhoneApplication; //현 핸드폰의 상태

    private bool isOnPhone; //핸드폰이 켜져있는지에 대한 변수

    private float startTime; //어떠한 일이 시작될 때 시작될 시간

    private bool isMovingUp; //핸드폰이 올라가고 있는 중인가?
    private bool isMovingDown; //핸드폰이 내려가고 있는 중인가?

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        phoneUI.SetActive(true);

        phoneUI.SetActive(true);
        applicationUI.SetActive(true);

        callScreenUI.SetActive(false);
        screenshotScreenUI.SetActive(false);
        galleryScreenUI.SetActive(false);
        noteScreenUI.SetActive(false);

        seeNoteSceneUI.SetActive(false);

        screenShotCameraOff();
        screenShot = GetComponent<ScreenShot>();

        isOnPhone = false;
        isMovingUp = false;
        isMovingDown = false;

        nowPhoneApplication = PhoneApplication.NONE;

        isWhere_Gallery = -1;
        isWhere_Note = -1;
        isFirstTimeNote = false;
    }
    //핸드폰은 꺼진 상태로 시작 움직이지는 않고 각종 요소들 초기화

    private void Update()
    {


        if (playerInput.phone) {
            if (!isMovingUp && !isMovingDown)
            {
                if (isOnPhone)
                {
                    PhoneOff();
                }
                else
                {
                    PhoneOn();
                }
            }
        }

        if (playerInput.pause && isOnPhone) {
            if (!isMovingUp && !isMovingDown)
            {
                PhoneOff();
            }
        }

        if (isMovingUp) PhoneMoveUp();

        if (isMovingDown) PhoneMoveDown();

        if (isFirstTimeNote) { isFirstTimeNote = false; OnClickReturnChoiceNoteButton(); }

    }
    //핸드폰의 꺼짐 켜짐과 이동을 담당





    


    public void PhoneOn() {
        if (GameManager.canInput == false) return;

        phoneUI.SetActive(true);
        applicationUI.SetActive(true);

        callScreenUI.SetActive(false);
        screenshotScreenUI.SetActive(false);
        galleryScreenUI.SetActive(false);
        noteScreenUI.SetActive(false);

        seeNoteSceneUI.SetActive(false);

        isOnPhone = true;

        startTime = Time.time;
        isMovingUp = true;
    }//핸드폰을 켜는 함수

    public void PhoneOff() {
        isOnPhone = false;

        nowPhoneApplication = PhoneApplication.NONE;

        screenShotCameraOff();

        isWhere_Note = -1;
        isWhere_Gallery = -1;

        startTime = Time.time;
        isMovingDown = true;
    }//핸드폰을 끄는 함수

    private void PhoneMoveUp()
    {
        phoneUI.transform.Translate(0f, 400f * Time.deltaTime,0f);
        if (Time.time >= startTime + 1f) { isMovingUp = false; }
    } //핸드폰을 위로 이동시키는 함수

    public void PhoneMoveDown()
    {
        phoneUI.transform.Translate(0f, -400f * Time.deltaTime, 0f);
        if (Time.time >= startTime + 1f) { isMovingDown = false; }
    }//핸드폰을 아래로 이동시키는 함수







    public void OnClickApplicationButton(int toChange) {
        switch (toChange)
        {
            case 0:
                break;
            case 1:nowPhoneApplication = PhoneApplication.GALLERY;
                applicationUI.SetActive(false);
                galleryScreenUI.SetActive(true);
                DrawGallery();
                break;
            case 2:
                nowPhoneApplication = PhoneApplication.SCREENSHOT;
                applicationUI.SetActive(false);
                screenshotScreenUI.SetActive(true);
                break;
            case 3:
                nowPhoneApplication = PhoneApplication.NOTE;
                applicationUI.SetActive(false);
                noteScreenUI.SetActive(true);
                isFirstTimeNote = true;
                break;
            case 4:
                break;
            default:
                break;
        }

    }//핸드폰 첫 화면에서 각 앱의 버튼을 누를 경우 실행될 함수 -> UI를 보여줄 것
    /*
     3 : Note
         */


    public void OnClickReturnApplicatinButton()
    {
        switch (nowPhoneApplication)
        {
            case PhoneApplication.CALL:
                break;
            case PhoneApplication.SCREENSHOT:
                nowPhoneApplication = PhoneApplication.NONE;
                applicationUI.SetActive(true);
                screenshotScreenUI.SetActive(false);
                break;
            case PhoneApplication.GALLERY:
                nowPhoneApplication = PhoneApplication.NONE;
                applicationUI.SetActive(true);
                galleryScreenUI.SetActive(false);
                break;
            case PhoneApplication.NOTE:
                nowPhoneApplication = PhoneApplication.NONE;
                applicationUI.SetActive(true);
                noteScreenUI.SetActive(false);
                break;
            case PhoneApplication.NONE:
                break;
            default:
                break;
        }
    }//핸드폰 첫 화면으로 돌아가는 함수






    //===================Note=====================
    public void OnClickNoteButton(int num) {
        isWhere_Note = num;

        if (isWhere_Note == -1) return;

        seeNoteSceneUI.SetActive(true);
        noteScreenUI.SetActive(false);

        if (playerData.stringsOfNotes[num] == null)
        {
            playerData.stringsOfNotes[num]="";
            noteInputField.text = playerData.stringsOfNotes[num];
        }
        else {
            noteInputField.text = playerData.stringsOfNotes[num];
        }
    }//각 노트의 버튼에 대한 함수 -> 화면을 옮겨주고 Text를 새로 써 줌

    public void OnClickSaveButton() {
        if (isWhere_Note == -1) return;

        playerData.stringsOfNotes[isWhere_Note] = noteInputField.text;
    }//저장 버튼을 눌렀을 때 나타날 함수 -> 스트링 데이터가 저장됨

    public void OnClickReturnChoiceNoteButton() {
        isWhere_Note = -1;

        seeNoteSceneUI.SetActive(false);
        noteScreenUI.SetActive(true);

        noteInputField.text = "";
    }//노트 선택 화면으로 돌아가는 함수
     //============================================


    //===============Screenshot========================
    public void Photograph() {

        int toSaveIdx=-1;

        for (int i = 0; i < playerData.isSavedPicture.Length; i++)
        {
            if (playerData.isSavedPicture[i] == false) { toSaveIdx = i; break; }
        }

        if (toSaveIdx == -1)
        {
            //저장공간이 없는 경우
            StartCoroutine(ShowCantScreenShot());
        }
        else {
            //저장공간이 있는 경우
            screenShot.DoScreenShot(toSaveIdx);
            playerData.isSavedPicture[toSaveIdx] = true;
        }

    }//사진을 찍도록 명령하는 함수

    private IEnumerator ShowCantScreenShot() {
        cantScreenShotUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        cantScreenShotUI.SetActive(false);
    }//잠시동안 저장공간이 없다는 UI를 띄울 코루틴


    public void screenShotCameraOff() {
        cantScreenShotUI.SetActive(false);
    }
    //=======================================================

    //===============Gallery========================
    public void DrawGallery() {
        for (int i = 0; i < playerData.isSavedPicture.Length; i++)
        {
            if (playerData.isSavedPicture[i])
            {
                galleyButtonObjs[i].SetActive(true);
            }
            else {
                galleyButtonObjs[i].SetActive(false);
            }
        }
    }//갤러리를 갱신시키는 함수

    public void OnClickCheckPicture(int idx) {
        checkPictureUI.SetActive(true);
        string path = idx.ToString()+"picture";
        showImage.sprite = Resources.Load<Sprite>(path);
        isWhere_Gallery = idx;
    }//사진버튼을 눌렀을 경우

    public void OnClickReturnGallery() {
        checkPictureUI.SetActive(false);
        isWhere_Gallery = -1;
    }//갤러리로 돌아가는 함수

    public void OnClickDeletePicture()
    {
        if (isWhere_Gallery == -1) return;
        playerData.isSavedPicture[isWhere_Gallery] = false;
        DrawGallery();
        OnClickReturnGallery();
    }//사진을 삭제하는 함수
    //=======================================================

}
