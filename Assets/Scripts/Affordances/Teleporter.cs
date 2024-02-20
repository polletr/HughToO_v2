using System.Collections;
using UnityEngine;

    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private AudioClip _teleportOutClip,_teleportInClip;
        [SerializeField] private GameObject _teleportParticlePrefab;
        [SerializeField] private float _teleportDelay = 0.5f;

        [SerializeField] private PlayerBaseInfo PlayerData;
        [SerializeField] private GameObject _blackScreen;

        [SerializeField] private Transform PosA;
        [SerializeField] private Transform PosB;


     

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

    }
