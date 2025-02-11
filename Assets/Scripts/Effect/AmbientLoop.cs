using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLoop : MonoBehaviour
{
    public AudioSource audioSourceA;
    public AudioSource audioSourceB;
    public float crossfadeDuration = 2.0f; // フェード時間

    void Start()
    {
        // 最初の音源を再生
        audioSourceA.Play();
        StartCoroutine(LoopWithCrossfade());
    }

    IEnumerator LoopWithCrossfade()
    {
        while (true)
        {
            yield return new WaitForSeconds(audioSourceA.clip.length - crossfadeDuration);

            // 次の音源をフェードインしながら再生
            StartCoroutine(FadeIn(audioSourceB, crossfadeDuration));
            yield return new WaitForSeconds(crossfadeDuration);

            // Aの音源を止める
            audioSourceA.Stop();
            SwapSources();
        }
    }

    IEnumerator FadeIn(AudioSource source, float duration)
    {
        source.volume = 0;
        source.Play();
        float elapsed = 0;

        while (elapsed < duration)
        {
            source.volume = Mathf.Lerp(0, 1, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        source.volume = 1;
    }

    void SwapSources()
    {
        AudioSource temp = audioSourceA;
        audioSourceA = audioSourceB;
        audioSourceB = temp;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
