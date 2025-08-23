using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/*
    ==========================================================
     ScreenShot : phone의 스크린샷을 구현하는 스크립트

    (---수정사항---)

    21.07.23 : 임재하 : 기본적인 틀 제작
    ==========================================================
     */



public class ScreenShot : MonoBehaviour
{
    public Camera mainCamera;       //보여지는 카메라.

    private int screenWidth; //스크린의 총 가로
    private int screenHeight; //스크린의 총 세로
    private string savePath; //저장할 경로

    public int screenshotWidth = 150; //스크린샷을 찍을 가로
    public int screenshotHeight = 250; //스크린샷을 찍을 세로

    void Awake()
    {
        FindCamera();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        savePath = Application.dataPath + "/Resources/";
    }//변수를 초기화

    public void DoScreenShot(int num)
    {

        DirectoryInfo dir = new DirectoryInfo(savePath);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(savePath);
        }
        //경로가 존재하지 않을 시 디렉토리(파일)을 만들어 줌


        string toSaveName = savePath + num.ToString() + "picture.png";
        //경로를 정확히 설정
        Debug.Log(toSaveName);

        if (File.Exists(toSaveName))
        {
            Debug.Log("이미 있음");
            File.Delete(toSaveName);
        }
        //이미 있는 자리에 찍은 사진이면 제거

        ScreenCapture.CaptureScreenshot(toSaveName);
    }

    public void DeleteScreenShot(int num)
    {
        string toSaveName = savePath + num.ToString() + ".png";
        //경로를 정확히 설정

        if (File.Exists(toSaveName))
        {
            File.Delete(toSaveName);
        }
    }


    public void FindCamera() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

}
