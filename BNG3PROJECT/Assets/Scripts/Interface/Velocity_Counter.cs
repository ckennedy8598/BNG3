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
        _velocityText.text = (Mathf.Floor(_rb.velocity.x + _rb.velocity.z)).ToString();
    }
}
