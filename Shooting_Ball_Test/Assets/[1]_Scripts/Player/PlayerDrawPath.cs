using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Game.ShootingBall
{
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerDrawPath : MonoBehaviour
    {
    
    #region Var

        DataPlayer data;

        LineRenderer line;
        const float HIGHT_Y = 0.5f;

    #endregion


    #region Init

        void Awake()
        {
            data = Resources.Load<DataPlayer>(Prm.Path.PLAYER_DATA);

            InitLineRenderer();
            Subscription();
        }


        private void Subscription()
        {
            GetComponent<PlayerController>().OnChangeScaleEvent += SetWidthLine;
        }


        void InitLineRenderer()
        {
            line = GetComponent<LineRenderer>();
            SetMaterial(data.lineRendererMaterial);
            
            var startPos = transform.position;
            startPos.y = HIGHT_Y;

            var endPos = GameObject.FindObjectOfType<Target>().transform.position;
            endPos.y = HIGHT_Y;

            line.positionCount = 2;

            line.SetPosition(0, startPos);
            line.SetPosition(1, endPos);
        }

    #endregion


    #region Update draw line

        void SetWidthLine(float width)
        {
            line.startWidth = width;
            line.endWidth = width;
        }        

        
        void SetMaterial(Material material)
        {
            line.material = material;
        }

    #endregion

    }
}