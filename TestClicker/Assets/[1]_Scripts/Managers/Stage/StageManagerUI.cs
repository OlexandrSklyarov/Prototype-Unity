using UnityEngine;
using UnityEngine.UI;
using SA.TestGame.DI;
using TMPro;

namespace SA.TestGame.UI
{
    public class StageManagerUI : MenuManagerUI
    {
        #region Var

        [Space]
        [SerializeField] Button closeStageButton;
        [SerializeField] TextMeshProUGUI stageName;

        #endregion


        #region Init

        protected override void Init()
        {
            base.Init();

            SetStageName();

            //нажатие на кнопку закрытие уровня
            closeStageButton.onClick.AddListener(() =>
            {
                signalBus.Fire(new SignalUI.OnClickCloseStageButton());
            });
        }


        void SetStageName()
        {
            var stageIndex = GameSettings.GetInstance().StageIndex;
            stageName.text = string.Format($"Stage{stageIndex}");
        }

        #endregion
    }
}