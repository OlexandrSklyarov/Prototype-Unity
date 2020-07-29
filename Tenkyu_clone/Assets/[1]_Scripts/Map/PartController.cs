using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SA.TenkyuClone
{
    public class PartController : MonoBehaviour
    {
        #region Var

        [SerializeField] float rotateSpeed = 5f;

        SignalBus signalBus;
        Transform mainCameraTR;

        const float MAX_SENSETIVE = 1f;

        bool isMousePressed;

        #endregion


        #region Init

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            this.mainCameraTR = Camera.main.transform;

            InitInput();
        }


        void InitInput()
        {
            ClickMouse();
            InputSwipe();
            ResetButton();
            ExitButton();
        }


        void ExitButton()
        {
            signalBus.Subscribe<SignalGame.OnClickExitButtton>(() =>
            {
                Application.Quit();
            });
        }


        void ResetButton()
        {
            signalBus.Subscribe<SignalGame.OnClickResetButton>(() =>
            {
                var currrentLevel = SceneManager.GetActiveScene();
                SceneManager.LoadSceneAsync(currrentLevel.name);
            });
        }


        //нажатие на кнопку мыши
        void ClickMouse()
        {
            signalBus.Subscribe((SignalGame.OnClickPointer s) =>
            {
                isMousePressed = s.IsPressed;
            });
        }


        //ввод пользователя через экран
        void InputSwipe()
        {
            //подписываемся на перемещение указателя по экрану
            signalBus.Subscribe((SignalGame.OnDragPointer s) =>
            {
                //не зажата кномка мыши, выходим
                if (!isMousePressed) return;

                var pointer = s.Direction;

                signalBus.Fire(new SignalGame.PlayerRotation()
                {
                    Horizontal = Mathf.Clamp(pointer.x, -MAX_SENSETIVE, MAX_SENSETIVE),
                    Vertical = Mathf.Clamp(pointer.y, -MAX_SENSETIVE, MAX_SENSETIVE),
                    SpeedRotate = rotateSpeed
                });
            });
        }

        #endregion


        #region Disable       

        #endregion
    }
}