using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseStar : InterectableObj
{
    public int starIdx;

    public ThirdPuzzleEventManager thirdPuzzleEventManager;

    public override void interection()
    {
        thirdPuzzleEventManager.CaseInOut(starIdx);
    }

}
