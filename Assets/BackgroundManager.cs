using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    [SerializeField] private GameObject correctBackground;

    [SerializeField] private GameObject[] otherBackground;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!correctBackground.active && collision.gameObject.GetComponent<Player>())
        {
            foreach (var background in otherBackground)
            {
                background.gameObject.SetActive(false);
            }
            correctBackground.SetActive(true);

        }
    }

}
