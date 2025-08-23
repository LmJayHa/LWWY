using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPuzzleOpener : InterectableObj
{
    public ForthPuzzleGameEventManager forthPuzzleGameEventManager;
    public int puzzleNum;

    public override void interection()
    {
        base.interection();
        forthPuzzleGameEventManager.OpenPuzzleUI(puzzleNum);


    }
}
