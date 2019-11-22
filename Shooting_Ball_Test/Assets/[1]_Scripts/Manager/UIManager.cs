using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA.Game.ShootingBall
{
    public class UIManager : MonoBehaviour
    {

    #region Var

        [SerializeField] GameManager gameManager;
        [SerializeField] GameObject infoPanel;
        [SerializeField] Text textMsg;


    #endregion


    #region Init

        void Awake()
        {
            if (gameManager == null)
                gameManager = GameObject.FindObjectOfType<GameManager>();

            InitInfopanel();
            Subscription();
        }


        private void Subscription()
        {
            gameManager.GameWinEvent += () =>
            {
                InfopanelEnabled(true);
                SetMessage("You win!!!", Color.green);
            };

            gameManager.GameOverEvent += () =>
            {
                InfopanelEnabled(true);
                SetMessage("GAME OVER", Color.red);
            };
            
        }

        private void InitInfopanel()
        {
            if (infoPanel == null)
                infoPanel = GameObject.Find("[UI]/Canvas/InfoPanel").gameObject;

            //restart button
            var btnRestart = infoPanel.transform.Find("RestartButton").GetComponent<Button>();
            btnRestart.onClick.AddListener( () =>
            {
                InfopanelEnabled(false);
                OnClickBtnRestartEvent?.Invoke();
            });

            //quit button
            var btnQuit = infoPanel.transform.Find("QuitButton").GetComponent<Button>();
            btnQuit.onClick.AddListener( () =>
            {
                OnClickBtnQuitEvent?.Invoke();
            });

            //msg text
            if (textMsg == null)
                textMsg = infoPanel.transform.Find("Text").GetComponent<Text>();

            InfopanelEnabled(false);           

        }

    #endregion

        public event Action OnClickBtnRestartEvent;
        public event Action OnClickBtnQuitEvent;

    #region Events



    #endregion


    #region Update

        void SetMessage(string msg, Color color)
        {
            textMsg.color = color;
            textMsg.text = msg;
        }


        void InfopanelEnabled(bool enable)
        {
            infoPanel.SetActive(enable); 
        }

    #endregion

    }
}