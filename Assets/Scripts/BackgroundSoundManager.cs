using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{


    public static BackgroundSoundManager backgroundSoundManager
    {
        get
        {
            if (bs_instance == null)
            {
                bs_instance = FindObjectOfType<BackgroundSoundManager>();
            }
            return bs_instance;
        }
    }

    private static BackgroundSoundManager bs_instance;

    public int nowBackgroundSoundNum;

    private AudioSource audioSource;

    public AudioClip[] AudioClips;

    private void Awake()
    {
        nowBackgroundSoundNum = -1;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    public void PlayBackgroundSound(int idx, bool isLoop)
    {
        nowBackgroundSoundNum = idx;
        Debug.Log(nowBackgroundSoundNum);
        Debug.Log(AudioClips.Length);
        audioSource.Stop();
        audioSource.loop = isLoop;
        Debug.Log(idx+"번 실행"+ AudioClips[idx]);
        audioSource.clip = AudioClips[idx];
        audioSource.Play();
        audioSource.volume = 0;
        StartCoroutine(FadeInSoundVolume());
        Debug.Log(AudioClips.Length);
    }

    public void StopBackgroundSound() {
        nowBackgroundSoundNum = -1;
        StartCoroutine(FadeOutSoundVolume());
    }

    public IEnumerator FadeInSoundVolume() {
        float time = Time.time;
        while (Time.time<=time+2f)
        {
            if (audioSource.volume + Time.deltaTime*3 >= 0.5f)
            {
                audioSource.volume = 1f;
                break;
            }
            else {
                audioSource.volume += Time.deltaTime * 5;
            }
            yield return new WaitForSeconds(0.05f);
            
        }
        audioSource.volume = 0.5f;
    }

    public IEnumerator FadeOutSoundVolume()
    {
        float time = Time.time;
        while (Time.time <= time + 2f)
        {
            if (audioSource.volume - Time.deltaTime * 3 <= 0f)
            {
                audioSource.volume = 0f;
                break;
            }
            else
            {
                audioSource.volume -= Time.deltaTime * 5;
            }
            yield return new WaitForSeconds(0.05f);

        }
        audioSource.volume = 0f;
        audioSource.Stop();
    }

}
