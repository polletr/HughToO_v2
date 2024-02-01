using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private GameObject pausePopUp;
    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (continueButton != null)
        { 
            continueButton.onClick.AddListener(BackToLevel);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            Time.timeScale = 1f;
            isPaused = false;
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PositionPausePopUp();
            TogglePausePopUp();
        }
    }
    public void BackToLevel()
    {
        pausePopUp.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Saindo do jogo");
    }
    public void TogglePausePopUp()
    {
        Debug.Log("calling: TogglePausePopUp");
        if (pausePopUp != null)
        {
            if (isPaused == false)
            {
                Debug.Log("check: isPaused == false");
                pausePopUp.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
            else
            {
                Debug.Log("check: isPaused != false");
                pausePopUp.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }   
        }
    }
    void PositionPausePopUp()
    {
        Debug.Log("calling: PositionPausePopUp");
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(screenCenter);
        pausePopUp.transform.position = worldCenter;
    }
}
