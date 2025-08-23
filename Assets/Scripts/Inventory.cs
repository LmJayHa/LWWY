using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





/*
    ==========================================================
     inventory : 인벤토리에 관한 전반적인 부분을 다루는 스크립트

    (---필수 컴포넌트---)
    1. PlayerInput
    2. PlayerData
    3. PlayerMovement

    (---수정사항---)

    21.07.14 : 임재하 : 기본적인 틀 제작
    21.07.15 : 임재하 : playerMovement추가해서 인벤토리 켜지면 움직이지 않도록 하고 
                        버튼개수 4개로 수정(총 개수 변함 x)
    21.08.19 : 임재하 : 더욱 사용되게 변경
    ==========================================================
     */





public class Inventory : MonoBehaviour
{
    public GameObject InventoryUI; //인벤토리 전체 UI ; 외부에서 지정

    public PlayerInput playerInput; //플레이어의 입력정보 ; 외부에서 지정 -> 플레이어로부터
    public PlayerData playerData; //플레이어의 데이터 ; 외부에서 지정 -> 플레이어로부터
    public PlayerMovement playerMovement; //플레이어의 무브먼트 ; 외부에서 지정 -> 플레이어로부터

    public GameObject[] buttonObjs = new GameObject[16]; //버튼들의 게임오브젝트들 ; 외부에서 지정 ; 총 개수 16개 ; 자손으로 Image 가져야함
    public Image[] buttonImages = new Image[16]; //버튼들의 이미지들 ; 외부에서 지정 ; 총 개수 16개

    //여기서부터 설명창의 데이터멤버들
    public Image explainImage; //설명창의 이미지 ; 외부에서 지정
    public Text explainText; //설명창의 텍스트 ; 외부에서 지정

    public Sprite[] spriteSet; //아이템 코드들에 맞는 이미지 스프라이트들 ; 외부에서 지정 ; 총 개수 미정
    public Sprite noneSprite; //아이템이 없는 자리의 스프라이트 ; 외부에서 지정


    //여기서부터 상단창의 데이터멤버들

    public GameObject CantDeleteUI; //삭제 불가능 UI ; 외부에서 지정

    //여ㅣ서부터 하단창의 버튼들
    public Button[] actionButtons = new Button[2]; //하단 버튼들 ; 외부에서 지정 ; 0-사용 1-버리기


    private bool isOnInventory; //인벤토리가 켜져있는지를 확인하는 변수

    private int selectedButtonNum; //선택된 버튼의 숫자 ; -1은 아무것도 선택하지 않은 상태

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        inventoryOff_NoneSound();
        CantDeleteUI.SetActive(false);
        //인벤토리를 꺼져있는 상태로 바꾼 후 게임에 진입한다
    }

    private void Update()
    {
        if (GameManager.canInput == false) return;
        if (playerInput.inventory)
        {
            SoundManager.soundManager.PlayClickSound();
            if (isOnInventory) { inventoryOff(); }
            else { inventoryOn(); }
        }
        //인벤토리 키가 눌린 경우 -> 인벤토리를 키거나 끈다

        if (playerInput.pause&& isOnInventory)
        {
            SoundManager.soundManager.PlayClickSound();
            inventoryOff();
        }
        //중지키가 눌리고 현재 인벤토리가 켜진 경우 -> 인벤토리를 끈다
    }





    public void inventoryOn()
    {
        if (GameManager.canInput == false) return;

        isOnInventory = true;
        InventoryUI.SetActive(true);
        drawInventory();
        playerMovement.canMove = false;//플레이어 움직임 정지
    }//인벤토리를 켜는 함수


    public void inventoryOff_NoneSound()
    {
        isOnInventory = false;
        InventoryUI.SetActive(false);
        selectedButtonNum = -1;//선택버튼 초기화
        playerMovement.canMove = true;//플레이어 움직임 돌아옴
    }
    public void inventoryOff() {
        SoundManager.soundManager.PlayClickSound();
        Debug.Log("Inventory 버튼 눌림");

        isOnInventory =false;
        InventoryUI.SetActive(false);
        selectedButtonNum = -1;//선택버튼 초기화
        playerMovement.canMove = true;//플레이어 움직임 돌아옴
    }//인벤토리를 끄는 함수 ; escape버튼에 이 함수 적용

    public void onClickButton(int num) {
        SoundManager.soundManager.PlayClickSound();

        selectedButtonNum = num;
        //선택된 버튼을 수정


        /*
         * +)num에 따라 playerData에 있는 데이터를 이용해
         * 설명창에 있는 데이터를 변경한다
         */
        if (selectedButtonNum == -1)
        {
            explainText.text = "";
            explainImage.sprite = noneSprite;
        }
        else
        {
            if (playerData.codesOfHavingItems.Count - 1 < selectedButtonNum)
            {
                explainText.text = "";
                explainImage.sprite =noneSprite;
            }
            else
            {
                changeText();
                explainImage.sprite = spriteSet[playerData.codesOfHavingItems[selectedButtonNum]];
            }
        }
        

    }//아이템을 선택하는 함수 ; 아이템 버튼들에 이 함수를 적용


    public void onClickUseButton() {
        SoundManager.soundManager.PlayClickSound();
        if (playerData.codesOfHavingItems.Count - 1 < selectedButtonNum || selectedButtonNum == -1) return;

        switch (playerData.codesOfHavingItems[selectedButtonNum])
        {
            case 0:
                selectedButtonNum = -1;
                playerData.removeItem(0);
                GameManager.gameManager.thisSceneEventManager.EndEvent_toNPC(2);
                SoundManager.soundManager.PlayEffectClip(3);
                drawInventory();
                break;
            case 1:
                inventoryOff();
                ChatManager.chatManager.OpenChat(254,null);
                break;
            case 2:
                selectedButtonNum = -1;
                playerData.removeItem(2);
                drawInventory();
                inventoryOff();
                ChatManager.chatManager.OpenChat(255, null);
                SoundManager.soundManager.PlayEffectClip(3);
                break;
            case 3:
                inventoryOff();
                ChatManager.chatManager.OpenChat(256, null);
                break;
            case 4:
                inventoryOff();
                ChatManager.chatManager.OpenChat(257, null);
                break;
            default:
                break;
        }
    }//아이템을 사용하는 함수

    public void onClickDeleteButton() {
        SoundManager.soundManager.PlayClickSound();
        if (playerData.codesOfHavingItems.Count - 1 < selectedButtonNum||selectedButtonNum==-1) return;

        switch (playerData.codesOfHavingItems[selectedButtonNum])
        {
            case 0:
                StartCoroutine(SeeCantDeleteUI());
                break;
            case 1:
                StartCoroutine(SeeCantDeleteUI());
                break;
            case 2:
                StartCoroutine(SeeCantDeleteUI());
                break;
            case 3:
                playerData.removeItem(3);
                drawInventory();
                break;
            case 4:
                StartCoroutine(SeeCantDeleteUI());
                break;
            default:
                break;
        }
    }//아이템을 삭제하는 함수

    public void drawInventory() {

        explainText.text = "";
        explainImage.sprite = noneSprite;

        for (int i = 0; i < playerData.sizeOfCodesOfHavingItems; i++)
        {
            buttonImages[i].sprite = spriteSet[playerData.codesOfHavingItems[i]];
        }

        for (int i = playerData.sizeOfCodesOfHavingItems; i < 16; i++)
        {
            buttonImages[i].sprite = noneSprite;
        }
    }//버튼들의 이미지를 플레이어 데이터에 따라 적용 시키는 함수






    public void changeText() {
        switch (playerData.codesOfHavingItems[selectedButtonNum])
        {
            case 0:
                explainText.text = "간호사가 건네어준 약이다.";
                break;
            case 1:
                explainText.text = "마트에서 구매한 초콜릿이다.";
                break;
            case 2:
                explainText.text = "마트에서 구매한 샌드위치이다.";
                break;
            case 3:
                explainText.text = "다 녹아버린 초콜릿이다.";
                break;
            case 4:
                explainText.text = "놀이마당에서 얻어낸 별모양 머리핀이다.";
                break;
            default:
                break;
        }

    }//텍스트를 바꾸는 함수

    public IEnumerator SeeCantDeleteUI() {
        CantDeleteUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        CantDeleteUI.SetActive(false);
    }//삭제 불가능 UI켰다 끄기


}
