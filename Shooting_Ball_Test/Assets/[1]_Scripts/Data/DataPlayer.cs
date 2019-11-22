using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Game.ShootingBall
{
    [CreateAssetMenu(menuName = "DATA/Player", fileName = "DataPlayer")]
    public class DataPlayer : ScriptableObject
    {
        public GameObject bulletPrefab;
        [Range(1f, 50f)] public float shootForce;
        public Material lineRendererMaterial;
    }
}
