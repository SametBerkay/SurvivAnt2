using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSceneChanger : MonoBehaviour
{
    public Button yourButton; // Sahnedeki buton
    public int sceneIndex;    // Geçilecek sahnenin index'i

    void Start()
    {
        // Başlangıçta butonu devre dışı bırak
        yourButton.interactable = false;

        // 5 saniye sonra butonu etkinleştir
        Invoke("ActivateButton", 3f);

        // Butonun onClick eventine ChangeScene metodunu ekle
        yourButton.onClick.AddListener(ChangeScene);
    }

    private void ActivateButton()
    {
        // Butonu etkinleştir
        yourButton.interactable = true;
    }

    private void ChangeScene()
    {
        // Belirtilen sahneye geçiş yap
        SceneManager.LoadScene(sceneIndex);
    }
}
