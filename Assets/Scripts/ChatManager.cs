using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Playables;
using UnityEngine.Timeline;
/*
==========================================================
ChatManager : 게임내의 대화창을 관리하는 스트립트 : 싱글턴

(---필수 컴포넌트---)
1. PlayerMovement

현재 버튼을 눌러서 대화 실행후 z키를 눌러 다음 대화로 
넘어가도록 설정해둠 추후에 대화 실행조건을 변경하면 됨
대화 시작하면 움직임역시 제한되고 대화가끝나면 다시 움직임가능

==========================================================
*/


public class ChatManager : MonoBehaviour
{
    public static ChatManager chatManager {
        get
        {
            if (chatM_instance == null) {
                chatM_instance = FindObjectOfType<ChatManager>();
            }

            return chatM_instance;
        }
    }//chatManager를 싱글턴으로 설정

    private static ChatManager chatM_instance; //싱글턴에 이용된 인스턴스

    public PlayerMovement playerMovement;

    public GameObject chatUI; // 대화창 ui를 띄울것 
    public Text chatText; //  대화창에 표시될 텍스트
    public Image chatBackGround; // 대화창 이미지

    private Color playerColor = new Color(255f / 255f, 212f / 255f, 148f / 255f, 201f / 255f);
    private Color nurseColor= new Color(246f/255f, 133f / 255f, 233f / 255f, 201f / 255f);
    private Color doctorColor = new Color(174f / 255f, 243f / 255f, 255f / 255f, 201f / 255f);
    private Color npcColor = new Color(135f / 255f, 135f / 255f, 135f / 255f, 201f / 255f);
    private Color clerkColor = new Color(147f / 255f, 124f / 255f, 255f / 255f, 201f / 255f);
    private Color heroinColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 201f / 255f);
    private Color alexColor = new Color(166f / 255f, 58f / 255f, 58f / 255f, 201f / 255f);

    public Image[] SceneImages; //컷씬 이미지들

   

    private StreamReader streamReader; //대화를 받아올 스트림리더

    private bool canNextChat; //다음 챗으로 넘어갈 수 있는지

    public bool isChat = false; // 현재 대화창이 켜져있는지 꺼져있는지 확인하는 변수

    

    public delegate void EndListner();
    EndListner endListner;




    private Vector2 ChatUiPosition;
    private Vector2 ChatUiMinusPosition;

    int index; //인덱스
    float startTime; //시작 시간
    float usingTime; //걸리는 시간

    string ToChangeColor;
    bool isColorChanged;

    float FadeValue;


    private bool isStart;
    private bool isFadeIn;
    private bool isFadeOut;
    private bool isEnd;
    private bool isChange;
    private bool isUp;
    private bool isDown;

    private bool isHaveTimeLine;

    private int howManyTimeLines;


    private bool isSkipped = false; //스킵이 되었는가?


    PlayerAnimation.SeeWhere seeWhere;

    public void OpenChat(int idx, EndListner deli) // 대화창을 띄워주는 함수
    {
        canNextChat = false;
        GameManager.canInput = false;

        endListner = deli;

        string toPath = Application.dataPath + "/Resources/textSet"+idx.ToString()+".txt";
        streamReader = new StreamReader(toPath);
        chatUI.gameObject.SetActive(true); // 함수가 실행되면 채팅창 UI를 실행시키고
        // 현재 채팅창이 켜져있음을 확인시키기 위해 true로 수정
        playerMovement.canMove = false; // 대화중에는 움직일수없음

        chatText.text = "";
        string startcolor = streamReader.ReadLine();
        changeColor(startcolor);

        chatUI.gameObject.SetActive(true); // 함수가 실행되면 채팅창 UI를 실행시키고

        isStart = false;
        isChat = true;
        isSkipped = false;
        NextChat();

        chatUI.transform.position = ChatUiMinusPosition;
    }

    public void NextChat() // 대화를 다음으로 넘겨주는 함수
    {
        canNextChat = false;
        isSkipped = false;
        string nextStr = streamReader.ReadLine();
        if (nextStr == null) {
            
            startTime = Time.time;
            usingTime = 1f;
            isEnd =true;
        }
        else {

            if (nextStr == "Animation")
            {
                int animateNumber = int.Parse(streamReader.ReadLine());
                GameManager.gameManager.thisSceneEventManager.PlayAnimations(animateNumber);
            }
            else if (nextStr == "ImageOn")
            {
                nextStr = streamReader.ReadLine();
                int idx = int.Parse(nextStr);

                SceneImages[idx].gameObject.SetActive(true);
                SceneImages[idx].color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 0f / 255f);

                startTime = Time.time;
                index = idx;
                FadeValue = 0f;
                usingTime = 3f;
                isFadeIn = true;
            }
            else if (nextStr == "ImageOff")
            {
                nextStr = streamReader.ReadLine();
                int idx = int.Parse(nextStr);

                SceneImages[idx].color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

                startTime = Time.time;
                index = idx;
                FadeValue = 255f;
                usingTime = 3f;
                isFadeOut = true;
            }
            else if (nextStr == "Camera")
            {
                nextStr = streamReader.ReadLine();
                if (nextStr == "Return")
                {
                    float time = float.Parse(streamReader.ReadLine());
                    GameManager.gameManager.thisSceneEventManager.ReturnToPlayerCamera();
                    StartCoroutine(changeCamera(time));
                }
                else {
                    int idx = int.Parse(nextStr);
                    float time = float.Parse(streamReader.ReadLine());
                    GameManager.gameManager.thisSceneEventManager.ChangeVitrualCamera(idx);
                    StartCoroutine(changeCamera(time));
                }
            }
            else if (nextStr == "ChangePerson")
            {
                chatText.text = "";
                nextStr = streamReader.ReadLine();
                startTime = Time.time;
                usingTime = 1f;
                ToChangeColor = nextStr;
                isColorChanged = false;
                isChange = true;
                //SoundManager.soundManager.PlayTextDownSound();
            }
            else if (nextStr == "TextDown")
            {
                chatText.text = "";
                startTime = Time.time;
                usingTime = 1f;
                isDown = true;
                //SoundManager.soundManager.PlayTextDownSound();
            }
            else if (nextStr == "TextUp")
            {
                Debug.Log("텍스트 올라감");
                chatText.text = "";
                nextStr = streamReader.ReadLine();
                changeColor(nextStr);
                startTime = Time.time;
                usingTime = 1f;
                isUp = true;
                //SoundManager.soundManager.PlayTextUpSound();

            }
            else if(nextStr == "Sound")
            {
                nextStr = streamReader.ReadLine();
                if (nextStr == "Background")
                {
                    int idx = int.Parse(streamReader.ReadLine());
                    bool isLoop = bool.Parse(streamReader.ReadLine());
                    BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(idx, isLoop);
                    NextChat();
                }
                else if (nextStr == "Effect")
                {
                    nextStr = streamReader.ReadLine();
                    if (nextStr == "NoDelay")
                    {
                        int idx = int.Parse(streamReader.ReadLine());
                        SoundManager.soundManager.PlayEffectClip(idx);
                        NextChat();
                    }
                    else if (nextStr == "Delay")
                    {
                        int idx = int.Parse(streamReader.ReadLine());
                        float time = float.Parse(streamReader.ReadLine());
                        StartCoroutine(SoundManager.soundManager.PlayEffectClipAndDelay(idx, time));
                    }
                }
                else if (nextStr == "StopBackground") {
                    BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
                    NextChat();
                }
                //사운드 추가
            }
            else {
                StartCoroutine(Talk(nextStr));
            }
        }
    }

    public void EndChat() // 대화를 끝내는 함수
    {
        chatUI.gameObject.SetActive(false);

        playerMovement.canMove = true; //대화가 끝났으니 이제 움직일수있음
        GameManager.canInput = true;

        isEnd = false;
        isChat = false;

        if (endListner != null)
            endListner();
        endListner = null;

        streamReader.Close();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        isStart = false; 
        isFadeIn = false;
        isFadeOut = false;
        isEnd = false;
        isChange = false;
        isUp = false;
        isDown = false;

        ChatUiPosition = chatUI.transform.position;
        ChatUiMinusPosition = ChatUiPosition + new Vector2(0, -350);

        chatUI.SetActive(false);
    }






    void Update()
    {
        if(isChat) // 일단 실행되려면 채팅창이 켜져있어야됨
        {
            if((Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))&&isChat)
            {
                if (canNextChat)
                {
                    SoundManager.soundManager.PlayNextTextSound();
                    NextChat();                     // 다음텍스트로 넘어가는 함수 실행
                }
                else if(!isSkipped) {
                    isSkipped = true;
                }
            }

        }

        if (isFadeIn) { FadeIn(); }
        else if (isFadeOut) { FadeOut(); }
        else if (isEnd) { ChatUIDown(); }
        else if (isChange) { ChatUIDownAndUp(); }
        else if (isUp) { ChatUIOnlyUp(); }
        else if (isDown) { ChatUIOnlyDown(); }
    }












    IEnumerator Talk(string str) {
        chatText.text = "";

        for (int i = 0; i < str.Length; i++)
        {
            if (isSkipped) { chatText.text = str; break; }
            chatText.text += str[i];
            SoundManager.soundManager.PlayTextSound();
            yield return new WaitForSeconds(0.05f);
        }

        canNextChat = true;

    }


    private void changeColor(string str) {

        if (str == "Player") {
            chatBackGround.color = playerColor;
        }else if (str == "Nurse")
        {
            chatBackGround.color = nurseColor;
        }
        else if (str == "Doctor")
        {
            chatBackGround.color = doctorColor;
        }
        else if (str == "Npc")
        {
            chatBackGround.color = npcColor;
        }
        else if (str == "Clerk")
        {
            chatBackGround.color = clerkColor;
        }
        else if (str == "Heroin")
        {
            chatBackGround.color = heroinColor;
        }
        else if (str == "Alex")
        {
            chatBackGround.color = alexColor;
        }
    }

    private void changeToSee(string str)
    {

        if (str == "Front")
        {
            seeWhere = PlayerAnimation.SeeWhere.FRONT;
        }
        else if (str == "Back")
        {
            seeWhere = PlayerAnimation.SeeWhere.BACK;
        }
        else if (str == "Left")
        {
            seeWhere = PlayerAnimation.SeeWhere.LEFT;
        }
        else if (str == "Right")
        {
            seeWhere = PlayerAnimation.SeeWhere.RIGHT;
        }
    }










    private void FadeIn() {
        if (Time.time<=startTime+usingTime) {
            FadeValue += Time.deltaTime / 3f * 255f;
            SceneImages[index].color= new Color(255f / 255f, 255f / 255f, 255f / 255f, FadeValue / 255f);
        }
        else {
            isFadeIn = false;
            NextChat();
        }
    }

    private void FadeOut()
    {
        if (Time.time <= startTime + usingTime)
        {
            FadeValue -= Time.deltaTime / 3f * 255f;
            SceneImages[index].color = new Color(255f / 255f, 255f / 255f, 255f / 255f, FadeValue / 255f);
        }
        else
        {
            SceneImages[index].gameObject.SetActive(false);
            isFadeOut = false;
            NextChat();
        }
    }


    private void ChatUIDown()
    {
        if (Time.time <= startTime + usingTime && ChatUiMinusPosition.y <= chatUI.transform.position.y)
        {
            chatUI.transform.Translate(0, -350 * Time.deltaTime, 0);
        }
        else
        {
            chatUI.transform.position = ChatUiMinusPosition;
            EndChat();
        }
    }

    private void ChatUIDownAndUp()
    {
        if (Time.time <= startTime + usingTime/ 2 && ChatUiMinusPosition.y <= chatUI.transform.position.y)
        {
            chatUI.transform.Translate(0, -700 * Time.deltaTime, 0);
        }
        else if (Time.time <= startTime + usingTime && ChatUiPosition.y >= chatUI.transform.position.y)
        {
            if (!isColorChanged) {
                changeColor(ToChangeColor);
                isColorChanged = true;
                //SoundManager.soundManager.PlayTextUpSound();
            }
            chatUI.transform.Translate(0, 700 * Time.deltaTime, 0);
        }
        else
        {
            chatUI.transform.position = ChatUiPosition;
            isChange = false;
            NextChat();
        }
    }

    private void ChatUIOnlyUp()
    {
        if (Time.time <= startTime + usingTime && ChatUiPosition.y >= chatUI.transform.position.y)
        {
            chatUI.transform.Translate(0, 350 * Time.deltaTime, 0);
        }
        else
        {
            chatUI.transform.position = ChatUiPosition;
            isUp = false;
            NextChat();
        }
    }

    private void ChatUIOnlyDown()
    {
        if (Time.time <= startTime + usingTime && ChatUiMinusPosition.y <= chatUI.transform.position.y)
        {
            chatUI.transform.Translate(0, -350 * Time.deltaTime, 0);
        }
        else
        {
            chatUI.transform.position = ChatUiMinusPosition;
            isDown = false;
            NextChat();
        }
    }


    public IEnumerator changeCamera(float time) {
        yield return new WaitForSeconds(time);
        NextChat();
    }

}
