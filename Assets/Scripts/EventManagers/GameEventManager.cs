using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class GameEventManager : MonoBehaviour
{

    public NurseAnimator nurseAnimator;
    public DoctorAnimator doctorAnimator;
    

    public GameObject[] EventTriggers;

    public GameObject[] virtualCameras;

    public int nowCamera=-1; //-1은 플레이어 따라다니는 카메라

    public bool isLoad = false;
    public int loadNum = -1;

    public bool isFirst=false;

    private void OnEnable()
    {
        isFirst = GameManager.gameManager.isFirst;

        nowCamera = -1;

        for (int i = 0; i < EventTriggers.Length; i++)
        {
            Debug.Log("start" + EventTriggers[i].active.ToString());
            EventTriggers[i].SetActive(false);
            Debug.Log(EventTriggers[i].active.ToString());
        }

        for (int i = 0; i < virtualCameras.Length; i++)
        {
            Debug.Log("Vstart" + virtualCameras[i].active.ToString());
            virtualCameras[i].SetActive(false);
            Debug.Log(virtualCameras[i].active.ToString());
        }

        isLoad = true;
    }

    public void CallEndSignalToChat()
    {
        ChatManager.chatManager.NextChat();
    }

    public virtual void PlayAnimations(int idx)
    {

    }

    public virtual void StartEvent_toNPC(int idx)
    {

    }

    public virtual void EndEvent_toNPC(int idx)
    {

    }

    public virtual void StartEventTrigger(int idx)
    {
        EventTriggers[idx].SetActive(false);
    }

    public virtual void SaveData(int where) {

    }

    public virtual void LoadData(int where)
    {

    }

    public virtual void ChangeLoadingThings()
    {
    }

    public virtual void ChangeVitrualCamera(int idx) {
        virtualCameras[idx].SetActive(true);

        if (nowCamera == -1) {
        PlayerVcam.playerVcam.VcamOff();
        } else {
            virtualCameras[nowCamera].SetActive(false);
        }

        nowCamera = idx;
    }

    public virtual void ReturnToPlayerCamera() {
        PlayerVcam.playerVcam.VcamOn();
        if(nowCamera!=-1)
            virtualCameras[nowCamera].SetActive(false);
        nowCamera = -1;
    }


    public string SceneName; //씬의 이름
    

}
