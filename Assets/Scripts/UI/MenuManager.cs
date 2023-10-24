using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine.PostFX;
public class MenuManager : MonoBehaviour
{
    private bool isPaused = false;
    [Header("Keybinds")]
    [SerializeField] public KeyCode Pause = KeyCode.Escape;
    [SerializeField] public KeyCode Inventory = KeyCode.I;

    [Header("UI")]
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject SettingsUI;
    [SerializeField] GameObject SoundUI;
    [SerializeField] GameObject GraphicsUI;

    [Header("UI Element")]
    [SerializeField] TMP_Dropdown graphicQuality;
    [SerializeField] TMP_Dropdown blurQuality;
    [SerializeField] Slider blurIntenseSlider;
    [SerializeField] TMP_Dropdown frameRateDropdown;

    [Header("Graphics")]
    [SerializeField] List<RenderPipelineAsset> renderPipelineAssets;
    [SerializeField] CinemachineVolumeSettings cinemachineVolumeSettings;

    [Header("Animatiors")]
    [SerializeField] Animator pausePanelAnimator;

    [Header("Scripts")]
    [SerializeField] GameAudios gameAudios;
    [SerializeField] StarterAssetsInputs starterAssetsInputs;
    [SerializeField] WeaponShoot weaponShoot;
    // Start is called before the first frame update
    void Start()
    {
        PauseUI.SetActive(false);
        SettingsUI.SetActive(false);
        HideThirdLevel();
        // pausePanelAnimator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Pause))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = !isPaused;
        foreach (var item in gameAudios.inGameAudioSources)
        {
            item.Pause();
        }
        starterAssetsInputs.cursorInputForLook = false;
        //starterAssetsInputs.cursorLocked = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        starterAssetsInputs.LookInput(Vector2.zero);
        weaponShoot.enabled = false;
        ShowMenuPause();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = !isPaused;
        foreach (var item in gameAudios.inGameAudioSources)
        {
            item.UnPause();
        }
        starterAssetsInputs.cursorInputForLook = true;
        //starterAssetsInputs.cursorLocked = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weaponShoot.enabled = true;
        HideMenuPause();
    }
    void ShowMenuPause()
    {
        PauseUI.SetActive(true);
        GameUI.SetActive(false);
        //pausePanelAnimator.enabled = true;
    }
    void HideMenuPause()
    {
        PauseUI.SetActive(false);
        GameUI.SetActive(true);
    }

    public void ShowSettings()
    {
        SettingsUI.SetActive(true);
    }
    public void HideSettings()
    {
        SettingsUI.SetActive(false);
    }
    public void ShowSound()
    {
        HideThirdLevel();
        SoundUI.SetActive(true);
    }
    public void HideSound()
    {
        SoundUI.SetActive(false);
    }
    public void ShowGraphics()
    {
        HideThirdLevel();
        GraphicsUI.SetActive(true);
    }
    void HideGraphics()
    {
        GraphicsUI.SetActive(false);
    }
    private void HideThirdLevel()
    {
        if (GraphicsUI.activeSelf)
            HideGraphics();
        if (SoundUI.activeSelf)
            HideSound();
    }

    public void qualityGraphics()
    {
        //GraphicsSettings.renderPipelineAsset = renderPipelineAssets[graphicQuality.value];
        QualitySettings.SetQualityLevel(graphicQuality.value, true);
    }
    public void SetPostProcess()
    {
        cinemachineVolumeSettings.enabled = !cinemachineVolumeSettings.enabled;
    }
    public void SetBloom()
    {
        cinemachineVolumeSettings.m_Profile.TryGet(out Bloom bloom);
        bloom.active = !bloom.active;
    }
    public void SetBlur()
    {
        cinemachineVolumeSettings.m_Profile.TryGet(out MotionBlur blur);
        blur.active = !blur.active;
    }
    public void SetBlurQuality()
    {
        cinemachineVolumeSettings.m_Profile.TryGet(out MotionBlur blur);
        if (blurQuality.value == 0)
        {
            blur.quality.value = MotionBlurQuality.Low;
        }
        else
        if (blurQuality.value == 1)
        {
            blur.quality.value = MotionBlurQuality.Medium;
        }
        else
        {
            blur.quality.value = MotionBlurQuality.High;
        }
    }
    public void SetBlurIntense()
    {
        cinemachineVolumeSettings.m_Profile.TryGet(out MotionBlur blur);
        blur.intensity.value = blurIntenseSlider.value;
    }
    public void SetChromatic()
    {
        cinemachineVolumeSettings.m_Profile.TryGet(out ChromaticAberration chromaticAberration);
        chromaticAberration.active = !chromaticAberration.active;
    }
    public void SetColorCorrector()
    {
        cinemachineVolumeSettings.m_Profile.TryGet(out ShadowsMidtonesHighlights shadowsMidtonesHighlights);
        shadowsMidtonesHighlights.active = !shadowsMidtonesHighlights.active;
    }
    public void SetFrameRate()
    {
        switch (frameRateDropdown.value)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 2:
                Application.targetFrameRate = 75;
                break;
            case 3:
                Application.targetFrameRate = 90;
                break;
            case 4:
                Application.targetFrameRate = 120;
                break;
            case 5:
                Application.targetFrameRate = 144;
                break;
            case 6:
                Application.targetFrameRate = 240;
                break;
            default:
                Application.targetFrameRate = 60;
                break;
        }

    }
    public void Exit()
    {
        Application.Quit();
    }
}
