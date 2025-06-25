using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Input_Player : MonoBehaviour
{
    //操作時のそれぞれの速度変数
    const float speed_move = 1;//移動
    [System.NonSerialized]public float speed_look = 1f;//視点

    //入力された値を格納する変数
    private Vector2 input;
    private Vector3 move;
    private Vector3 look;

    //操作するオブジェクトの格納変数
    private Transform body, VRdevise;

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        move.x = context.ReadValue<Vector2>().x;
        move.z = context.ReadValue<Vector2>().y;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }


    void Start()
    {
        //このscriptがついているオブジェクトをbodyとする
        body = transform;
        VRdevise = transform.Find("OVRCameraRig");
    }

    void Update()
    {
        /*手入力*/{
            //移動_手入力
            //実行中はconst定義によってSpeedの値は変わらない
            body.Translate(move * speed_move * Time.deltaTime);

            //回転_手入力
            float yaw = look.x * speed_look;//左右の操作
            float pitch = look.y * -1f; //上下の操作

            body.Rotate(Vector3.up, yaw);
            //VRdevise.Rotate(Vector3.right, pitch);
        }

        /*HMD入力*/{
            
        }

    }
}
