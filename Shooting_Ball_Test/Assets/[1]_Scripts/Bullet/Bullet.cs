using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Game.ShootingBall
{
    public class Bullet : MonoBehaviour
    {

    #region 

        public Vector3 StartPoint {get; set;}

    #endregion

        
    #region Collision

        private void OnCollisionEnter(Collision other) 
        {
            var possibleEnemy = other.gameObject.GetComponent<Enemy>();

            if( possibleEnemy is Enemy)
            {
                var myColor = GetComponent<Renderer>().material.color;
                possibleEnemy.Infect(transform.localScale.magnitude, myColor);
            }

            Destroy(gameObject);
        }

    #endregion


    #region Update

        void Update()
        {
            if (Vector3.Distance(StartPoint, transform.position) > 1000f)
            {
                Destroy(gameObject);
            }
        }

    #endregion

    }
}
