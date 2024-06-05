using UnityEngine;

public class KidSafePlace : MonoBehaviour
{
    private bool isInSafePlace = false;

    public bool IsInSafePlace
    {
        get { return isInSafePlace; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SafePlace"))
        {
            isInSafePlace = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SafePlace"))
        {
            isInSafePlace = false;
        }
    }
}
