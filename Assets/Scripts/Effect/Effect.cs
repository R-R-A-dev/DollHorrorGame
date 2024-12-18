using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effects
{
    public abstract class Effect : MonoBehaviour
    {
        public string effectName;

        public abstract void PlayEffect();
    }

}
