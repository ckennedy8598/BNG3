using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Platformer
{
    public class Target_Script : MonoBehaviour
    {
        //private int test_hits;
        //public TMP_Text text;

        private MeshRenderer _targetMesh;
        private BoxCollider _collider;
        public AudioClip TargetHit;
        private void Start()
        {
            _targetMesh = GetComponent<MeshRenderer>();
            _collider = GetComponent<BoxCollider>();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Fireball(Clone)")
            {
                //text.text = "Number of Hits: " + test_hits++.ToString();
                _setDead();
                AudioSource.PlayClipAtPoint(TargetHit, gameObject.transform.position);
                Destroy(gameObject, 3);
            }
        }

        private void _setDead()
        {
            _targetMesh.enabled = false;
            _collider.enabled = false;
        }
    }
}
