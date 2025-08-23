using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthPickableObj : InterectableObj
{
    public int Idx;

    public FifthPuzzleGameEventManager fifthPuzzleGameEventManager;

    public override void interection()
    {
        fifthPuzzleGameEventManager.Pick(Idx);
    }

}
