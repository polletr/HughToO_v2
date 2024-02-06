using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{ 
    [SerializeField]
    private GameObject drop1;
    [SerializeField]
    private GameObject drop2;
    [SerializeField]
    private GameObject drop3;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject tryAgain;
    [SerializeField]
    private GameObject goToNextLevel;


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        UpdateObjectColors();
    }
    private void UpdateObjectColors()
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
}
