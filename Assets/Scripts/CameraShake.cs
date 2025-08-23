using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake cameraShake
    {
        get
        {
            if (knowed_instance == null)
            {
                knowed_instance = FindObjectOfType<CameraShake>();
            }

            return knowed_instance;
        }
    }

    private static CameraShake knowed_instance; //싱글턴에 이용된 인스턴스

    public GameObject vcamOfPlayer;
    GameObject playerVcam;

    float ShakeAmount;
    public float ShakeTime;
    Vector3 startPosition;

    public bool isInShake=false;

    public void Shake(float time, float shakeAmount) {
        vcamOfPlayer.SetActive(false);
        playerVcam = GameObject.Find("Main Camera");
        ShakeTime = time;
        startPosition = playerVcam.transform.position;
        ShakeAmount = shakeAmount;
        isInShake = true;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


        // Update is called once per frame
        void Update()
    {
        if (isInShake)
        {
            if (ShakeTime > 0)
            {
                playerVcam.transform.position = Random.insideUnitSphere * ShakeAmount + startPosition;
                Debug.Log(playerVcam.transform.position);
                ShakeTime -= Time.deltaTime;

            }
            else
            {
                vcamOfPlayer.SetActive(true);
                ShakeTime = 0;
                playerVcam.transform.position = startPosition;
                isInShake = false;
            }
        }
    }
}
