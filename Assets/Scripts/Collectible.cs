using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            CollectibleManager.Instance.Add();
            CollectibleManager.Instance.PlaySound();
            Destroy(this.gameObject);
        }
    }

}
