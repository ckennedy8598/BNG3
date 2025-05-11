using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer
{
    public class StoryboardSFX : MonoBehaviour
    {
        public AudioSource SwordClash;
        public AudioSource SwordFleshImpact;
        public AudioSource BloodSpill;

        public void SwordClashSFX()
        {
            SwordClash.Play();
        }

        public void SwordImpactSFX()
        {
            SwordFleshImpact.Play();
        }

        public void BloodSpillSFX()
        {
            BloodSpill.Play();
        }
    }
}
