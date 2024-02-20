using System.Collections;
using UnityEngine;

namespace Weather
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private AudioClip _teleportOutClip,_teleportInClip;
        [SerializeField] private GameObject _teleportParticlePrefab;
        [SerializeField] private float _teleportDelay = 0.5f;

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

        private void TeleportPlayer(GameObject player)
        {
            if (PlayerData != null)
            {
               _teleportPosition.x = PlayerData.Data.position[0];
               _teleportPosition.y = PlayerData.Data.position[1] ;
               _teleportPosition.z = PlayerData.Data.position[2] ;
            }
            //player.enabled = true;

            player.transform.position = _teleportPosition;
            player.GetComponent<Player>().HandlePotatoState(_teleportDelay);

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                TeleportPlayer(collision.gameObject);
            }
            
            //Instantiate(_teleportParticlePrefab, collision.transform.position, Quaternion.identity);


           // AudioSource.PlayClipAtPoint(_teleportInClip, _teleportPosition);

            //StartCoroutine(ActivatePlayer(player));
        }

        void FadeInOutScreen()
        {

            if (!_blackScreen)
            {

                
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, _teleportPosition);
            Gizmos.DrawSphere(_teleportPosition, 0.2f);
        }
    }
}