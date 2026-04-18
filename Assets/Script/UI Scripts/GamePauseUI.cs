using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;
    private void Awake()
    {
        resumeButton.onClick.AddListener( () =>
        {
           GameInput.Instance.TogglePauseGame(); 
        });

        mainMenuButton.onClick.AddListener(() =>
        {
           Loader.Load(Loader.Scene.MainMenuScene); 
        });
        
        optionsButton.onClick.AddListener( () =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        } );
    }

    private void Start()
    {
        GameInput.Instance.OnGamePauseAction += GameInput_OnGamePauseAction;
        GameInput.Instance.OnGameUnpauseAction += GameInput_OnGameUnpauseAction;
        Hide();
    }

    private void GameInput_OnGameUnpauseAction(object sender, EventArgs e)
    {
        Player.Instance.LockCursorState();
        Hide();
    }

    private void GameInput_OnGamePauseAction(object sender, EventArgs e)
    {
        Player.Instance.UnlockCursorState();
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}