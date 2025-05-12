using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class SoundManager : MonoBehaviour
    {
        public Slider VolSlider;
        public float Volume;

        [Header("Volume Number Display")]
        public TMP_Text VolumeCounter;
        // Start is called before the first frame update
        void Start()
        {
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 50);
                Load();
            }
            else
            {
                Load();
            }
        }

        // Update is called once per frame
        void Update()
        {
            _updateCounter();
        }

        private void _updateCounter()
        {
            if (VolSlider != null)
            {
                if (VolSlider.value > 0)
                {
                    VolumeCounter.text = (VolSlider.value * 100).ToString("#");
                }
                else
                {
                    VolumeCounter.text = "0";
                }
            }
        }

        public void ChangeVolume()
        {
            AudioListener.volume = VolSlider.value;
            Save();
        }

        void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", VolSlider.value);
        }

        void Load()
        {
            VolSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
    }
}
