using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class IceDetectorBox : MonoBehaviour
{

        [SerializeField]
        GameObject _iceBox;


        void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            if (_iceBox != null)
                _iceBox.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<Player>()?.currentStats.currentForm == ScriptableStats.Form.Ice)
            {
                if (_iceBox != null)
                    _iceBox.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_iceBox != null)
                    _iceBox.SetActive(false);
            }
        }

    }
