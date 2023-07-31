using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBase.Character
{
	[RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rigid;//玩家刚体
        //private Animator animator;//玩家动画机
        public Transform mCamera;//相机组件

        private Vector3 moveDir;//人物移动方向
        private float currentVelocity = 1;//目前的转向速度（SmoothDampAngle函数的返还参数）
        private float smoothTime = 0.1f;//完成平滑的时间（SmoothDampAngle函数的参数）
        public float wSpeed;//移动速度
        public float rSpeed;//旋转速度
        public float jPower;//跳跃力度
        private float inputH;//水平移动参数
        private float inputV;//垂直移动参数
        private bool isMove;//是否移动
        private bool isRun;//是否奔跑
        private bool isGround;//是否在地面上

        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            //animator = GetComponent<Animator>();
        }

        void Update()
        {
        }

        private void FixedUpdate()
        {
            inputH = Input.GetAxis("Horizontal");//获取水平/垂直移动参数
            inputV = Input.GetAxis("Vertical");
            //animator.SetFloat("inputH", inputH);//更新动画机参数
            //animator.SetFloat("inputV", Mathf.Abs(inputV));
            //animator.SetBool("isMove", isMove);
            //animator.SetBool("isRun", isRun);
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    isGround = false;
            //    //animator.CrossFade("Jump", 0.1f);
            //    rigid.AddForce(0, jPower, 0);//给刚体向上的力
            //}//更新跳跃动画
            if (inputH != 0 || inputV != 0)
            {
                isMove = true;
                Vector3 euler = mCamera.eulerAngles;
                euler.x = 0;
                transform.rotation = Quaternion.Euler(euler);
            }//改变人物朝向
            else
            {
                isMove = false;
            }//更新移动状态
            if (Input.GetKey(KeyCode.LeftShift))
            {
                wSpeed = 6;
                isRun = true;
            }//更新奔跑状态
            else
            {
                wSpeed = 3;
                isRun = false;
            }
            moveDir = mCamera.TransformDirection(inputH, 0, inputV);
            rigid.MovePosition(transform.position + new Vector3(moveDir.x, 0, moveDir.z) * wSpeed * Time.fixedDeltaTime);//移动人物
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (!isGround)
        //    {
        //        if (collision.gameObject.CompareTag("Ground"))
        //        {
        //            isGround = true;
        //        }
        //    }//判断是否接地
        //}
    }
}

