using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{
    public Sprite emptyBoxSprite;
    public GameObject coinPrefab;
    public Transform coinSpawnPoint;
    public AudioClip coinSound;

    private bool isUsed = false;
    private Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isUsed) return;

        //Check if player hits from below
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].normal.y > 0)
        {
            StartCoroutine(SpawnCoin());
        }
    }

    IEnumerator SpawnCoin()
    {
        isUsed = true;
        animator.enabled = false;

        GetComponent<SpriteRenderer>().sprite = emptyBoxSprite;

        // Create coin above box
        GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);
        audioSource.PlayOneShot(coinSound);

        // Move coin up
        float elapsedTime = 0f;
        float duration = 0.5f;
        Vector3 startPos = coin.transform.position;
        Vector3 peakPos = startPos + Vector3.up * 1.5f;

        while (elapsedTime < duration)
        {
            coin.transform.position = Vector3.Lerp(startPos, peakPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        Vector3 endPos = startPos;

        // Move coin back down
        while (elapsedTime < duration)
        {
            coin.transform.position = Vector3.Lerp(peakPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        Destroy(coin);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
