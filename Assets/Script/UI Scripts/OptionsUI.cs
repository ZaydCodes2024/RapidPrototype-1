using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance {get; private set;}
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] PlayerLook playerLook;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI mouseSensitivityText;
    private Action OnCloseButtonAction;
    private void Awake()
    {
        Instance = this;

        sfxSlider.onValueChanged.AddListener( (value) =>
        {
            SoundManager.Instance.ChangeVolume(value);
            UpdateVisual();
        });

        musicSlider.onValueChanged.AddListener( (value) =>
        {
            MusicManager.Instance.ChangeVolume(value);
            UpdateVisual();
        } );

        mouseSensitivitySlider.onValueChanged.AddListener( (value) =>
        {
            playerLook.SetMouseSensitivityValue(value);
            UpdateVisual();
        } );

        closeButton.onClick.AddListener( () =>
        {
            Hide();
            OnCloseButtonAction();
        } );
    }

    private void Start()
    {
        GameInput.Instance.OnGameUnpauseAction += GameInput_OnGameUnpauseAction;

        sfxSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();
        mouseSensitivitySlider.value = playerLook.GetMouseSensitivityValue();
        UpdateVisual();
        Hide();
    }

    private void GameInput_OnGameUnpauseAction(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsVolumeText.text = Mathf.Ceil(sfxSlider.value * 10f).ToString();
        musicVolumeText.text = Mathf.Ceil(musicSlider.value * 10f).ToString();
        mouseSensitivityText.text = Mathf.Ceil(mouseSensitivitySlider.value * 10f).ToString();
    }
    public void Show(Action closeButtonAction)
    {
        this.OnCloseButtonAction = closeButtonAction;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}