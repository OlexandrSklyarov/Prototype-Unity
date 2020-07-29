using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace SA.TenkyuClone.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Var

        [SerializeField] Button resetButton;
        [SerializeField] Button exitButton;
        [SerializeField] TouchInputPanel touchPanel;

        #endregion


        #region Init

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            resetButton.onClick.AddListener(() =>
            {
                signalBus.Fire(new SignalGame.OnClickResetButton());
            });

            exitButton.onClick.AddListener(() =>
            {
                signalBus.Fire(new SignalGame.OnClickExitButtton());
            });

            touchPanel.OnDragPointer += (dir) =>
            {
                signalBus.Fire(new SignalGame.OnDragPointer()
                { 
                    Direction = dir
                });
            };

            touchPanel.OnPressedPointer += (isPressed) =>
            {
                signalBus.Fire(new SignalGame.OnClickPointer()
                {
                    IsPressed = isPressed
                });
            };
        }

        #endregion
    }
}