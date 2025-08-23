using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondDayMiniPuzzle : MonoBehaviour
{
    //미니 퍼즐 관련 변수들

    private bool isOpenPuzzle = false;

    private int selectedNum = 0;
    private int selectedEmpty;

    private int[] placeNumber = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };

    public int[] cardNumber = new int[9];


    public Sprite[] images;
    public Sprite emptyImage;

    private bool isLookCard = false;

    public GameObject UI;
    public GameObject CanBuy;

    public GameObject[] empties;
    public GameObject[] cards;

    public Image[] empties_I;
    public Image[] cards_I;

    public GameObject cursor;


    public int number;
    public SecondDayGameEventManager secondDayGameEventManager;

    public void OpenPuzzle() {

        

        cursor.transform.position = new Vector2(empties[0].transform.position.x, empties[0].transform.position.y+55);
        selectedNum = 0;
        isLookCard = false;
        isOpenPuzzle = true;

        for (int i = 0; i < 9; i++)
        {
            cards_I[i].sprite = images[cardNumber[i]];
            empties_I[i].sprite = emptyImage;
        }
        StartCoroutine(canInputFalseDalay());
    }
    public IEnumerator canInputFalseDalay() {
        yield return new WaitForSeconds(1f);
        GameManager.canInput = false;
        UI.SetActive(true);
        Debug.Log(GameManager.canInput);
        isOpenPuzzle = true;
    }

    public void ClosePuzzle()
    {
        GameManager.canInput = true;

        UI.SetActive(false);
        CanBuy.SetActive(false);
        SoundManager.soundManager.PlayEffectClip(14);
        if (number == 0) { secondDayGameEventManager.EndMiniGame_0(); }
        else { secondDayGameEventManager.EndMiniGame_1(); }
        isOpenPuzzle = false;
    }


    private void cursorMove()
    {
        if (isLookCard)
        {
            cursor.transform.position = new Vector2(cards[selectedNum].transform.position.x, cards[selectedNum].transform.position.y+55);
        }
        else
        {
            cursor.transform.position = new Vector2(empties[selectedNum].transform.position.x, empties[selectedNum].transform.position.y+55);
        }
    }






    // Update is called once per frame
    void Update()
    {
        if (isOpenPuzzle) {



            if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D)) {
      
                    switch (selectedNum)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            selectedNum++;
                            break;
                        case 8:
                            selectedNum = 0;
                            break;
                        default:
                            break;
                    }
                    cursorMove();
                
            } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                switch (selectedNum)
                {
                    case 8:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        selectedNum--;
                        break;
                    case 0:
                        selectedNum = 8;
                        break;
                    default:
                        break;
                }
                cursorMove();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (isLookCard)
                {
                    switch (selectedNum)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            selectedNum +=2;
                            break;
                        case 7:
                            selectedNum = 1;
                            break;
                        case 8:
                            selectedNum = 0;
                            break;
                        default:
                            break;
                    }
                    cursorMove();
                }
                else {
                    switch (selectedNum)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            selectedNum+=3;
                            break;
                        case 6:
                            selectedNum=0;
                            break;
                        case 7:
                            selectedNum=1;
                            break;
                        case 8:
                            selectedNum=2;
                            break;
                        default:
                            break;
                    }
                    cursorMove();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (isLookCard)
                {
                    switch (selectedNum)
                    {
                        case 0:
                            selectedNum = 8;
                            break;
                        case 1:
                            selectedNum = 7;
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            selectedNum -= 2;
                            break;
                        default:
                            break;
                    }
                    cursorMove();
                }
                else
                {
                    switch (selectedNum)
                    {
                        case 0:
                            selectedNum = 6;
                            break;
                        case 1:
                            selectedNum = 7;
                            break;
                        case 2:
                            selectedNum = 8;
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            selectedNum -= 3;
                            break;
                        default:
                            break;
                    }
                    cursorMove();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isLookCard) {
                    bool isEmpty = false;
                    for (int i = 0; i < 9; i++)
                    {
                        if (placeNumber[i] == selectedNum) isEmpty = true;
                    }

                    if (!isEmpty)
                    {
                        if (placeNumber[selectedEmpty] == -1)
                        {
                            placeNumber[selectedEmpty] = selectedNum;
                            empties_I[selectedEmpty].sprite = images[cardNumber[selectedNum]];
                            cards_I[selectedNum].sprite = emptyImage;
                        }
                        else {
                            cards_I[placeNumber[selectedEmpty]].sprite= images[cardNumber[placeNumber[selectedEmpty]]];
                            cards_I[selectedNum].sprite = emptyImage;
                            empties_I[selectedEmpty].sprite = images[cardNumber[selectedNum]];
                            placeNumber[selectedEmpty] = selectedNum;
                        }
                    }
                    else {
                        if (placeNumber[selectedEmpty] != -1)
                        {
                            empties_I[selectedEmpty].sprite = emptyImage;
                            cards_I[placeNumber[selectedEmpty]].sprite = images[cardNumber[placeNumber[selectedEmpty]]];
                            placeNumber[selectedEmpty] = -1;
                        }
                        else {
                        }
                    }

                    cursor.transform.position = new Vector2(empties[0].transform.position.x, empties[0].transform.position.y + 55);
                    selectedNum = 0;
                    isLookCard = false;

                }
                else {
                    selectedEmpty = selectedNum;
                    isLookCard = true;
                    selectedNum = 0;
                    cursorMove();
                }
                bool isCorrect = true;
                for (int i = 0; i < 9; i++)
                {
                    if (placeNumber[i] == -1) { isCorrect = false; break; }
                    if (cardNumber[placeNumber[i]] != i) { isCorrect = false; break; }
                }

                if (isCorrect) {
                    ClosePuzzle();
                    Debug.Log("성공");

                }
            }

        }
    }
}
