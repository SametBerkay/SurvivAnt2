using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpriteOnPlayerEnter : MonoBehaviour
{
    // Sprite'ı temsil eden GameObject referansı
    public GameObject spriteObject;

    private void OnTriggerEnter2D(Collider2D other) {
        // Trigger alanına "Player" etiketine sahip bir nesne (player) girdiğinde
        if (other.CompareTag("Player")) {
            // spriteObject'i etkinleştir
            spriteObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Trigger alanından "Player" etiketine sahip bir nesne (player) çıktığında
        if (other.CompareTag("Player")) {
            // spriteObject'i devre dışı bırak
            spriteObject.SetActive(false);
        }
    }
}
