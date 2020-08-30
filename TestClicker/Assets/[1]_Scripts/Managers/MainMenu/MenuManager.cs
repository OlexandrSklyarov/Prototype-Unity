using UnityEngine;
using Zenject;
using SA.TestGame.DI;
using UnityEngine.SceneManagement;
using SA.TestGame.Util;
using SA.TestGame.Data;
using System;

namespace SA.TestGame
{
    public class MenuManager : MonoBehaviour
    {
        #region Var

        protected DataGame dataGame;
        protected SignalBus signalBus;

        #endregion


        #region Init

        [Inject]
        public void Construct(SignalBus signalBus, DataGame dataGame)
        {
            this.signalBus = signalBus;
            this.dataGame = dataGame;

            Init();
        }


        protected virtual void Init()
        {
            //обработка выбора уровня
            signalBus.Subscribe((SignalUI.OnClickStageButton s) => 
            {
                SetupStageData(s.LevelIndex);
                LoadScene(StaticPrm.Scene.STAGE);
            });
        }


        //устанавливает в настройках индекс прифаба, 
        //который нужно создать при загрузки уровня
        void SetupStageData(int levelIndex)
        {
            GameSettings.GetInstance().StageIndex = levelIndex;
        }


        protected void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        #endregion

    }
}