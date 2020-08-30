using UnityEngine;

namespace SA.TestGame
{
    public class GameSettings
    {

        #region Properties

        public int StageIndex { get; set; }

        #endregion


        #region Var

        private static object syncRoot = new object();
        private static GameSettings instance;

        #endregion


        #region Init

        private GameSettings()
        {
            Debug.Log("GameSettings => Construct()");
        }


        public static GameSettings GetInstance()
        {
            lock (syncRoot)
            {
                if (instance == null)
                {
                    instance = new GameSettings();
                }
            }

            return instance;
        }

        #endregion

    }
}