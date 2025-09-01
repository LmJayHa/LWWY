using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager
    {
        get
        {
            if (soundM_instance == null)
            {
                soundM_instance = FindObjectOfType<SoundManager>();
            }

            return soundM_instance;
        }
    }//SoundManager를 싱글턴으로 설정

    public AudioMixer mainMixer;      // 인스펙터에서 MainMixer를 연결할 변수
    public AudioSource sfxSource;     // SFX 전용 오디오 소스

    private static SoundManager soundM_instance; //싱글턴에 이용된 인스턴스

    public PlayerInput playerInput;

    public AudioClip[] EffectClips;

    public AudioClip TextSound;
    public AudioClip NextTextSound;

    public AudioClip[] FootStepSounds;

    public AudioClip OnClickSound;

    public AudioClip[] DoorSounds;

    public AudioClip doorLockSound;

    private bool isStepping;

    public int FootStepIdx;

    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        sfxSource.loop = false;
        isStepping = false;
        FootStepIdx = 0;


        InitializeVolumeSetting();
    }

    void InitializeVolumeSetting()
    {
        float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        // 슬라이더와 믹서에 적용
        bgmSlider.value = savedBGM;
        sfxSlider.value = savedSFX;

        SetBGMVolume(savedBGM);
        SetSFXVolume(savedSFX);

        // 슬라이더 이벤트 연결
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SaveBGMVolume()
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value); // 값 저장
    }

    public void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value); // 값 저장
    }

    public void SetBGMVolume(float volume)
    {
        mainMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);    
    }

    public void PlayEffectClip(int idx) {
        sfxSource.PlayOneShot(EffectClips[idx]);
    }

    public IEnumerator PlayEffectClipAndDelay(int idx, float time) {
        sfxSource.PlayOneShot(EffectClips[idx]);
        yield return new WaitForSeconds(time);
        ChatManager.chatManager.NextChat();
    }

    public void PlayTextSound() {
        sfxSource.PlayOneShot(TextSound);
    }

    public void PlayClickSound()
    {
        sfxSource.PlayOneShot(OnClickSound);
        //audioSource.PlayOneShot(OnClickSound);
    }

    public void PlayDoorSound(int idx)
    {
        sfxSource.PlayOneShot(DoorSounds[idx]);
       // audioSource.PlayOneShot(DoorSounds[idx]);
    }

    public void PlayDoorLockSound()
    {
        sfxSource.PlayOneShot(doorLockSound);
    }

    public void PlayTextDownSound()
    {
        //audioSource.PlayOneShot(TextSound);
    }

    public void PlayTextUpSound()
    {
        //audioSource.PlayOneShot(TextSound);
    }

    public void PlayNextTextSound()
    {
        sfxSource.PlayOneShot(NextTextSound);
    }

    public void PlayFootStepSounds() {

        if (!isStepping) {
            StartCoroutine(FootStepSound(FootStepIdx));
        }
    }

    public IEnumerator FootStepSound(int idx) {
        isStepping = true;
        if (playerInput.run)
        {
            sfxSource.PlayOneShot(FootStepSounds[idx]); // sfxSource로 변경
          //  audioSource.PlayOneShot(FootStepSounds[idx]);
            yield return new WaitForSeconds(0.3f);
        }
        else {
            sfxSource.PlayOneShot(FootStepSounds[idx]); // sfxSource로 변경
           // audioSource.PlayOneShot(FootStepSounds[idx]);
            yield return new WaitForSeconds(0.45f);
        }
        isStepping = false;
    }

    //==================
    public void PlayFootStepRunSounds()
    {
        if (!isStepping)
        {
            StartCoroutine(FootStepRunSound(FootStepIdx));
        }
    }

    public IEnumerator FootStepRunSound(int idx)
    {
        isStepping = true;
        sfxSource.PlayOneShot(FootStepSounds[idx]);
            yield return new WaitForSeconds(0.3f);
        isStepping = false;
    }
}
