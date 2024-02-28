using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameControlsManager : Singleton<UIGameControlsManager>
{
    [SerializeField] private GameObject keyBoardUI;
    [SerializeField] private GameObject gamePadUI;

    [SerializeField] PlayerData player;

    [SerializeField] private Color colorLocked = Color.black;
    [SerializeField] private Color colorUnLocked = Color.white;

    [SerializeField] private Image[] water;
    [SerializeField] private Image[] ice;
    [SerializeField] private Image[] wind;

    private bool IceUnlocked = false;
    private bool WindsUnlocked = false;

    private void Awake()
    {
        keyBoardUI.SetActive(false);
        gamePadUI.SetActive(false);
    }

    public void SetToKeyBoardUI()
    {
        keyBoardUI.SetActive(true);
        gamePadUI.SetActive(false);
    }
    public void SetToGamePadUI()
    {
        keyBoardUI.SetActive(false);
        gamePadUI.SetActive(true);
    }

    private void Update()
    {
        IceUnlocked = player.HasIce;
        WindsUnlocked = player.HasWind;

        foreach (Image obj in water)//water
        {
            obj.color = colorUnLocked;
        }

        if (IceUnlocked)
        {
            foreach (Image obj in ice)
            {
                obj.color = colorUnLocked;
            }
        }
        else
        {
            foreach (Image obj in ice)
            {
                obj.color = colorLocked;
            }
        }

        if (WindsUnlocked)
        {
            foreach (Image obj in wind)
            {
                obj.color = colorUnLocked;
            }

        }
        else
        {
            foreach (Image obj in wind)
            {
                obj.color = colorLocked;
            }
        }
    }


}
