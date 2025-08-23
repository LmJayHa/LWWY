using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGameInterectable : InterectableObj
{
    public flagGameManager flagGameManager;

    public GameObject AskUI;

    public override void interection()
    {
        AskUI.SetActive(true);
    }

    public void OnClickButtonYes()
    {
        AskUI.SetActive(false);
        ChatManager.chatManager.OpenChat(216, EndGame);
    }

    public void OnClickButtonNo()
    {
        AskUI.SetActive(false);
    }

    public void EndGame()
    {
        flagGameManager.StartGame();
    }

}
