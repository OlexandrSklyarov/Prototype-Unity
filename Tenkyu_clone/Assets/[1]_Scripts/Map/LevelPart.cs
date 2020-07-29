using UnityEngine;
using Zenject;

namespace SA.TenkyuClone
{
    public class LevelPart : MonoBehaviour
    {
        #region Properties

        public bool IsActive { get; private set; }
        public Transform EnterPoint => enterPoint;
        public BallExitTrigger BallExitPoint => exitTrigger;
        public BallReturnToStart BallRestartTrigger => ballRestartTrigger;

        #endregion


        #region Var

        [SerializeField] Transform enterPoint;
        [SerializeField] BallExitTrigger exitTrigger;
        [SerializeField] BallReturnToStart ballRestartTrigger;

        Quaternion originRotation;
        float verticalRotationAngle;
        float horizontalRotationAngle;

        const float MAX_ROTATTE_ANGLE = 25f;

        Transform myTR;

        #endregion


        #region Init

        public void Init(SignalBus signalBus)
        {
            myTR = transform;
            originRotation = transform.rotation;

            signalBus.Subscribe((SignalGame.PlayerRotation s) =>
            {
                if (IsActive)
                {
                    Rotate(s.Horizontal, s.Vertical, s.SpeedRotate);
                }
            });

            ballRestartTrigger.OnBallFall += () =>
            {
                verticalRotationAngle = 0f;
                horizontalRotationAngle = 0f;
            };
        }

        #endregion


        #region Controll 

        void Rotate(float horizontal, float vertical, float speed)
        {
            horizontalRotationAngle += horizontal * speed;
            verticalRotationAngle += vertical * speed;

            horizontalRotationAngle = Mathf.Clamp(horizontalRotationAngle, -MAX_ROTATTE_ANGLE, MAX_ROTATTE_ANGLE);
            verticalRotationAngle = Mathf.Clamp(verticalRotationAngle, -MAX_ROTATTE_ANGLE, MAX_ROTATTE_ANGLE);
        }


        void FixedUpdate()
        {
            var xRot = Quaternion.Euler(horizontalRotationAngle, 0f, 0f);
            var zRot = Quaternion.Euler(0f, 0f, verticalRotationAngle);

            transform.rotation = originRotation * xRot * zRot;
        }

        #endregion


        #region Trigger collisionk

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<GameBall>() is GameBall)
            {
                IsActive = true;
            }
        }


        void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<GameBall>() is GameBall)
            {
                IsActive = false;
            }
        }

        #endregion
    }
}
