using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Rotate : MonoBehaviour
{
    Vector3 eulerAngles;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        eulerAngles = transform.eulerAngles; // ���[�J���ϐ��Ɋi�[
        eulerAngles.x += -90; // ���[�J���ϐ��Ɋi�[�����l���㏑��
        transform.eulerAngles = eulerAngles; // ���[�J���ϐ�����
    }
}
