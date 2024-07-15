using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Hellmade.Sound;

public class PopupSetting : Popups
{
    public static PopupSetting Instance;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private List<Button> button;
    [SerializeField] private CustomButton yesButton;
    [SerializeField] private CustomButton noButton;
   
    private float _musicVolume;
    private float _soundVolume;

    public float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            EazySoundManager.GlobalMusicVolume = value;
            EazySoundManager.GlobalSoundsVolume = value;
        }
    }

    public float SoundVolume
    {
        get { return _soundVolume; }
        set
        {
            _soundVolume = value;
            EazySoundManager.GlobalSoundsVolume = value;
            EazySoundManager.GlobalSoundsVolume = value;
        }
    }

    private Action<bool> _onResult;
    void InitUI()
    {

        volumeSlider.value = EazySoundManager.GlobalMusicVolume;
        volumeSlider.onValueChanged.AddListener(OnChangeSliderMusic);
        sfxSlider.value = EazySoundManager.GlobalSoundsVolume;
        sfxSlider.onValueChanged.AddListener(OnChangeSfxVoloume);
    }

    public void OnYesButtonClicked()
    {
        _onResult?.Invoke(true);
        Hide();
    }

    public void OnNoButtonClicked()
    {
        _onResult?.Invoke(false);
        Hide();
    }
    public void OnChangeSliderMusic(float value)
    {
        EazySoundManager.GlobalMusicVolume = value;
        AudioManager.Instance.playerVolumeSetting.gameVolume = EazySoundManager.GlobalMusicVolume;
        // Debug.Log(EazySoundManager.GlobalMusicVolume);
    }
    public void OnChangeSfxVoloume(float value)
    {
        EazySoundManager.GlobalSoundsVolume = value;
        AudioManager.Instance.playerVolumeSetting.sfxVolume = EazySoundManager.GlobalSoundsVolume;
    }
    public void OnUIColorChange(int id)
    {
        switch (id)
        {
            case 1:
                UserUIManager.Instance.ChangeUIColor(GameStatic.CRITICAL_TIER_5_COLOR);
                Frame.color = UserUIManager.Instance.currentUIColor;
                break;
            case 2:
                UserUIManager.Instance.ChangeUIColor(GameStatic.USER_UI_COLOR_BLUE);
                Frame.color = UserUIManager.Instance.currentUIColor;
                break;
            case 3:
                UserUIManager.Instance.ChangeUIColor(GameStatic.USER_UI_COLOR_CYAN);
                Frame.color = UserUIManager.Instance.currentUIColor;
                break;
            case 4:
                UserUIManager.Instance.ChangeUIColor(GameStatic.USER_UI_COLOR_PURPLE);
                Frame.color = UserUIManager.Instance.currentUIColor;
                break;
        }
    }


    #region BASE POPUP 
    static void CheckInstance(Action completed)//
    {
        if (Instance == null)
        {

            var loadAsset = Resources.LoadAsync<PopupSetting>("Prefab/UI/PopupPrefabs/PopupSetting" +
                "");
            loadAsset.completed += (result) =>
            {
                var asset = loadAsset.asset as PopupSetting;
                if (asset != null)
                {
                    Instance = Instantiate(asset,
                        CanvasPopup4.transform,
                        false);

                    if (completed != null)
                    {
                        completed();
                    }
                }
            };

        }
        else        
        {
            if (completed != null)
            {
                completed();
            }
        }
    }

    public static void Show()//
    {

        CheckInstance(() =>
        {
            Instance.Appear();
            Instance.InitUI();
        });

    }

    public static void Hide()
    {
        if (GameStatic.IS_ANIMATING ) return;

        Instance.Disappear();
    }
    public override void Appear()
    {
        IsLoadBoxCollider = false;
        base.Appear();
        //Background.gameObject.SetActive(true);
        Panel.gameObject.SetActive(true);
    }
    public void Disappear()
    {
        //Background.gameObject.SetActive(false);
        base.Disappear(()=>{
            Panel.gameObject.SetActive(false);
        });
    }

    public override void Disable()
    {
        base.Disable();
    }

    public override void NextStep(object value = null)
    {
    }
    #endregion

}
