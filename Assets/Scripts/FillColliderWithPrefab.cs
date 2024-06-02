using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillColliderWithPrefab : MonoBehaviour

 {
    public GameObject fillPrefab; // Collider'ı dolduracak prefab
    public float delay = 2.0f;    // Prefab'ı instantiate etmeden önceki gecikme süresi
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu collider'a girdiğinde bir şey yapma
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(InstantiateFillPrefabWithDelay(delay));
        }
    }

    private IEnumerator InstantiateFillPrefabWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        InstantiateFillPrefab();
        boxCollider.isTrigger = false; // Collider'ın trigger özelliğini devre dışı bırak
    }

    private void InstantiateFillPrefab()
    {
        GameObject fillObject = Instantiate(fillPrefab, transform.position, Quaternion.identity);
        fillObject.transform.localScale = new Vector3(boxCollider.size.x, boxCollider.size.y, 1);
        fillObject.transform.position = transform.position + (Vector3)boxCollider.offset;
    }
}
