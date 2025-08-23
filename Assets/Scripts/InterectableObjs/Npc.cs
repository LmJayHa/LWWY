using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : InterectableObj
{
    public bool canTalk; //현재 대화가 가능한 상태인지

    public bool isFixedTalk; //대화한다고 안사라지는지

    public int talkNum; //대화할 파일의 번호

    public bool isEvent;
    public int eventIndex;

    public override void interection()
    {
        base.interection();

        if (isEvent) { GameManager.gameManager.thisSceneEventManager.StartEvent_toNPC(eventIndex); }

        if (canTalk)
        {
            Debug.Log("채팅 시작");
            ChatManager.chatManager.OpenChat(talkNum, setTalkState);
        }
    }
    public void setTalkState() {
        if(!isFixedTalk)
        canTalk = false;

        if (isEvent) {
            GameManager.gameManager.thisSceneEventManager.EndEvent_toNPC(eventIndex);
        }
    }
}
