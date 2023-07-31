using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBase.Character
{
    public class CameraController : MonoBehaviour
    {
        public Transform body;//玩家人物组件

        public float maxY = 40;//相机旋转上界限
        public float minY = -10;//相机旋转下界限
        public float xSpeed = 250;//相机纵向旋转速度
        public float ySpeed = 125;//相机横向旋转速度
        private float mouseX = 0;//鼠标水平移动参数
        private float mouseY = 0;//鼠标垂直移动参数

        void Start()
        {

        }

        void Update()
        {
            mouseX += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;//更新鼠标水平/垂直移动参数
            mouseY -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            mouseY = clampAngle(mouseY);
            transform.position = body.position;//相机跟随人物移动
            transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);//相机旋转
        }

        private float clampAngle(float angle)
        {
            if (angle < -360)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, minY, maxY);
        }//控制相机上下旋转角度
    }
}
