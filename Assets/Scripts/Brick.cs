using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform coinSpawnPoint;
    public bool spawnsCoin = false;
    public AudioClip coinSound;
    private AudioSource audioSource;
    
    private Vector3 originalPosition;
    private bool isBouncing = false;

    void Start()
    {
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].normal.y > 0)
        {
            if (!isBouncing)
                StartCoroutine(Bounce());
        }
    }

    IEnumerator Bounce()
    {
        isBouncing = true;
        Vector3 peakPos = originalPosition + Vector3.up * 0.2f;

        // Move up
        float elapsedTime = 0f;
        float duration = 0.1f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(originalPosition, peakPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move back down
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(peakPos, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (spawnsCoin)
        {
            GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);
            audioSource.PlayOneShot(coinSound);
            Destroy(coin, 0.5f);
        }

        isBouncing = false;
    }
}
