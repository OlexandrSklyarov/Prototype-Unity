using UnityEngine;
using Zenject;
using UnityEngine.UI;
using SA.TestGame.DI;

namespace SA.TestGame.UI
{
    public class MenuManagerUI : MonoBehaviour
    {
        #region Data

        [System.Serializable]
        protected struct StageButton
        {
            public Button btn;
            public int stageIndex;
        }

        #endregion


        #region Var

        [Header("Buttons")]
        [SerializeField] protected StageButton[] stageButtons;

        protected SignalBus signalBus;

        #endregion


        #region Init

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;

            Init();
        }


        protected virtual void Init()
        {
            for(int i = 0; i< stageButtons.Length; i++)
            {
                var stgButton = stageButtons[i];

                //подписываемся на нажатие кнопки уровня
                stgButton.btn.onClick.AddListener(() => 
                    SignalClickLevelButton(stgButton.stageIndex));
            }
        }


        //сигнал о нажатии на кнопку определённого уровня (по индексу)
        void SignalClickLevelButton(int index)
        {
            signalBus.Fire(new SignalUI.OnClickStageButton() { LevelIndex = index});
        }

        #endregion
    }
}