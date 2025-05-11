using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class Storyboard_Handler : MonoBehaviour
    {
        public StoryboardSFX StoryboardSFX;
        public GameObject SkipIntro;

        public void SetSkipIntroActive()
        {
            SkipIntro.SetActive(true);
        }

        public void SFXSwordClash()
        {
            StoryboardSFX.SwordClashSFX();
        }

        public void SFXSwordImpact()
        {
            StoryboardSFX.SwordImpactSFX();
        }

        public void SFXBloodSpill()
        {
            StoryboardSFX.BloodSpillSFX();
        }

        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
