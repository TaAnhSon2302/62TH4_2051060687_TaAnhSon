using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("--------------Sound Tracks--------------")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip logInBackground;
    [SerializeField] private AudioClip normalBattleHematos;
    [SerializeField] private float turnOnOffDuration = 0.5f;
    [Header("--------------Sound Effects--------------")]
    [SerializeField] private AudioClip gunFire;

    [Space(10)]
    [Header("Adjust")]
    public int bgMusicMain;
    public int bgNormalBattleHematos;
    [SerializeField] public GameSetting playerVolumeSetting;
    private bool isStoppingAudio = false;
    private void Start()
    {
        StartLogInBackground();
        //Debug.Log(bgMusicMain);   
    }

    public void StartLogInBackground(){
        // EazySoundManager.StopAllMusic();
        EazySoundManager.PlayMusic(logInBackground,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
        // StartCoroutine(IEStopMusic());
        // StartCoroutine(IEStartMusic(logInBackground));
    }
    public void StartMainMenuBackGround(){
        // EazySoundManager.PlayMusic(backgroundMusic,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
        StartCoroutine(IEStartMusic(backgroundMusic));
    }
    public void StartNormalBattleHematos(){
        StartCoroutine(IEStartMusic(normalBattleHematos));
        // EazySoundManager.PlayMusic(normalBattleHematos,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
    }
    public void StopCurrentMusic(){
        StartCoroutine(IEStopMusic());
    }
    public IEnumerator IEStopMusic(){
        float duration = turnOnOffDuration;
        isStoppingAudio = true;
        while(duration>0){
            yield return new WaitForSecondsRealtime(0.01f);
            EazySoundManager.GlobalMusicVolume -= playerVolumeSetting.gameVolume/(turnOnOffDuration/0.01f);
            duration-= 0.01f;
        }
        EazySoundManager.GlobalMusicVolume = 0;
        EazySoundManager.StopAllMusic();
        isStoppingAudio = false;
    }
    public IEnumerator IEStartMusic(AudioClip audioClip){
        float duration = turnOnOffDuration;
        while(isStoppingAudio){
            yield return null;
        }
        // yield return new WaitForSeconds(turnOnOffDuration*2);
        
        EazySoundManager.StopAllMusic();
        EazySoundManager.PlayMusic(audioClip,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
        EazySoundManager.GlobalMusicVolume = 0;
        while(duration>0){
            yield return new WaitForSeconds(0.01f);
            EazySoundManager.GlobalMusicVolume += playerVolumeSetting.gameVolume/(turnOnOffDuration/0.01f);
            duration -= 0.01f;
        }
        EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume;
    }
    public void PlayGunFire(){
        EazySoundManager.PlaySound(gunFire,EazySoundManager.GlobalSoundsVolume = playerVolumeSetting.sfxVolume);
    }
}
