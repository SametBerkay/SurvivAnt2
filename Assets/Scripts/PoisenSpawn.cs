using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpawn : MonoBehaviour
{
    public GameObject poisonPrefab; // Zehir damlası prefab
    public Transform spawnPoint; // Zehir damlasının çıkış noktası
    public float initialSpawnDelay = 1f; // İlk zehir damlasının oluşma gecikmesi
    public float spawnInterval = 2f; // Zehir damlasının oluşma aralığı

    private void Start()
    {
        // İlk spawn için belirli bir gecikmeyle zehir damlası oluştur
        StartCoroutine(SpawnPoison(initialSpawnDelay));
    }

    IEnumerator SpawnPoison(float delay)
    {
        // İlk spawn için bekle
        yield return new WaitForSeconds(delay);
        
        // İlk zehir damlasını spawn et
        Instantiate(poisonPrefab, spawnPoint.position, Quaternion.identity);

        // Döngüsel olarak zehir damlası spawn etmeye devam et
        while (true)
        {
            // Zehir damlasını spawn et
            Instantiate(poisonPrefab, spawnPoint.position, Quaternion.identity);

            // Bir sonraki spawn'a kadar bekle
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
