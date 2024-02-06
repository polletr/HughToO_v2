using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button muteButton;
    [SerializeField]
    private Button howToPlayButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private GameObject muteCross;

    private bool gameIsMuted = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //These if statements will check if there are some buttons attached to the variables and if yes, add a new listener to their respective functions
        #region If Statements
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            Debug.Log("Listener for PlayButton added!");
        }
        if (muteButton != null)
        {
            muteButton.onClick.AddListener(OnMuteButtonClicked);
            Debug.Log("Listener for Settings added!");
        }
        if (howToPlayButton != null)
        {
            howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
            Debug.Log("Listener for howtoPlayButton added!");
        }
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
            Debug.Log("Listener for quit added!");
        }
        if (creditsButton != null)
        {
            creditsButton.onClick.AddListener(OnCreditsButtonClicked);
            Debug.Log("Listener for credits added!");
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Level1");
        Debug.Log("clicou em play");
    }
    void OnMuteButtonClicked()
    {

        if (muteCross != null)
        {
            if (gameIsMuted == false)
            {
            muteCross.SetActive(true);
            gameIsMuted = true;
            Debug.Log(gameIsMuted);
            }
        else
            {
            muteCross.SetActive(false);
            gameIsMuted = false;
            }
        }
       
    }
    void OnHowToPlayButtonClicked()
    {
        SceneManager.LoadScene("How To Play");
        Debug.Log("clicou em how to play");
    }
    void OnQuitButtonClicked()
    {
        Debug.Log("clicou em quit");
        Application.Quit();
    }
    void OnCreditsButtonClicked()
    {
        Debug.Log("clicou em Credits");
        SceneManager.LoadSceneAsync("Credit");
    }
}
