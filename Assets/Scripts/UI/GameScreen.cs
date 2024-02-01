using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    #region Private Varaiables
    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject drop1;
    [SerializeField]
    private GameObject drop2;
    [SerializeField]
    private GameObject drop3;
    [SerializeField]
    private GameObject summerBar;
    [SerializeField]
    private GameObject fallBar;
    [SerializeField]
    private GameObject winterBar;
    [SerializeField]
    private GameObject summerText;
    [SerializeField]
    private GameObject fallText;
    [SerializeField]
    private GameObject winterText;
    [SerializeField]
    private GameObject summerPicture;
    [SerializeField]
    private GameObject fallPicture;
    [SerializeField]
    private GameObject winterPicture;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Atualizar a visibilidade das imagens
        UpdateImageVisibility();
        UpdateObjectColors();
    }
    private void UpdateObjectColors()//just colouring the drops according the player be taking the collectable items
    {
        SetObjectColor(drop1, Color.gray);//Make sure the objects are in their original colours
        SetObjectColor(drop2, Color.gray);
        SetObjectColor(drop3, Color.gray);

        if (CollectibleManager.Instance.collectibleCount >= 1) SetObjectColor(drop1, Color.white);//Refresh the object colours based in this variable value
        if (CollectibleManager.Instance.collectibleCount >= 2) SetObjectColor(drop2, Color.white);
        if (CollectibleManager.Instance.collectibleCount >= 3) SetObjectColor(drop3, Color.white);
    }
    private void SetObjectColor(GameObject obj, Color color)
    {
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
            else
            {
                Image image = obj.GetComponent<Image>();
                if (image != null)
                {
                    image.color = color;
                }
            }
        }
    }
    private void UpdateImageVisibility() //Changing the bar visibility
    {
        switch (ClimateManager.Instance.currentState)
        {
            case ClimateManager.State.Water:
                fallBar.gameObject.SetActive(true);
                winterBar.gameObject.SetActive(false);
                summerBar.gameObject.SetActive(false);

                summerText.gameObject.SetActive(false);
                fallText.gameObject.SetActive(true);
                winterText.gameObject.SetActive(false);

                summerPicture.gameObject.SetActive(false);
                fallPicture.gameObject.SetActive(true);
                winterPicture.gameObject.SetActive(false);
                break;
            case ClimateManager.State.Gas:
                fallBar.gameObject.SetActive(false);
                winterBar.gameObject.SetActive(false);
                summerBar.gameObject.SetActive(true);

                summerText.gameObject.SetActive(true);
                fallText.gameObject.SetActive(false);
                winterText.gameObject.SetActive(false);

                summerPicture.gameObject.SetActive(true);
                fallPicture.gameObject.SetActive(false);
                winterPicture.gameObject.SetActive(false);
                break;
            case ClimateManager.State.Ice:
                fallBar.gameObject.SetActive(false);
                winterBar.gameObject.SetActive(true);
                summerBar.gameObject.SetActive(false);

                summerText.gameObject.SetActive(false);
                fallText.gameObject.SetActive(false);
                winterText.gameObject.SetActive(true);

                summerPicture.gameObject.SetActive(false);
                fallPicture.gameObject.SetActive(false);
                winterPicture.gameObject.SetActive(true);
                break;
        }
    }
    private void OnEnable()
    {
        UpdateObjectColors();
    }
}
