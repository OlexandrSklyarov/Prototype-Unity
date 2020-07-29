using System.Collections;
using UnityEngine;
using Zenject;

namespace SA.TenkyuClone
{
    public class MapGenerator : MonoBehaviour
    {
        #region Var

        [SerializeField] GameObject ballPrefab;

        [Space]
        [SerializeField] GameObject[] partPrefabs;


        SignalBus signalBus;
        Vector3 nextCreatePoint;
        Transform partContainer;

        const float VERTICAL_BALL_CREATE_OFFSET = 10f;
        const float PLATFORM_ROTATE_ANGLE = 65f;

        LevelPart currentLevelPart;
        GameObject gameBall;

        #endregion


        #region Init

        [Inject]
        public void Construct(SignalBus signalBus, [Inject(Id = "PART_CONTAINER")]Transform partContainer)
        {
            this.signalBus = signalBus;
            this.partContainer = partContainer;
            nextCreatePoint = Vector3.zero;

            StartCoroutine(InitMap(0.2f));
        }


        IEnumerator InitMap(float time)
        {
            yield return new WaitForSeconds(time);

            CreatePartMap();
            CreateBall();
        }

        #endregion


        #region Generation

        private void CreateBall()
        {
            var startPosition = currentLevelPart.EnterPoint.position +
                                Vector3.up * VERTICAL_BALL_CREATE_OFFSET;

            gameBall = Instantiate(ballPrefab, startPosition, Quaternion.identity, partContainer);

            signalBus.Fire(new SignalGame.CreateBall()
            {
                BallTransform = gameBall.transform
            });
        }


        void CreatePartMap()
        {
            //получаем случайный кусок
            var randomIndex = UnityEngine.Random.Range(0, partPrefabs.Length);
            var prefab = partPrefabs[randomIndex];

            var go = Instantiate(prefab, nextCreatePoint, GetVerticalRotation(PLATFORM_ROTATE_ANGLE), partContainer);
            currentLevelPart = go.GetComponent<LevelPart>();

            //инициализируем новый кусок
            var part = go.GetComponent<LevelPart>();
            part.Init(signalBus);

            //подписываемся на событие, когда мяч покинет кусок карты
            part.BallExitPoint.OnBallExit += (point) =>
            {
                nextCreatePoint = point.position;

                DeletePreviousPart();
                CreatePartMap();
                BallResetVelocity();
                BallSetStartPosition();
            };

            //подписываемся на событие, когда мяч вылетел за бортик
            part.BallRestartTrigger.OnBallFall += () => BallSetStartPosition();
        }


        Quaternion GetVerticalRotation(float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.up);
        }


        void BallSetStartPosition()
        {
            if (currentLevelPart)
            {
                gameBall.transform.position = currentLevelPart.EnterPoint.position +
                                                Vector3.up * VERTICAL_BALL_CREATE_OFFSET;
            }
        }


        void BallResetVelocity()
        {
            gameBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }


        void DeletePreviousPart()
        {
            if (currentLevelPart) Destroy(currentLevelPart.gameObject, 0.1f);
        }

        #endregion
    }
}