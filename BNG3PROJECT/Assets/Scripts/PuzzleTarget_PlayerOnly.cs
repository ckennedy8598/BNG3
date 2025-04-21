using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PuzzleTarget_PlayerOnly : MonoBehaviour
    {
        public PuzzleGateController gateController;
        private bool isHit = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fireball") && !isHit)
            {
                isHit = true;
                gateController?.NotifyTargetDestroyed();
                Destroy(gameObject);
            }
        }
    }
}
