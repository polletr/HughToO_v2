using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDone : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private GameObject transitionPopUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Funcao sendo chamada");
        // Verifica se o objeto que entrou em contato é o jogador (você pode ajustar isso conforme necessário)
        if (other.CompareTag("Player"))
        {
            // Ativa o objeto desejado
            if (transitionPopUp != null)
            {
                transitionPopUp.SetActive(true);
            }

            // Pausa o jogo (se desejado)
            Time.timeScale = 0f;
        }
    }
}
