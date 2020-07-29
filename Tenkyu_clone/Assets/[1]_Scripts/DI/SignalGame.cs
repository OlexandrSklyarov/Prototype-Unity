using UnityEngine;

namespace SA.TenkyuClone
{
    public class SignalGame
    {
        public class CreateBall
        {
            public Transform BallTransform { get; set; }
        }


        public class PlayerRotation
        {
            public float Horizontal { get; set; }
            public float Vertical { get; set; }
            public float SpeedRotate { get; set; }
        }


        public class OnClickResetButton { }


        public class OnClickExitButtton { }


        public class OnClickPointer
        {
            public bool IsPressed { get; set; }
        }


        public class OnDragPointer 
        {
            public Vector2 Direction { get; set; }
        }
    }
}