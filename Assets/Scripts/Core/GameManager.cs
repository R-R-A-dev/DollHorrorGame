using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
    {
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null) instance = this;
               else Destroy(gameObject);
            
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

