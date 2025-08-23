using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectableObjDance : InterectableObj
{

    public DanceManager duduGManager;


    public GameObject AskUI;

    public override void interection()
    {
        AskUI.SetActive(true);
    }

    public void OnClickButtonYes() {
        AskUI.SetActive(false);
        ChatManager.chatManager.OpenChat(217, EndGame);
    }

    public void OnClickButtonNo()
    {
        AskUI.SetActive(false);
    }

    public void EndGame() {
        duduGManager.StartGame();
    }
    
}