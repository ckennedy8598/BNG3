using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Velocity_Counter : MonoBehaviour
{
    public TMP_Text _velocityText;
    public Rigidbody _rb;

    // Update is called once per frame
    void Update()
    {
        _velocityText.text = Mathf.Floor(Mathf.Abs(_rb.velocity.x) + Mathf.Abs(_rb.velocity.z)).ToString();
    }
}
