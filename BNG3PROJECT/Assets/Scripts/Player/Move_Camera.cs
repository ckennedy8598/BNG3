using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    public Player_Cam PCam_Script;

    public Transform Target;

    private void Start()
    {
        PCam_Script = FindAnyObjectByType<Player_Cam>();
    }
    void Update()
    {
        if (PCam_Script.DeathCamera)
        {
            return;
        }
        else
        {
            transform.position = Target.position;
        }
    }
}
