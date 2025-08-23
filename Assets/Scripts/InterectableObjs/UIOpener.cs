using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpener : InterectableObj
{
    private bool isOpen = false;

    public GameObject UIObj;

    public bool isClose = false;

    public override void interection()
    {
        if (isClose) { CloseUI(); isClose = false; return;  }

        base.interection();
        isOpen = true;
        UIObj.SetActive(true);
        GameManager.canInput = false;
    }

    public void CloseUI()
    {
        isOpen = false;
        UIObj.SetActive(false);
        GameManager.canInput = true;
    }

    private void Update()
    {
        if (isOpen && (Input.GetKeyDown(KeyCode.Escape))) {
            CloseUI();
        }
        else if (isOpen && (Input.GetKeyDown(KeyCode.E)))
        {
            Debug.Log("오프너에서 E눌림");
            GameManager.canInput = true;
            isClose = true;
        }
    }

}
