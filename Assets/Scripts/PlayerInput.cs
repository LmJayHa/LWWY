using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/*
    ==========================================================
     playerInput : 키보드나 마우스의 입력을 감지하는 스크립트

    (---수정사항---)

     21.07.03 : 임재하 : 기본적인 틀 제작
     21.07.20 : 임재하 : phone 제작
     21.07.23 : 임재하 : interection 제작
    ==========================================================
     */





public class PlayerInput : MonoBehaviour
{
    //축과 입력되는 이름
    private string movingAxisNameWidth ="Horizontal"; //가로축의 입력 이름
    private string movingAxisNameHeight = "Vertical"; //세로축의 입력 이름

    private string inventoryButtonName = "Inventory"; //인벤토리 버튼 이름 : 기본 e로 설정
    private string pauseButtonName = "Pause"; //퍼즈 버튼 이름 : 기본 escape로 설정
    private string knowedButtonName = "Knowed"; //인벤토리 버튼 이름 : 기본 k로 설정
    private string phoneButtonName = "Phone"; //폰 버튼 이름 : 기본 p로 설정
    private string interectionButtonName = "Interection"; //상호작용 버튼 이름 : 기본 e로 설정
    private string runButtonName = "Run"; //상호작용 버튼 이름 : 기본 shift로 설정


    //입력값
    public float moveRateWidth { get; private set; }
    public float moveRateHeight { get; private set; }

    public bool inventory { get; private set; }
    public bool pause { get; private set; }
    public bool knowed { get; private set; }
    public bool phone { get; private set; }
    public bool interection { get; private set; }
    public bool run { get; private set; }

    public bool canRun=true;

    public bool isMoving {
        get
        {
            if (moveRateWidth == 0f && moveRateHeight == 0f)
            {
                return false;
            }
            else {
                return true;
            }
        }
    } //현재 움직이고 있는 중인지


    void Awake()
    {
        DontDestroyOnLoad(this);
        moveRateWidth = 0f;
        moveRateHeight = 0f;
        //isMoving의 정확한 값을 위하여 먼저 축의 이동 값을 0으로 설정
}

    void Update()
    {
        moveRateWidth = Input.GetAxisRaw(movingAxisNameWidth);
        moveRateHeight = Input.GetAxisRaw(movingAxisNameHeight);

        inventory = Input.GetButtonDown(inventoryButtonName);
        pause = Input.GetButtonDown(pauseButtonName);
        knowed = Input.GetButtonDown(knowedButtonName);
        phone = false;
        interection = Input.GetButtonDown(interectionButtonName);
        if (canRun)
        {
            run = Input.GetButton(runButtonName);
        }
        else { run = false; }
        //값을 불러옴
    }

}
