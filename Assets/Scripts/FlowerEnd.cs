using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowerEnd : MonoBehaviour
{
    public UnityEvent winCondition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && ClimateManager.Instance.currentState == 0)
        {
            winCondition.Invoke();
        }
    }


}
