using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Rotate : MonoBehaviour
{
    Vector3 eulerAngles;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        eulerAngles = transform.eulerAngles; // ローカル変数に格納
        eulerAngles.x += -90; // ローカル変数に格納した値を上書き
        transform.eulerAngles = eulerAngles; // ローカル変数を代入
    }
}
