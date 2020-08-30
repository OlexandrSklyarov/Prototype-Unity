using UnityEngine;

namespace SA.TestGame
{
    public class ClickCube : BaseClickItem
    {
        #region Var

        [SerializeField] [Range(0.1f, 5f)] protected float verticalLimit = 2f;
        [SerializeField] [Range(0.1f, 3f)] protected float horizontallLimit = 1.5f;

        #endregion


        #region Click

        protected void OnMouseDown()
        {
            RandomMove(horizontallLimit, verticalLimit);
        }
       
        #endregion
    }
}