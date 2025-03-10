using effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadCallEffect : MonoBehaviour
{
    [SerializeField] OwnDollEffect ownDollEffect;
    [SerializeField] AudioSource callVoice;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            callVoice.Play();
            ownDollEffect.StartMove();
            this.gameObject.SetActive(false);
        }
    }
}
