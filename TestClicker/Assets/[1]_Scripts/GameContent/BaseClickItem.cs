using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SA.TestGame
{
    public abstract class BaseClickItem : MonoBehaviour
    {
        #region Var

        [SerializeField] [Range(0.1f, 500f)] float moveSpeed = 10f;

        protected Transform myTR;

        Vector3 targetPosition;    
        bool isMoveProcess;

        const float MIN_DISTANCE = 0.2f;

        #endregion


        #region Init

        protected virtual void Awake()
        {
            myTR = transform;
            targetPosition = Vector3.zero;
        }

        #endregion


        #region Move

        protected void RandomMove(float horizontallLimit, float verticalLimit)
        {
            if (isMoveProcess) return;

            //задает рандомную позицию для передвижения предмету
            targetPosition = new Vector3()
            {
                x = Random.Range(-horizontallLimit, horizontallLimit),
                y = Random.Range(-verticalLimit, verticalLimit),
                z = 0f
            };

            StartCoroutine(Move());
        }


        IEnumerator Move()
        {
            isMoveProcess = true;

            while (Vector2.Distance(myTR.localPosition, targetPosition) > MIN_DISTANCE)
            {
                myTR.localPosition = Vector3.MoveTowards(myTR.localPosition,
                                                        targetPosition,
                                                        moveSpeed);
                yield return null;
            }

            isMoveProcess = false;
        }

        #endregion      

    }
}