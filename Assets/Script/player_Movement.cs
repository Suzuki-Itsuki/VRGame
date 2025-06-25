using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class player_Movement : MonoBehaviour
{
    private Vector2 input;
    private Vector3 move;
    private Vector3 look_body;
    //private Quaternion lookHMD_head, lookHMD_body;
    public Transform VRdevise, CenterEyes, head_figure, body_figure;
    public bool getHMD = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        move.x = context.ReadValue<Vector2>().x;
        move.z = context.ReadValue<Vector2>().y;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look_body = context.ReadValue<Vector2>();
    }

    /*public void OnLook_HMD(InputAction.CallbackContext context)
    {
        //体
        lookHMD_body.y = context.ReadValue<Quaternion>().y;
        //顔
        //lookHMD_head.x = context.ReadValue<Quaternion>().x;
        lookHMD_head.z = context.ReadValue<Quaternion>().z;
    }*/

    public void Start()
    {
        VRdevise = transform.parent.Find("OVRCameraRig");
        CenterEyes = VRdevise.Find("TrackingSpace/CenterEyeAnchor");
        head_figure = transform.Find("Cube_head");  
        body_figure = transform;
        if (XRSettings.isDeviceActive)
        {
            getHMD = true;
            VRdevise.position = head_figure.position;
            VRdevise.rotation = head_figure.rotation;
        }

    }

    private void Update()
    {
        {/*// 左スティックの入力をVector2として取得
        Vector2 moveInput = leftStickAction.action.ReadValue<Vector2>();

        // 移動方向を計算（ワールド空間のXZ平面に限定）
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // 移動量を計算
        Vector3 moveVelocity = moveDirection * moveSpeed * Time.deltaTime;

        // CharacterControllerを使用して移動
        characterController.Move(moveVelocity);*/
        }
        /*手による入力制御(移動関連)*/{
            //移動の制御_手による入力
            const float Speed = 1f;
            if (input.magnitude < 0.01f) // magnitudeはベクトルの大きさ。ほぼゼロに近い値で判定
            {
                Debug.Log("スティック入力なし");
                // ここでスティックが離された際の処理を記述
                move = Vector3.zero; // 移動ベクトルをリセットするなどの処理
            }
            body_figure.Translate(move * Speed * Time.deltaTime);
            //回転の制御_手による入力
            const float rotationSpeed = 1f; // 回転速度
            float yaw = look_body.x * rotationSpeed;
            float pitch = look_body.y * -1f;
            //顔の動き制御_手による入力
            /*if (getHMD)
            {
                body_figure.Rotate(Vector3.up, yaw * Speed * Time.deltaTime);
                CenterEyes.Rotate(Vector3.right, pitch * Speed * Time.deltaTime);
            }
            else*/
            {
                body_figure.Rotate(Vector3.up, yaw);
                CenterEyes.Rotate(Vector3.right, pitch);
            }
        }
       
        /*HMDによる入力制御(移動関連)*/{
            if (getHMD)//顔の動きを検出してオブジェクトに適応しようとしたが以下に最適解アリのため無しも可
            {
                //カメラオブジェクトの動きをそのままfigure着きObjに参照する
                head_figure.rotation = CenterEyes.rotation;//顔の向き
                //head_figure.position = VRdevise.position;//顔の位置

                body_figure.rotation = Quaternion.Euler(0, CenterEyes.rotation.eulerAngles.y, 0); ;//体の向き
                                                                                                   //body_figure.position = new Vector3(VRdevise.position.x, body_figure.position.y, VRdevise.position.z);//体の移動

                /*カメラObjの制御*/
                {
                    //VRdevise;
                    VRdevise.position = new Vector3(body_figure.position.x, head_figure.position.y, body_figure.position.z);
                    CenterEyes.rotation = Quaternion.Euler(CenterEyes.rotation.eulerAngles.x, body_figure.rotation.eulerAngles.y, CenterEyes.rotation.eulerAngles.z);
                }
            }
        }

        

    }
}
