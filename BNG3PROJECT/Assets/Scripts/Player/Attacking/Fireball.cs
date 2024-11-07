using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private FireballType fireballType;
        public enum FireballType {LookatCamera, CameraForward};

        private float _duration;

        public SpriteRenderer _spriteMesh;
        public SphereCollider _sphereCollider;

        public AudioClip _audioClip;
        // Start is called before the first frame update
        void Start()
        {
            _duration = 5f;
        }

        // Update is called once per frame
        void Update()
        {
            _duration -= Time.deltaTime;
            if (_duration < 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void LateUpdate()
        {
            switch (fireballType)
            {
                case FireballType.LookatCamera:
                    transform.LookAt(Camera.main.transform.position, Vector3.up);
                    break;
                case FireballType.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                default:
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Jukebox")
            {
                Destroy(other.gameObject);
                AudioSource.PlayClipAtPoint(_audioClip, gameObject.transform.position);
                _setDead();
                Destroy(gameObject, 2);
            }
            else if(other.gameObject.name != "PlayerBody")
            {
                AudioSource.PlayClipAtPoint(_audioClip, gameObject.transform.position);
                _setDead();
                Destroy(gameObject, 2);
            }
        }

        public void _setDead()
        {
            _spriteMesh.enabled = false;
            _sphereCollider.enabled = false;
        }
    }
}
