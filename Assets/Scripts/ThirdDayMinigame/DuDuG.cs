using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuDuG : MonoBehaviour
{
    public enum State{RED,BLUE,GREEN};
    public State state;

    public bool isInGame = false;

    public bool isOn; //두더지가 올라온 상태인지 아닌지
    public float stayTime; //두더지가 올라오고 기다릴 시간
    public float nextTime; //올라오기까지 기다릴 시간
    public int point;
    public float lastStopTime;

    public GameObject[] Buttons;

    public float checkTime { get { return lastStopTime + nextTime + stayTime; } }

    GameObject[] DuduG = new GameObject[3];

    public int seedNum;

    System.Random rn;

    private void Awake()
    {
        rn =  new System.Random(seedNum + (int)Time.time);
    }


    public void SetState() {
 
        int idx = rn.Next() % 3;
        //랜덤으로 지정

        for (int i = 0; i < 3; i++)
        {
            Buttons[i].SetActive(false);
        }

        isOn = false;

        switch (idx)
        {
            case 0:
                state = State.RED;
                stayTime = 2f;
                nextTime = 3f;
                point = 10;
                break;
            case 1:
                state = State.BLUE;
                stayTime = 1f;
                nextTime = 2f;
                point = 15;
                break;
            case 2:
                state = State.GREEN;
                stayTime = 0.75f;
                nextTime = 4f;
                point = 20;
                break;
            default:
                break;
        }

        lastStopTime = Time.time;

    }

    public void DuDuGOn() {
        isOn = true;
        switch (state)
        {
            case State.RED:
                Buttons[0].SetActive(true);
                break;
            case State.BLUE:
                Buttons[1].SetActive(true);
                break;
            case State.GREEN:
                Buttons[2].SetActive(true);
                break;
            default:
                break;
        }
    }








    private void Update()
    {
        if (!isInGame) { return; }

        if (checkTime <= Time.time && isOn)
        {
            SetState();
        }
        else if (lastStopTime + nextTime <= Time.time && !isOn) {
            DuDuGOn();
        }
    }
}
