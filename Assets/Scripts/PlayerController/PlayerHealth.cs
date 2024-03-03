using UnityEngine;


public class PlayerHealth : MonoBehaviour
{

    [Header("Health Sprites")]
    [SerializeField]
    private Sprite _fullheart;
    [SerializeField]
    private Sprite _emptyheart;

    private int maxHealth;

    [Header("Health UI")]
    [SerializeField]
    private GameObject[] _heartContainers;

    public PlayerBaseInfo _playerData;

    [HideInInspector]
    public bool isDead = false;

    private int _currentHealth;

    [SerializeField]
    private int _deathCooldown = 10;

    private float _deathAnimationTime;

    [SerializeField]
    private float _teleportDelay = 0.5f;

    Animator anim;

    void Start()
    {
        maxHealth = _playerData.Data.MaxHealth;
        _currentHealth = _playerData.Data.CurrentHealth;


        UpdateHeartUI();
    }
    void Update()
    {
        //testing controls
        /*        if (Input.GetKeyDown(KeyCode.Q))
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
                }
        */
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

        _playerData.Data.MaxHealth = maxHealth;
        _playerData.Data.CurrentHealth = _currentHealth;

    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            isDead = true;
            Debug.Log("Player is dead"); // Add death logic here
                                         //Player death animation

            GetComponent<Player>().HandlePotatoState();

            anim = GetComponent<Animator>();
            float clipLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            Invoke("SavePointTeleport", clipLength * 3f);

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
    public void HealToFull()
    {
        _currentHealth = maxHealth;
        _playerData.Data.MaxHealth = maxHealth;
        _playerData.Data.CurrentHealth = _currentHealth;
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
    public void SavePointTeleport()
    {
        Vector3 _teleportPosition = new Vector3(_playerData.Data.position[0], _playerData.Data.position[1], _playerData.Data.position[2]);
        GetComponent<Player>().ExitPotatoState();
        this.transform.position = _teleportPosition;
        Invoke("ComeBackToLife", _deathCooldown);
        HealToFull();
        if (GetComponent<Player>().currentStats.currentForm != ScriptableStats.Form.Water)
            GetComponent<Player>().ChangeForm(ScriptableStats.Form.Water);

    }
    void ComeBackToLife()
    {
        isDead = false;
    }

}
