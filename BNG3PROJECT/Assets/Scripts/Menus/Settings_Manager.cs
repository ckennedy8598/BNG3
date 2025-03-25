using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Platformer
{
    public class Settings_Manager : MonoBehaviour
    {

        public TMP_Dropdown ResDropDown;
        public Toggle FullScreenToggle;

        Resolution[] AllResolutions;
        bool IsFullScreen;
        int SelectedResolution;
        List<Resolution> SelectedResolutionList = new List<Resolution>();

        // Start is called before the first frame update
        void Start()
        {
            IsFullScreen = true;
            AllResolutions = Screen.resolutions;

            List<string> resolutionStringList = new List<string>();
            foreach (Resolution res in AllResolutions)
            {
                var newRes = res.width.ToString() + " x " + res.height.ToString();
                if (!resolutionStringList.Contains(newRes))
                {
                    resolutionStringList.Add(newRes);
                    SelectedResolutionList.Add(res);
                }
            }

            ResDropDown.AddOptions(resolutionStringList);
        }

        public void ChangeResolution()
        {
            SelectedResolution = ResDropDown.value;
            Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, IsFullScreen);
        }

        public void ChangeFullScreen()
        {
            IsFullScreen = FullScreenToggle.isOn;
            Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, IsFullScreen);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
