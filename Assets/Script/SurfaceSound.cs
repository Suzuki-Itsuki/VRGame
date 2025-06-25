using UnityEngine;

public class SurfaceSound : MonoBehaviour
{
    public AudioSource audioSource;

    void PlaySoundAtSurface(Vector3 hitPoint)
    {
        if (audioSource != null)
        {
            audioSource.transform.position = hitPoint; // ���̈ʒu��\�ʂɐݒ�
            audioSource.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            PlaySoundAtSurface(contact.point); // �Փ˓_�ŉ����Đ�
        }
    }
}