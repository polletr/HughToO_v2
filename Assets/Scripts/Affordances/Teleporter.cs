using System.Collections;
using UnityEngine;

namespace Weather
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private AudioClip _teleportOutClip,_teleportInClip;
        [SerializeField] private GameObject _teleportParticlePrefab;
        [SerializeField] private float _teleportDelay = 0.5f;
        [SerializeField] private bool _isSavePoint;

        [SerializeField] private PlayerBaseInfo PlayerData;
        [SerializeField] private GameObject _blackScreen;

        private Vector3 _teleportPosition;

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void Update()
        {
                
        }

        private void SavePointTeleport(GameObject player)
        {
            if (PlayerData != null)
            {
               _teleportPosition.x = PlayerData.Data.position[0];
               _teleportPosition.y = PlayerData.Data.position[1] ;
               _teleportPosition.z = PlayerData.Data.position[2] ;
            }

            player.transform.position = _teleportPosition;
            player.GetComponent<Player>().HandlePotatoState(_teleportDelay);

        }
        public void Teleport(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameObject player = collision.gameObject;
                if (_isSavePoint)
                {
                    SavePointTeleport(player);
                }
                else
                {
                    CheckPointTeleport(player);
                }
                
            }
        }

        public void CheckPointTeleport(GameObject player)
        {
            //_teleportPosition = player.GetComponent<Player>().LastCheckPoint.position;
            player.transform.position = _teleportPosition;
            player.GetComponent<Player>().HandlePotatoState(_teleportDelay);
        }

        void FadeInOutScreen()
        {

            if (!_blackScreen)
            {

                
            }
        }

    }
}