using System.Collections;
using UnityEngine;

public class DeadAntSpawner : MonoBehaviour
{
    public GameObject deadAntPrefab; // Spawn edilecek prefab
    public float spawnInterval = 5f; // Spawn aralığı (saniye cinsinden)
    public Transform spawnPoint; // Spawn noktası
    public float lifeTime = 10f; // Prefab'ın yaşam süresi (saniye cinsinden)

    private void Start()
    {
        StartCoroutine(SpawnAnts()); // Coroutine başlat
    }

    private IEnumerator SpawnAnts()
    {
        while (true)
        {
            SpawnAnt();
            yield return new WaitForSeconds(spawnInterval); // Belirli aralıklarla bekle
        }
    }

    private void SpawnAnt()
    {
        if (deadAntPrefab != null && spawnPoint != null)
        {
            GameObject spawnedAnt = Instantiate(deadAntPrefab, spawnPoint.position, spawnPoint.rotation); // Prefabı spawn et
            Destroy(spawnedAnt, lifeTime); // Spawn edilen prefab'ı 10 saniye sonra yok et
        }
        else
        {
            Debug.LogWarning("DeadAntPrefab veya SpawnPoint eksik!"); // Hata mesajı
        }
    }
}
