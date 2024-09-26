using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject Player;

    private float _rotX;
    public float MinTurnAngle = -90f;
    public float MaxTurnAngle = 0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
