using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SA.TenkyuClone
{
    public class CameraFollow : MonoBehaviour
    {
        #region Var

        [SerializeField] Transform target;
        [SerializeField] [Range(1f, 50f)] float moveSpeed = 30f;
        [SerializeField] Vector3 offset = new Vector3(0, 45, -50);

        Transform myTR;
        SignalBus signalBus;

        #endregion


        #region Init

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            myTR = transform;
            this.signalBus = signalBus;

            signalBus.Subscribe((SignalGame.CreateBall s) =>
            {
                target = s.BallTransform;
            });
        }

        #endregion


        #region Update       


        void LateUpdate()
        {
            if (target == null) return;

            transform.position = Vector3.MoveTowards(transform.position, target.position +
                                                offset, Time.deltaTime * moveSpeed);

            RotateToTarget();
        }


        void RotateToTarget()
        {
            if (target == null) return;

            myTR.LookAt(target);
        }

        #endregion
    }
}