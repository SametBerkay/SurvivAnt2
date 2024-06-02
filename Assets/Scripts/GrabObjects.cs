using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için eklenen kütüphane

public class GrabObjects : MonoBehaviour
{
    public Transform player; // Ana karakter referansı
    public Transform childAnt; // Yavru karınca referansı
    public Transform carryPosition; // Yavruyu taşımak için boş obje
    public float grabDistance = 2.0f; // Yavruyu taşıma mesafesi
    private bool isCarrying = false; // Yavru taşınıyor mu?
    public bool hasFood = false;
    public float destroyDelay = 0.5f;
    public SpriteRenderer foodSprite; // hasFood durumunu göstermek için sprite referansı
    private bool inSafePlace = false; // Bebek güvenli alanda mı?
    private bool kidInSafePlace = false; // Bebek güvenli alanda mı?
    public int requiredFoodCount = 5; // Gerekli food sayısı
    private int deliveredFoodCount = 0; // Teslim edilen food sayısı
    public float sceneChangeDelay = 1.0f; // Sahne değişim gecikmesi
    public int nextSceneIndex; // Geçilecek sahnenin numarası
    public string nextLevelTag = "NextLevel"; // Geçiş yapılacak alanın tag'i

    void Start()
    {
        // Null kontrolleri
        if (player == null) Debug.LogError("Player referansı ayarlanmadı.");
        if (childAnt == null) Debug.LogError("Child Ant referansı ayarlanmadı.");
        if (carryPosition == null) Debug.LogError("Carry Position referansı ayarlanmadı.");
        if (foodSprite == null) Debug.LogError("Food Sprite referansı ayarlanmadı.");
    }

    void Update()
    {
        // Null kontrolü
        if (player == null || childAnt == null || carryPosition == null || foodSprite == null) return;

        float distance = Vector3.Distance(player.position, childAnt.position);

        // Yavruya belirli bir mesafede ve "E" tuşuna basılmışsa
        if (distance <= grabDistance && Input.GetKeyDown(KeyCode.E) && !hasFood)
        {
            if (isCarrying)
            {
                DropChild();
            }
            else
            {
                CarryChild();
            }
        }

        // Yavru taşınıyorsa, onu taşıma pozisyonuna hareket ettir
        if (isCarrying)
        {
            childAnt.position = carryPosition.position;
        }

        // Sprite görünürlüğünü hasFood durumuna göre ayarla
        foodSprite.enabled = hasFood;
    }

    void CarryChild()
    {
        if (hasFood) return; // Eğer hasFood true ise, yavruyu taşıma

        isCarrying = true;
        childAnt.SetParent(carryPosition);
        childAnt.localPosition = Vector3.zero; // Taşıma pozisyonuna yerleştir
    }

    void DropChild()
    {
        isCarrying = false;
        childAnt.SetParent(null); // Yavruyu istediği yerde bırak
        childAnt.position = new Vector3(childAnt.position.x, childAnt.position.y + 0.5f, childAnt.position.z); // 0.5 birim yukarıda bırak

        if (inSafePlace)
        {
            kidInSafePlace = true; // Yavruyu güvenli alanda bıraktık
        }
        else
        {
            kidInSafePlace = false; // Yavruyu güvenli alanda bırakmadık
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food") && !hasFood && kidInSafePlace)
        {
            Debug.Log("Ezi çiyyyeee");
            hasFood = true;
            Destroy(other.gameObject, destroyDelay);
        }
        else if (other.CompareTag("kid") && hasFood && kidInSafePlace)
        {
            Debug.Log("HAt KERWANEEE");
            hasFood = false;
            deliveredFoodCount++; // Teslim edilen food sayısını artır

            // Gerekli food sayısına ulaşıldıysa sahne geçişi yap
            if (deliveredFoodCount >= requiredFoodCount)
            {
                Invoke("ChangeSceneWithDelay", sceneChangeDelay); // Sahne değişim gecikmesi ile sahne geçişi
            }
        }
        else if (other.CompareTag("SafePlace"))
        {
            inSafePlace = true;
        }
        else if (other.CompareTag(nextLevelTag) && isCarrying)
        {
            Invoke("ChangeSceneImmediate", sceneChangeDelay); // NextLevel tag'i ile sahne geçişi
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SafePlace"))
        {
            inSafePlace = false;
        }
    }

    void ChangeSceneWithDelay()
    {
        SceneManager.LoadScene(nextSceneIndex); // Sahne numarasına göre sahne değişimi
    }

    void ChangeSceneImmediate()
    {
        SceneManager.LoadScene(3); // Sahne numarasına göre sahne değişimi
    }
}
