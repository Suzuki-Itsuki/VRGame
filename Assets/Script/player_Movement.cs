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
        //��
        lookHMD_body.y = context.ReadValue<Quaternion>().y;
        //��
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
        {/*// ���X�e�B�b�N�̓��͂�Vector2�Ƃ��Ď擾
        Vector2 moveInput = leftStickAction.action.ReadValue<Vector2>();

        // �ړ��������v�Z�i���[���h��Ԃ�XZ���ʂɌ���j
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // �ړ��ʂ��v�Z
        Vector3 moveVelocity = moveDirection * moveSpeed * Time.deltaTime;

        // CharacterController���g�p���Ĉړ�
        characterController.Move(moveVelocity);*/
        }
        /*��ɂ����͐���(�ړ��֘A)*/{
            //�ړ��̐���_��ɂ�����
            const float Speed = 1f;
            if (input.magnitude < 0.01f) // magnitude�̓x�N�g���̑傫���B�قڃ[���ɋ߂��l�Ŕ���
            {
                Debug.Log("�X�e�B�b�N���͂Ȃ�");
                // �����ŃX�e�B�b�N�������ꂽ�ۂ̏������L�q
                move = Vector3.zero; // �ړ��x�N�g�������Z�b�g����Ȃǂ̏���
            }
            body_figure.Translate(move * Speed * Time.deltaTime);
            //��]�̐���_��ɂ�����
            const float rotationSpeed = 1f; // ��]���x
            float yaw = look_body.x * rotationSpeed;
            float pitch = look_body.y * -1f;
            //��̓�������_��ɂ�����
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
       
        /*HMD�ɂ����͐���(�ړ��֘A)*/{
            if (getHMD)//��̓��������o���ăI�u�W�F�N�g�ɓK�����悤�Ƃ������ȉ��ɍœK���A���̂��ߖ�������
            {
                //�J�����I�u�W�F�N�g�̓��������̂܂�figure����Obj�ɎQ�Ƃ���
                head_figure.rotation = CenterEyes.rotation;//��̌���
                //head_figure.position = VRdevise.position;//��̈ʒu

                body_figure.rotation = Quaternion.Euler(0, CenterEyes.rotation.eulerAngles.y, 0); ;//�̂̌���
                                                                                                   //body_figure.position = new Vector3(VRdevise.position.x, body_figure.position.y, VRdevise.position.z);//�̂̈ړ�

                /*�J����Obj�̐���*/
                {
                    //VRdevise;
                    VRdevise.position = new Vector3(body_figure.position.x, head_figure.position.y, body_figure.position.z);
                    CenterEyes.rotation = Quaternion.Euler(CenterEyes.rotation.eulerAngles.x, body_figure.rotation.eulerAngles.y, CenterEyes.rotation.eulerAngles.z);
                }
            }
        }

        

    }
}
