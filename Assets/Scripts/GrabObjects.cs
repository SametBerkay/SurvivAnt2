using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabObjects : MonoBehaviour
{
    public Transform player;
    public Transform childAnt;
    public Transform carryPosition;
    public float grabDistance = 2.0f;
    private bool isCarrying = false;
    public bool hasFood = false;
    public float destroyDelay = 0.5f;
    public SpriteRenderer foodSprite;
    public int requiredFoodCount = 5;
    private int deliveredFoodCount = 0;
    public float sceneChangeDelay = 1.0f;
    public int nextSceneIndex;
    public string nextLevelTag = "NextLevel";
    private KidSafePlace kidSafePlace;

    void Start()
    {
        if (player == null) Debug.LogError("Player referansı ayarlanmadı.");
        if (childAnt == null) Debug.LogError("Child Ant referansı ayarlanmadı.");
        if (carryPosition == null) Debug.LogError("Carry Position referansı ayarlanmadı.");
        if (foodSprite == null) Debug.LogError("Food Sprite referansı ayarlanmadı.");

        kidSafePlace = childAnt.GetComponent<KidSafePlace>();
        if (kidSafePlace == null) Debug.LogError("Child Ant üzerinde KidSafePlace bileşeni bulunamadı.");
    }

    void Update()
    {
        if (player == null || childAnt == null || carryPosition == null || foodSprite == null || kidSafePlace == null) return;

        float distance = Vector3.Distance(player.position, childAnt.position);

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

        if (isCarrying)
        {
            childAnt.position = carryPosition.position;
        }

        foodSprite.enabled = hasFood;
    }

    void CarryChild()
    {
        if (hasFood) return;

        isCarrying = true;
        childAnt.SetParent(carryPosition);
        childAnt.localPosition = Vector3.zero;
    }

    void DropChild()
    {
        isCarrying = false;
        childAnt.SetParent(null);
        childAnt.position = new Vector3(childAnt.position.x, childAnt.position.y + 1.0f, childAnt.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food") && !hasFood && kidSafePlace.IsInSafePlace)
        {
            Debug.Log("Food collected");
            hasFood = true;
            Destroy(other.gameObject, destroyDelay);
        }
        else if (other.CompareTag("kid") && hasFood && kidSafePlace.IsInSafePlace)
        {
            Debug.Log("Food delivered to kid");
            hasFood = false;
            deliveredFoodCount++;

            // Teslim edilen food sayısını kontrol et ve gerekli sayıya ulaşıldıysa sahne geçişi yap
            if (deliveredFoodCount >= requiredFoodCount)
            {
                Invoke("ChangeSceneWithDelay", sceneChangeDelay);
            }
        }
        else if (other.CompareTag(nextLevelTag) && isCarrying)
        {
            Invoke("ChangeSceneImmediate", sceneChangeDelay);
        }
    }

    void ChangeSceneWithDelay()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ChangeSceneImmediate()
    {
        SceneManager.LoadScene(3);
    }
}
