using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    [Header("Health Sprites")]
    [SerializeField]
    private Sprite _fullheart;
    [SerializeField]
    private Sprite _emptyheart;

    [Header("Max Health")]
    [SerializeField]
    private int maxHealth = 5;

    [Header("Health UI")]
    [SerializeField]
    private GameObject[] _heartContainers;

    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
        UpdateHeartUI();
    }
    void Update()
    {
        //testing controls
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IncreaseMaxHealth(1);
        }*/
    }
    private void UpdateHeartUI()
    {
        for (int i = 0; i < _heartContainers.Length; i++)
        {
            if (i < _currentHealth)
                _heartContainers[i].GetComponent<UnityEngine.UI.Image>().sprite = _fullheart;
            else
                _heartContainers[i].GetComponent<UnityEngine.UI.Image>().sprite = _emptyheart;
        }
        for (int i = 0; i < _heartContainers.Length; i++)
        {
            if (i < maxHealth)
                _heartContainers[i].SetActive(true);
            else
                _heartContainers[i].SetActive(false);
        }

    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Debug.Log("Player is dead"); // Add death logic here
        }
        UpdateHeartUI();
    }
    public void Heal(int heal)
    {
        _currentHealth += heal;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        UpdateHeartUI();

    }
    public void IncreaseMaxHealth(int newMaxHealth)
    {
        if (maxHealth + newMaxHealth > _heartContainers.Length)
            maxHealth = _heartContainers.Length;
        else
            maxHealth += newMaxHealth;

        Heal(newMaxHealth);
        UpdateHeartUI();
    }
}