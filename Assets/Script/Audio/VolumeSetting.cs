using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hellmade.Sound;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private float _musicVolume;
    private float _soundVolume;

    public float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            EazySoundManager.GlobalMusicVolume = value;
        }
    }

    public float SoundVolume
    {
        get { return _soundVolume; }
        set
        {
            _soundVolume = value;
            EazySoundManager.GlobalSoundsVolume = value;
            EazySoundManager.GlobalUISoundsVolume = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
