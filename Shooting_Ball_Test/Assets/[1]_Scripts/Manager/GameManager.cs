using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SA.Game.ShootingBall
{
    public class GameManager : MonoBehaviour
    {
    
    #region Var
        
        [SerializeField] PlayerController player;
        [SerializeField] UIManager uIManager;


    #endregion


    #region Init

        void Awake()
        {
            if (uIManager == null)
                uIManager = GameObject.FindObjectOfType<UIManager>();


            if (player == null)
                player = GameObject.FindObjectOfType<PlayerController>();

            Subscription();
        }


        private void Subscription()
        {
            uIManager.OnClickBtnRestartEvent += RestartGame;
            uIManager.OnClickBtnQuitEvent += QuitGame;

            player.OnPlayeArriveToTargetEvent += GameWin;
            player.OnEmptyEnergyEvent += GameOver;
        }

        #endregion


        #region Events

        public event Action GameWinEvent;
        public event Action GameOverEvent;


    #endregion


    #region Update

        void GameWin()
        {
            GameWinEvent?.Invoke();
        }

        void GameOver()
        {
            GameOverEvent?.Invoke();
        }


        void RestartGame()
        {
            SceneManager.LoadScene(0);
        }


        void QuitGame()
        {
            Application.Quit();
        }

    #endregion

    }
}