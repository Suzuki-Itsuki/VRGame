using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Input_Player : MonoBehaviour
{
    //���쎞�̂��ꂼ��̑��x�ϐ�
    const float speed_move = 1;//�ړ�
    [System.NonSerialized]public float speed_look = 1f;//���_

    //���͂��ꂽ�l���i�[����ϐ�
    private Vector2 input;
    private Vector3 move;
    private Vector3 look;

    //���삷��I�u�W�F�N�g�̊i�[�ϐ�
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
        //����script�����Ă���I�u�W�F�N�g��body�Ƃ���
        body = transform;
        VRdevise = transform.Find("OVRCameraRig");
    }

    void Update()
    {
        /*�����*/{
            //�ړ�_�����
            //���s����const��`�ɂ����Speed�̒l�͕ς��Ȃ�
            body.Translate(move * speed_move * Time.deltaTime);

            //��]_�����
            float yaw = look.x * speed_look;//���E�̑���
            float pitch = look.y * -1f; //�㉺�̑���

            body.Rotate(Vector3.up, yaw);
            //VRdevise.Rotate(Vector3.right, pitch);
        }

        /*HMD����*/{
            
        }

    }
}
