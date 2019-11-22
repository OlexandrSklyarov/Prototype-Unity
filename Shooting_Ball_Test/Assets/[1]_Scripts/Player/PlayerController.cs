using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Game.ShootingBall
{
    [RequireComponent(typeof(PlayerShooting))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {        

    #region Data

        private enum StatePlayer
        {
            SHOOT, STAY, JUMP, FINISH
        }

    #endregion

        
    #region Var

        //motor
        SphereCollider SphereCollider 
        {
            get 
            {
                if (sCollider == null)
                    sCollider = gameObject.AddComponent<SphereCollider>();
                
                return sCollider;
            }
        }

        SphereCollider sCollider;       
        StatePlayer state;
        Rigidbody rb;
        const float JUMP_POWER = 500f;
        float lastTimeCheckGround;        

        //components
        PlayerShooting shootingComp;
        PlayerInput input;
        
        //target
        Transform target;
        bool isPathClear;
        
        //scale
        const float MIN_SCALE_VALUE = 1f;
        const float SUBTRACTED_SCALE_VALUE = 0.25f; 
        float currentScale;
        float lastTimeUpdateScale;

        //shooting
        Vector3 shootPoint;        
        const float TIME_SHOOT_OFFSET = 0.2f;
        const float START_BULLET_SCALE = 1f;
        bool isShoot;

    #endregion


    #region Events

        public event Action OnEmptyEnergyEvent;
        public event Action OnPlayeArriveToTargetEvent;
        public event Action<float> OnChangeScaleEvent;

    #endregion


    #region Init

        void Awake()
        {
            Init();
            InitRigidBody();
            GetTarget();

            Subscription();
        }


        void Init()
        {
            shootingComp = GetComponent<PlayerShooting>();
            input = GetComponent<PlayerInput>();
            state = StatePlayer.SHOOT;
            currentScale = transform.localScale.x;
        }


        private void GetTarget()
        {
            target = GameObject.FindObjectOfType<Target>().transform;
        }


        private void InitRigidBody()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }


        void Start()
        {
            OnChangeScaleEvent?.Invoke(currentScale);
        }

        private void Subscription()
        {
            input.OnPointerClickEvent += CreateBullet;
            input.OnPointerDownEvent += SetPowerBullet;
            input.OnPointerUpEvent += Shoot;
        }


        private void Desubscription()
        {
            input.OnPointerClickEvent -= CreateBullet;
            input.OnPointerDownEvent -= SetPowerBullet;
            input.OnPointerUpEvent -= Shoot;
        }


    #endregion


    #region Update

        void FixedUpdate()
        {
            switch(state)
            {
                case StatePlayer.STAY:

                    CheckArrival();

                break;

                case StatePlayer.JUMP:

                    CheckGraund();

                break;

            }                        
        }
        

    #endregion


    #region Shooting

        void CreateBullet(Vector3 point)
        {
            CheckedClearPath();

            if (state != StatePlayer.SHOOT || currentScale <= MIN_SCALE_VALUE || shootingComp.CurBullet != null)
                return;

            //shootPoint = point;
            shootPoint = new Vector3(target.position.x, transform.position.y ,target.position.z );
            lastTimeUpdateScale = Time.time;
            isShoot = false;

            var dist = currentScale * 0.8f; //80% размера
            var startPoint = transform.position + (shootPoint - transform.position).normalized * dist;
            shootingComp.CreateBullet(startPoint, START_BULLET_SCALE);
        }


        void SetPowerBullet()
        {
            if (state != StatePlayer.SHOOT || currentScale <= MIN_SCALE_VALUE || isShoot)
                return;           

            if (Time.time > lastTimeUpdateScale + TIME_SHOOT_OFFSET)
            {
                lastTimeUpdateScale = Time.time;

                SetScale(SUBTRACTED_SCALE_VALUE);
                shootingComp.SetBulletScale(SUBTRACTED_SCALE_VALUE);
            }
        }


        void SetScale(float scaleValue)
        {
            currentScale -= scaleValue;

            if (currentScale <= MIN_SCALE_VALUE)   
            {                
                //game over
                currentScale = MIN_SCALE_VALUE;
                shootingComp.DestroyBullet();
                GameOver();
            }                
             
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            OnChangeScaleEvent?.Invoke(currentScale);
        }


        void Shoot()
        {
            if (state == StatePlayer.SHOOT && shootingComp.CurBullet != null)
            {
                isShoot = true;
                shootPoint = Vector3.zero;                
                var dir = shootPoint - transform.position;
                shootingComp.PushBullet(dir);                
            }
        }


        private void CheckedClearPath()
        {
            if (isPathClear)            
                return;                          

            RaycastHit hit;

            var point = new Vector3(target.position.x, transform.position.y, target.position.z);
            var targetPos = point - transform.position; 

            var dir = targetPos.normalized;         
            var dist = targetPos.magnitude;
            var radius = currentScale / 2f;

            if (Physics.SphereCast(transform.position, radius, dir, out hit, dist, 1 << 8))
            {                
                Debug.Log("Enemy forward!!!");
                return;
            }    
                    
            Debug.Log("CLEAR...");
            isPathClear = true;
            state = StatePlayer.STAY;            
            
        }

    #endregion


    #region Moving to target


        void Move()
        {
            SphereCollider.isTrigger = false;
            rb.useGravity = false;

            var dir = target.position - transform.position;                        
            var speed = (dir.sqrMagnitude > 1f) ? 30f : 0.1f;
            dir.Normalize();
            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }


        void Jump()
        {
            SphereCollider.isTrigger = true;
            rb.isKinematic = false;
            var dir = (target.position - transform.position).normalized + Vector3.up; 
            rb.AddForce(dir * JUMP_POWER);

            lastTimeCheckGround = Time.time;

            state = StatePlayer.JUMP;
        }


        void CheckGraund()
        {
            // проверяем землю спустя 1 секю после прыжка
            if (Time.time > lastTimeCheckGround + 0.5f)
            {
                RaycastHit hit;
                var  dist = currentScale * 0.6f; //60% размера
                
                if (Physics.Raycast(transform.position, Vector3.down, out hit, dist))
                {
                    if (hit.collider != null)
                    {
                        rb.isKinematic = true;
                        state = StatePlayer.STAY;
                    }
                }
            }
        }


        void CheckArrival()
        {
            var curTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
            var dist = Vector3.Distance(transform.position, curTarget);
            if ( dist > 3f)
            {
                Jump();
            }
            else if (dist > 1f)
            {
                Move();
            }
            else
            {
                ArrivedToTarget();
            }  
        }


        void ArrivedToTarget()
        {            
            if (state == StatePlayer.FINISH)
                return;

            transform.position = target.position;
            state = StatePlayer.FINISH;
            Desubscription();
            OnPlayeArriveToTargetEvent?.Invoke();
        }

    #endregion


    #region LOSE

        void GameOver()
        {
            Desubscription();
            OnEmptyEnergyEvent?.Invoke();
        }

    #endregion

    }
}