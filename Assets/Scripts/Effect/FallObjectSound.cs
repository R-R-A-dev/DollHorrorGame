using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObjectSound : MonoBehaviour
{
    [SerializeField] AudioSource impactFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.5f)
        {
            impactFX.Play();
        }
    }
}
