using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;

namespace SA.TestGame
{
    public class TimerUI : MonoBehaviour
    {
        #region Var

        [SerializeField] Button playButton;
        [SerializeField] Button pauseButton;
        [SerializeField] Button stopButton;
        [SerializeField] TextMeshProUGUI timeText;

        StringBuilder strBuilder = new StringBuilder();

        float hours;
        float minutes;
        float seconds;

        Coroutine timeCoroutine;

        bool isTimerWork;

        #endregion


        #region Init

        void Awake()
        {           
            InitButton();
        }


        void InitButton()
        {
            playButton.onClick.AddListener(() => 
            {
                timeCoroutine =  StartCoroutine( StartTimer() );
            });

            pauseButton.onClick.AddListener(() => PauseTimer());

            stopButton.onClick.AddListener(() => StopTimer());
        }

        #endregion


        #region Timer

        IEnumerator StartTimer()
        {
            isTimerWork = true;
                       
            while (isTimerWork)
            {
                seconds += Time.deltaTime;

                if (seconds > 59f)
                {
                    minutes++;
                    seconds = 0f;
                }

                if(minutes > 59f)
                {
                    hours++;
                    minutes = 0f;
                }

                if (hours > 59f) hours = 0f;

                ChangeTimeText();

                yield return null;
            }
        }


        void StopTimer()
        {
            isTimerWork = false;
            hours = minutes = seconds = 0;

            ChangeTimeText();
        }


        void PauseTimer()
        {
            isTimerWork = false;
        }


        void ChangeTimeText()
        {
            strBuilder.Clear();
            strBuilder.Append($"{hours:00}:{minutes:00}:{seconds:00}");
            timeText.text = strBuilder.ToString();
        }

        #endregion
    }
}
