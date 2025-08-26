using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    private static SoundManager soundM_instance; //싱글턴에 이용된 인스턴스

    public PlayerInput playerInput;

    private AudioSource audioSource;

    public AudioClip[] EffectClips;

    public AudioClip TextSound;
    public AudioClip NextTextSound;

    public AudioClip[] FootStepSounds;

    public AudioClip OnClickSound;

    public AudioClip[] DoorSounds;

    public AudioClip doorLockSound;

    private bool isStepping;

    public int FootStepIdx;

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        isStepping = false;
        FootStepIdx = 0;
    }

    public void PlayEffectClip(int idx) {
        audioSource.PlayOneShot(EffectClips[idx]);
    }

    public IEnumerator PlayEffectClipAndDelay(int idx, float time) {
        audioSource.PlayOneShot(EffectClips[idx]);
        yield return new WaitForSeconds(time);
        ChatManager.chatManager.NextChat();
    }

    public void PlayTextSound() {
        audioSource.PlayOneShot(TextSound);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(OnClickSound);
    }

    public void PlayDoorSound(int idx)
    {
        audioSource.PlayOneShot(DoorSounds[idx]);
    }

    public void PlayDoorLockSound()
    {
        audioSource.PlayOneShot(doorLockSound);
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
        audioSource.PlayOneShot(NextTextSound);
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
            audioSource.PlayOneShot(FootStepSounds[idx]);
            yield return new WaitForSeconds(0.3f);
        }
        else {
            audioSource.PlayOneShot(FootStepSounds[idx]);
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
            audioSource.PlayOneShot(FootStepSounds[idx]);
            yield return new WaitForSeconds(0.3f);
        isStepping = false;
    }
}
