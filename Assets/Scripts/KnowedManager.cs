using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


/*
    ==========================================================
    knowedManager: 알림창에 대한 메니지먼트를 다룬 스크립트

    (---필수 컴포넌트---)
    1. PlayerInput
    2. PlayerMevement
    3. PlayerData

    (---수정사항---)

    21.07.18 : 채우재 : knowed창을 닫을때 버튼으로도 닫을수 있게 구현함
                        ChangeWilldo함수, Imgunlock함수 생성 내용구현x            
    
    21.07.19 : 임재하 : knowed에대한 메니지먼트 전면 수정


    ==========================================================
     */









public class KnowedManager : MonoBehaviour
{
    public static KnowedManager knowedManager
    {
        get
        {
            if (knowed_instance == null)
            {
                knowed_instance = FindObjectOfType<KnowedManager>();
            }

            return knowed_instance;
        }
    }//SoundManager를 싱글턴으로 설정

    private static KnowedManager knowed_instance; //싱글턴에 이용된 인스턴스


    private string[] StateSet = {
    "1층에 있는 원장을 만나자.",
    "병원을 둘러보자.",
    "간호사가 건네어준 약을 먹자.",
    "간호사에게 다시 말을 걸자.",
    "오늘은 이만 자자.",
    "화장실에 가자.",

    "특실에 가보자.",
    "특실을 나가자.",
    "다시 잠에 들러 가자.",
    "1층의 원장에게 가자.",
    "도서관에 가보자.",

    "책을 구경하자.",
    "밖에 나가보자.",
    "마트에서 물건을 고르자.",
    "계산을하자.",
    "특실로 가보자.",

    "특실에 들어가보자.",
    "이만 자러가자.",
    "...",
    "특실로 가보자.",
    "장치들을 살펴보자.",

    "2층에 올라가 에델을 만나자.",
    "특실로 돌아가 사과하자.",
    "1층의 원장에게 가자.",
    "밖으로 나가보자.",
    "게임들을 클리어하자.",

    "상품을 받자.",
    "특실로 돌아가자.",
    "퍼즐을 해결하자.",
    "어린 에델을 따라서 올라가자.",
    "꽃집에 가보자.",

    "알렉스 집으로 가보자.",
    "서쪽숲에서 에델바이스를 찾자.",
    "특실에가서 에델을 만나자.",
    "마트에 가보자.",
    "병원에서 물품을 찾아보자.",

    "특실로 가보자.",
    "광장으로 가자.",
    "특실에 가보자.",
    "병원으로 가자.",
    "원장에게 가보자.",

    "특실로 가보자.",
    "퇴원하러 가자.",
    "고목나무숲으로 가자.",
    "그래 이제 다 기억났니?"
    };





    public GameObject knowedUI; // knowed UI를 받아줄 오브젝트 ; 외부에서 지정
    public GameObject desText; // knowed 슬롯에 있는 조각 설명텍스트를 넣을곳 description text의 약자 ; 외부에서 지정
    public GameObject willDoText; // 가장 위 슬롯에 있는 해야 할 일에 대한 텍스트를 넣을곳 ; 외부에서 지정
    public GameObject[] btns;  // knowed의 버튼들 ; 외부에서 지정 ; 필요한 버튼의 갯수 -> 개발하며 조정 현재 16개
    public Image[] btnImages; // knowed버튼들의 이미지들 ; 외부에서 지정

    public Text ToDoText; //지금 할 일에 대한 텍스트 ; 외부에서 지정

    public Sprite[] spriteSet; //이미지창고 ; 외부에서 지정
    public Sprite noneSprite; //안되는이미지 ; 외부에서 지정

    public PlayerInput playerInput; // Player의 입력정보 ; 외부에서 지정
    public PlayerMovement playerMovement; // Player의 움직임 ; 외부에서 지정
    public PlayerData playerData; //Player의 데이터들 ; 외부에서 지정

    private bool isActiveKnowed; // Knowed창이 켜져 있는지 확인 할 변수

    public int nowState = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isActiveKnowed = false;
        knowedUI.SetActive(isActiveKnowed); //knowed창을 끔
        changeNowState(0);
    }

    void Update()
    {
        if (GameManager.canInput == false) return;

        if (playerInput.knowed)
        {
            SoundManager.soundManager.PlayClickSound();
            if (isActiveKnowed)
            {
                knowedUI.SetActive(false); // 창을 닫음
                isActiveKnowed = !isActiveKnowed;  // activeKnowed의 상태를 반전시킴
                playerMovement.canMove = true; // 움직임 통제
            }
            else
            {
                knowedUI.SetActive(true); // 창을 엶
                desText.GetComponent<Text>().text = ""; //텍스트 초기와
                drawKnowed(); // knowed창 갱신
                isActiveKnowed = !isActiveKnowed;  // activeKnowed의 상태를 반전시킴
                playerMovement.canMove = false; // 움직임 통제
            }
        }

        if (playerInput.pause && isActiveKnowed)
        {
            SoundManager.soundManager.PlayClickSound();
            knowedUI.SetActive(false); // 창을 닫음
            isActiveKnowed = !isActiveKnowed;  // activeKnowed의 상태를 반전시킴
            playerMovement.canMove = true; // 움직임 통제
        }
    }





    public void OnClickBtn(int num) // 클릭하면 버튼에 따라 다른 정수가 num에 입력됨
    {
        SoundManager.soundManager.PlayClickSound();
        for (int count = 0; count <= btns.Length; count++) // 버튼수만큼 반복되는 반복문
        {
            if (count == num)  // 선택한 번호가 나올때까지 count를 올려가면서 찾음
            {
                Print(num); // 누르는 버튼에 따라서 다른 문장이 출력되야됨 
            }
        }
    }


    public void CloseKnowed()   // Knowed켜는 함수
    {
        SoundManager.soundManager.PlayClickSound();
        playerMovement.canMove = true;
        knowedUI.SetActive(false);
        isActiveKnowed = !isActiveKnowed; // 이 문장을 안쓰면 버튼을 눌러서 knowed종료시 k를 두번 눌러야 knowed가 켜지는 오류발생
    }




    public void Print(int idx) // 누르는 버튼에 따라서 다른 문장을 출력해주는 함수
    {

        // +)개발을 하며 필요한 양만큼 늘려서 사용
        switch (idx)
        {
            case 0:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "특실에서 무언가 이상한 빛이 흘러나오는 것을 봤다.. 특실도 매우 이상했는데, 단순히 내 착각이었나?"; } else { desText.GetComponent<Text>().text = ""; }
                break;
            case 1:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "늦은 밤 특실에 들어가니 누군가의 기억을 봤다. 카밀리아라는 사람이었는데? "; } else { desText.GetComponent<Text>().text = ""; }
                break;
            case 2:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "특실에서 본 기억의 에델이라는 여자는 그때 본 기억의 꼬마 여자애였다."; } else { desText.GetComponent<Text>().text = ""; }
                break;
            case 3:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "특실의 빛, 그리고 카밀리아의 기억은 매일 반복되고 있다.."; } else { desText.GetComponent<Text>().text = ""; }
                break;
            case 4:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "카밀리아와 에델은 정말 둘도없는 친구였나보다. 그녀의 머리핀도 에델이 선물한 것이다."; } else { desText.GetComponent<Text>().text = ""; }
                break;
            case 5:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "무언가 기억이 한가지 떠올랐다! 나에게 매우 소중 기억인것 같은데."; } else { desText.GetComponent<Text>().text = ""; }
                break;
            case 6:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "에델의 상태는 점점 악화되고있음에, 카밀리아는 기증까지 생각하고 있었다."; } else { desText.GetComponent<Text>().text = ""; }
                break;
            case 7:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "카밀리아가 고목나무근처에서 에델과 찍은 소중한 사진을 잃어버렸다."; } else { desText.GetComponent<Text>().text = ""; }
                break;
            case 8:
                if (playerData.isOpenKnoweds[idx]) { desText.GetComponent<Text>().text = "모든게 다.. 기억났다 내 이름은 에델이다."; } else { desText.GetComponent<Text>().text = ""; }
                break;
            default:
                break;
        }
    }

    public void ChnageWilldo(string str) // 특정 조건이 성립되면 knowed창의 해야할일을 바꿔주는 함수, 특정 조건을 성립하면 정수를 받아오도록 설계
    {
        willDoText.GetComponent<Text>().text = str;
    }


    public void drawKnowed()
    {
        for (int i = 0; i < btnImages.Length; i++)
        {
            if (playerData.isOpenKnoweds[i])
            {
                btnImages[i].sprite = spriteSet[i];
            }
            else
            {
                btnImages[i].sprite=noneSprite;
            }
        }
    }

    public void changeNowState(int idx) {
        Debug.Log("Knowed변경");
        nowState = idx;
        ToDoText.text = StateSet[nowState];
    }

}


