using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class setgamemasternobreak : MonoBehaviour
    {
        public GameObject GameMaster;
        public GameMaster gm;

        void Start()
        {
            GameMaster = GameObject.Find("Game Master");
            gm = GameMaster.GetComponent<GameMaster>();
            gm.MainMenu = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
