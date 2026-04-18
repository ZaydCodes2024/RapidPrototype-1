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
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    private Action OnCloseButtonAction;
    private void Awake()
    {
        Instance = this;

        sfxSlider.onValueChanged.AddListener( (_) =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicSlider.onValueChanged.AddListener( (_) =>
        {
            MusicManager.Instance.ChangeVolume();
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
        UpdateVisual();
        Hide();
    }

    private void GameInput_OnGameUnpauseAction(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        // float sfxVolume = SoundManager.Instance.GetVolume();
        // float musicVolume = MusicManager.Instance.GetVolume();

        // soundEffectsVolumeText.text = Mathf.Round(sfxVolume * 10f).ToString();
        // musicVolumeText.text = Mathf.Round(musicVolume * 10f).ToString();

        // sfxSlider.value = sfxVolume;
        // musicSlider.value = musicVolume;
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