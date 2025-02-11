using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLoop : MonoBehaviour
{
    public AudioSource audioSourceA;
    public AudioSource audioSourceB;
    public float crossfadeDuration = 2.0f; // �t�F�[�h����

    void Start()
    {
        // �ŏ��̉������Đ�
        audioSourceA.Play();
        StartCoroutine(LoopWithCrossfade());
    }

    IEnumerator LoopWithCrossfade()
    {
        while (true)
        {
            yield return new WaitForSeconds(audioSourceA.clip.length - crossfadeDuration);

            // ���̉������t�F�[�h�C�����Ȃ���Đ�
            StartCoroutine(FadeIn(audioSourceB, crossfadeDuration));
            yield return new WaitForSeconds(crossfadeDuration);

            // A�̉������~�߂�
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
