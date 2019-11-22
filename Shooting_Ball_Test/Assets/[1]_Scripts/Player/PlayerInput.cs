using System;
using UnityEngine;

namespace SA.Game.ShootingBall
{
    public class PlayerInput : MonoBehaviour
    {
    
    #region Var

        Vector3 pointClick;
        bool isFindTarget;

    #endregion


    #region Events

        public event Action<Vector3> OnPointerClickEvent;
        public event Action OnPointerDownEvent;
        public event Action OnPointerUpEvent;

    #endregion


    #region Update Input

        void Update()    
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnPointerClick();
            }
            
            if (Input.GetMouseButton(0))
            {
                OnPointerDown();
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                OnPointerUp();
            }
        }
        

        public void OnPointerClick()
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                pointClick = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                OnPointerClickEvent?.Invoke(pointClick);
                isFindTarget = true;
            }          
        }


        public void OnPointerDown()
        {
            if(isFindTarget)
            {
                OnPointerDownEvent?.Invoke();
            }            
        }


        public void OnPointerUp()
        {
            isFindTarget = false;
            pointClick = Vector3.zero;

            OnPointerUpEvent?.Invoke();
        }

        #endregion

    }
}