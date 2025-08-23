using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerButton : InterectableObj
{
    public int idx;

    public SixthPuzzleGameEventManager sixthPuzzleGameEventManager;

    public override void interection()
    {
        sixthPuzzleGameEventManager.OnClickButton(idx);
    }

}
