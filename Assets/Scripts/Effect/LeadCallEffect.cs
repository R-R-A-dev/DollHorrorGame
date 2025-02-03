using effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadCallEffect : MonoBehaviour
{
    [SerializeField] OwnDollEffect ownDollEffect;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ownDollEffect.StartMove();
            this.gameObject.SetActive(false);
        }
    }
}
