using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
[RequireComponent(typeof(BoxCollider2D))]
public class WinBox : MonoBehaviour
{
    public UnityEvent Win;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player has entered the win box");
            Win?.Invoke();
            StartCoroutine(OnLoadWinLevel());
        }
    }

    public void LoadWinLevel()
    {
        StartCoroutine(OnLoadWinLevel());
    }

    public IEnumerator OnLoadWinLevel()
    {
        yield return new WaitForSeconds(2f); // Wait for 3 seconds
        SceneManager.LoadScene("02_WinScreen");
    }
}
