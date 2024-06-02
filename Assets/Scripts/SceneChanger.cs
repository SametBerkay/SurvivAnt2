using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int sceneIndex; // Geçilecek sahnenin index'i

    private void Start()
    {
        // Sahne başladıktan 12 saniye sonra ChangeScene metodunu çağır
        Invoke("ChangeScene", 12f);
    }

    private void ChangeScene()
    {
        // Belirtilen sahneye geçiş yap
        SceneManager.LoadScene(2);
    }
}
