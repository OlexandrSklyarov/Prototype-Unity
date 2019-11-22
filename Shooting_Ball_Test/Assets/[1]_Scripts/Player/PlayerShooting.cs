using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Game.ShootingBall
{
    public class PlayerShooting : MonoBehaviour
    {
    
    #region Var       

        public GameObject CurBullet {get; private set;}
               
        float curBulletScale;
        bool isPush;        

        DataPlayer data;

    #endregion


    #region Init

        void Awake()
        {
            data = Resources.Load<DataPlayer>(Prm.Path.PLAYER_DATA);
        }

    #endregion


    #region Update

        public void CreateBullet(Vector3 pos, float startScale)
        {
            if (CurBullet != null)
                return;

            curBulletScale = startScale;

            CurBullet = Instantiate(data.bulletPrefab, pos, Quaternion.identity); 
            CurBullet.transform.localScale = new Vector3(curBulletScale, curBulletScale, curBulletScale);

            isPush = false;
        }


        public void SetBulletScale(float scaleValue)
        {
            if (CurBullet != null && !isPush)
            {
                curBulletScale += scaleValue;
                var newScale = new Vector3(curBulletScale, curBulletScale, curBulletScale);
                CurBullet.transform.localScale = newScale;
            }
        }


        public void PushBullet(Vector3 direction)
        {
            CurBullet.GetComponent<Rigidbody>().AddForce(direction * data.shootForce);
            CurBullet.GetComponent<Bullet>().StartPoint = CurBullet.transform.position;
            isPush = true;
        }


        public void DestroyBullet()
        {
            if (CurBullet != null)
                Destroy(CurBullet.gameObject);
        }

    #endregion

    }
}