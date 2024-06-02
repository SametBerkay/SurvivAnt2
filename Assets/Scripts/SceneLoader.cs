using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int sceneIndex; // Yüklenecek sahnenin index'i

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Player veya kid tag'ine sahip bir nesne alana girdiğinde
        if (other.CompareTag("Player") && other.CompareTag("kid"))
        {
            // Yeni sahneyi yükle
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("Change");
        }
    }
}
