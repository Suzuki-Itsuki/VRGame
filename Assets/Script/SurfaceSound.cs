using UnityEngine;

public class SurfaceSound : MonoBehaviour
{
    public AudioSource audioSource;

    void PlaySoundAtSurface(Vector3 hitPoint)
    {
        if (audioSource != null)
        {
            audioSource.transform.position = hitPoint; // ‰¹‚ÌˆÊ’u‚ğ•\–Ê‚Éİ’è
            audioSource.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            PlaySoundAtSurface(contact.point); // Õ“Ë“_‚Å‰¹‚ğÄ¶
        }
    }
}