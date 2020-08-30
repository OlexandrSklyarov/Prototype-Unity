using UnityEngine;
using UnityEngine.EventSystems;

namespace SA.TestGame
{
    public class ClickImage : BaseClickItem, IPointerClickHandler
    {
        #region Var

        float width;
        float height;

        #endregion


        #region Init

        protected override void Awake()
        {
            base.Awake();

            myTR = transform.GetChild(0).transform;

            var delta = GetComponent<RectTransform>().sizeDelta;
            width = delta.x * 0.3f;
            height = delta.y * 0.3f;
        }

        #endregion       


        #region Click

        public void OnPointerClick(PointerEventData eventData)
        {
            RandomMove(width, height);
        }

        #endregion
    }
}