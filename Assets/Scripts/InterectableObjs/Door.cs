using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InterectableObj
{
    public bool isLock;

    public int doorSoundIdx;

    public float toGoX;
    public float toGoY;

    public int doorLockTextidx;


    public bool isChangeBackgroundSound;

    public int changeBackgroundSoundIdx;


    [SerializeField]
    public GameManager.Space toGo;

    [SerializeField]
    public PlayerAnimation.SeeWhere toSee;

    public override void interection() {
        if (isLock)
        {
            StartCoroutine(CantOpenDoor());
        }
        else {
            StartCoroutine(DoorToGo());
        }
    }

    private IEnumerator DoorToGo() {
        GameManager.canInput = false;

        FadeInOutBlack.fadeInOutBlack.SetFadeIn(1f);
        SoundManager.soundManager.PlayDoorSound(doorSoundIdx);

        if (isChangeBackgroundSound) { BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound(); }

        yield return new WaitForSeconds(1.01f);

        PlayerData.playerData.transform.position = new Vector2(toGoX, toGoY);


        GameManager.gameManager.lastSpace = toGo;
        GameManager.gameManager.changeFootStepSound();


        PlayerAnimation.playerAnimation.seeAnimationWhere(toSee);

        yield return new WaitForSeconds(1.01f);

        if (isChangeBackgroundSound) {
            BackgroundSoundManager.backgroundSoundManager.StopBackgroundSound();
            yield return new WaitForSeconds(1f);
            BackgroundSoundManager.backgroundSoundManager.PlayBackgroundSound(changeBackgroundSoundIdx, true); 
        }
        FadeInOutBlack.fadeInOutBlack.SetFadeOut(1f);
        yield return new WaitForSeconds(1.01f);

        SoundManager.soundManager.PlayDoorSound(doorSoundIdx+1);
        GameManager.canInput = true;
    }

    private IEnumerator CantOpenDoor()
    {
        GameManager.canInput = false;

        SoundManager.soundManager.PlayDoorLockSound();

        yield return new WaitForSeconds(1.01f);
        ChatManager.chatManager.OpenChat(doorLockTextidx, null);
    }
}
