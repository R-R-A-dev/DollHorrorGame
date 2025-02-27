using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSounds : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] AudioSource waterStream;
    [SerializeField] AudioSource waterFall;
    [SerializeField] AudioSource environment;
    [SerializeField] Transform[] waterStreamPos;
    [SerializeField] Transform waterFallPos;
    [SerializeField] float distancea ;
    [SerializeField] float distanceb ;

    bool waterfallPlaying = false;
    bool waterStreamPlaying = false;
    float soundDistance = 10;

    void Start()
    {

    }

    void Update()
    {
        if (true) return;
        if (!waterStreamPlaying)
        {
            for (int i = 0; i < waterStreamPos.Length; i++)
            {
                float distance = Vector3.Distance(player.position, waterStreamPos[i].position);
                Debug.Log(distance);
                if (distance < soundDistance)
                {
                    waterStream.Play();
                    waterStreamPlaying = true;
                    break;
                }
            }
        }
        else
        {
            int allOutOfCount = 0; 
            for (int i = 0; i < waterStreamPos.Length; i++)
            {
                float distance = Vector3.Distance(player.position, waterStreamPos[i].position);
                if(soundDistance < distance)
                {
                    Debug.Log(soundDistance);
                    Debug.Log(distance);
                    allOutOfCount++; 
                    Debug.Log(allOutOfCount);
                    break;
                }
            }
            if (allOutOfCount == waterStreamPos.Length) // ‚·‚×‚Ä‚Ì `distance` ‚ª `soundDistance` ‚æ‚è‘å‚«‚¢ê‡
            {
                Debug.Log("’âŽ~");
                waterStream.Stop();
                waterStreamPlaying = false;
            }
        }

        if (!waterfallPlaying)
        {
            float distance = Vector3.Distance(player.position, waterFallPos.position);
            if (distance < soundDistance)
            {
                waterFall.Play();
                waterfallPlaying = true;
            }
        }
        else if (waterfallPlaying)
        {
            float distance = Vector3.Distance(player.position, waterFallPos.position);
            if (soundDistance < distance)
            {
                waterFall.Stop();
                waterfallPlaying = false;
            }
        }
    }

    public void PlayEnvironmentSound()
    {
        environment.Play();
    }

    public void StopEnvironmentSound()
    {
        environment.Stop();
    }
}
