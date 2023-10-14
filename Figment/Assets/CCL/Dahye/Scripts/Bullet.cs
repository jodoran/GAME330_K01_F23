using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    public class Bullet : Monobehaviour
    {
        //OnCollisionEnter
        //
        public float bulletDamage;

        void Start ()
        {

        }

        void Update () 
        {
            
        }

        OnCollisionEnter()
        {
            OnDamaged;
        }


    }

}