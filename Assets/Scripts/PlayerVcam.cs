using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVcam : MonoBehaviour
{
    public static PlayerVcam playerVcam
    {
        get
        {
            if (pVcam_instance == null)
            {
                pVcam_instance = FindObjectOfType<PlayerVcam>();
            }

            return pVcam_instance;
        }
    }//SoundManager를 싱글턴으로 설정

    private static PlayerVcam pVcam_instance; //싱글턴에 이용된 인스턴스

    public GameObject VCam;

    private void Awake()
    {
        VcamOn();
    }

    public void VcamOff() {
        VCam.SetActive(false);
    }

    public void VcamOn()
    {
        VCam.SetActive(true);
    }
}
