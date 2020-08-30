
using UnityEngine;

namespace SA.TestGame.Data
{
    [CreateAssetMenu(fileName = "DataGame", menuName = "Data/DataGame")]
    public class DataGame : ScriptableObject
    {
        #region Properties

        public GameObject[] StagePrefabs => stagePrefabs;

        #endregion


        #region Var

        [SerializeField] GameObject[] stagePrefabs;

        #endregion
    }
}