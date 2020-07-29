using System;
using UnityEngine;

namespace SA.TenkyuClone
{
    public class BallReturnToStart : MonoBehaviour
    {
        #region Event

        public event Action OnBallFall;

        #endregion

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<GameBall>() is GameBall)
            {
                OnBallFall?.Invoke();
            }
        }
    }
}