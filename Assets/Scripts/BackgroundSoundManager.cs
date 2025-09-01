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

    public AudioSource bgmSource;     // BGM 전용 오디오 소스

    public AudioClip[] AudioClips;

    private void Awake()
    {
        nowBackgroundSoundNum = -1;
        DontDestroyOnLoad(gameObject);
        bgmSource.loop = false;
    }


    public void PlayBackgroundSound(int idx, bool isLoop)
    {
        nowBackgroundSoundNum = idx;
        Debug.Log(nowBackgroundSoundNum);
        Debug.Log(AudioClips.Length);
        bgmSource.Stop();
        bgmSource.loop = isLoop;
        Debug.Log(idx+"번 실행"+ AudioClips[idx]);
        bgmSource.clip = AudioClips[idx];
        bgmSource.Play();
        bgmSource.volume = 0;
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
            if (bgmSource.volume + Time.deltaTime*3 >= 0.5f)
            {
                bgmSource.volume = 1f;
                break;
            }
            else {
                bgmSource.volume += Time.deltaTime * 5;
            }
            yield return new WaitForSeconds(0.05f);
            
        }
        bgmSource.volume = 0.5f;
    }

    public IEnumerator FadeOutSoundVolume()
    {
        float time = Time.time;
        while (Time.time <= time + 2f)
        {
            if (bgmSource.volume - Time.deltaTime * 3 <= 0f)
            {
                bgmSource.volume = 0f;
                break;
            }
            else
            {
                bgmSource.volume -= Time.deltaTime * 5;
            }
            yield return new WaitForSeconds(0.05f);

        }
        bgmSource.volume = 0f;
        bgmSource.Stop();
    }

}
