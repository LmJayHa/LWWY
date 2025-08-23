using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : InterectableObj
{
    public int starIdx;

    public ThirdPuzzleEventManager thirdPuzzleEventManager;

    public override void interection()
    {
        thirdPuzzleEventManager.pickUp(starIdx);
    }

}
