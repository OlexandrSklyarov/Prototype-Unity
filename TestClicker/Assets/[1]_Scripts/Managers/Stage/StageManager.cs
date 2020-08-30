using SA.TestGame.Data;
using SA.TestGame.DI;
using SA.TestGame.Util;
using UnityEngine;
using Zenject;

namespace SA.TestGame
{
    public class StageManager : MenuManager
    {
        #region Init

        //обработка сигналов
        protected override void Init()
        {
            base.Init();

            //обработка закрытия уровня
            signalBus.Subscribe((SignalUI.OnClickCloseStageButton s) =>
            {
                LoadScene(StaticPrm.Scene.MAIN_MENU);
            });

            CreateSceneContent();
        }       


        //создает нужный префаб на сцене в зависимости от выбраного уровня
        void CreateSceneContent()
        {           
            var index = GameSettings.GetInstance().StageIndex;
            index = Mathf.Clamp(index, 1, dataGame.StagePrefabs.Length);
            var prefab = dataGame.StagePrefabs[index-1];

            Instantiate(prefab);
        }

        #endregion
    }
}