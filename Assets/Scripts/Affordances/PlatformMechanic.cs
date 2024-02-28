using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMechanic : MonoBehaviour
{
    private GameObject currentOneWayPlatform;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {

        BoxCollider2D plataformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        plataformCollider.enabled = false;
        yield return new WaitForSeconds(0.25f);
        plataformCollider.enabled = true;
    }
}
