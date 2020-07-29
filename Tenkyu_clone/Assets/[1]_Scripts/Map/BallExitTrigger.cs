using System;
using UnityEngine;

namespace SA.TenkyuClone
{
    public class BallExitTrigger : MonoBehaviour
    {
        #region Var

        [SerializeField] Transform newSpawnPoint;

        #endregion


        #region Event

        public event Action<Transform> OnBallExit;

        #endregion

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<GameBall>() is GameBall)
            {
                OnBallExit?.Invoke(newSpawnPoint);
            }
        }
    }
}