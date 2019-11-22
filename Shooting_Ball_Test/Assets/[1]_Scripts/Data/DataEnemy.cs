using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Game.ShootingBall
{
    [CreateAssetMenu(menuName = "DATA/Enemy", fileName = "DataEnemy")]
    public class DataEnemy : ScriptableObject
    {
        [Range(0.01f, 10f)] public float timeToAutodestroy;
        [Range(0.01f, 10f)] public float timeToInfectedOther;
        
    }
}