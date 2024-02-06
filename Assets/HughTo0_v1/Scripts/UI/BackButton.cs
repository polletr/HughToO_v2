using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
    public void BackToMainMenu()
    {
        Debug.Log("Funcao chamada");
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
