using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoChatNpc : InterectableObj
{
    public int eventIndex;

    public override void interection()
    {
        base.interection();

        GameManager.gameManager.thisSceneEventManager.StartEvent_toNPC(eventIndex);

    }
}
