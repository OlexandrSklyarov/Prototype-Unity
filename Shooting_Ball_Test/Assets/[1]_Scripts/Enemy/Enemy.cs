using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Game.ShootingBall
{
    public class Enemy : MonoBehaviour
    {

    #region Var 

        DataEnemy data;

        bool isInfected;

    #endregion 


    #region Init

        void Awake()
        {
            data = Resources.Load<DataEnemy>(Prm.Path.ENEMY_DATA);
        }

    #endregion 


    #region Events

        //public event Action<int> OnDestroyEnemyEvent;

    #endregion 


    #region Infected

        //если еще не инфицированы, то заражаемся, и пытаемся заразить окружающих в радиусе
        public void Infect(float radius, Color color)
        {
            if (!isInfected)
            {
                isInfected = true;                
                SetColor(color);
                StartCoroutine(InfectedNearby(radius, color));                
            }
        }


        private void SetColor(Color newColor)
        {
            gameObject.GetComponent<Renderer>().material.color = newColor;
        }


        IEnumerator InfectedNearby(float radius, Color color)
        {
            yield return new WaitForSeconds(data.timeToInfectedOther);

            var hitColliders = Physics.OverlapSphere(transform.position, radius);

            foreach(var c in hitColliders)
            {
                var enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Infect(radius, color);
                }
            }

            StartCoroutine( AutoDestroy() );
        }


    #endregion


    #region Destroy

        IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(data.timeToAutodestroy);
            Destroy(gameObject);
        }


        void OnDestriy()
        {
            //эффект
        }


    #endregion

    }
}