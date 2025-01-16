using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace effects
{
    public class OwnDollEffect : Effect
    {
        [SerializeField] Light light;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        void OwnDollEffectHandler()
        {

        }

        public override void PlayEffect()
        {

        }

        public void Scream()
        {
               
        }

        public void InsectSound()
        {

        }
    
        public void InsectsInfest()
        {//TODO: 這うゴキブリ
            //Cockroach rayで識別してまとわりつく
        }
        
        public void Cry()
        {

        }

        public void Laugh()
        {

        }

        public void Tremble()
        {

        }

        public void LightFlicking()
        {
            
        }

        public void PoorVisibility()
        {
            //TODO: 次にやる

        }

        public void FootSound()
        {

        }

        public void LeadDoll()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            //OwnDollEffectHandler();
        }
    }
}
/*視界演出の列挙
 * 声とセット
 * 
*/
