using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instances;
    public AudioSource kazoo;


    public AudioClip Ataque;
    public AudioClip Dano;
    public AudioClip Hablar;

    private AudioSource audioSource;

    public float pitch;
    private void Awake()
    {
        if (Instances == null)
        {
            Instances = this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void PLayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void CrossfadeAudio(AudioSource fromAudio, AudioSource toAudio, float fadeOutSpeed, float fadeInSpeed)
    {
        StartCoroutine(FadeOutIn(fromAudio, toAudio, fadeOutSpeed, fadeInSpeed));
    }

    private IEnumerator FadeOutIn(AudioSource fromAudio, AudioSource toAudio, float fadeOutSpeed, float fadeInSpeed)
    {
        float fromStartVolume = fromAudio.volume;
        float toStartVolume = toAudio.volume;

        float fromTime = 0f;
        float toTime = 0f;

        toAudio.Play(); // Asegurar que el nuevo audio comienza a reproducirse

        while (fromAudio.volume > 0 || toAudio.volume < 1)
        {
            if (fromAudio.volume > 0)
            {
                fromTime += Time.deltaTime / fadeOutSpeed;
                fromAudio.volume = Mathf.Lerp(fromStartVolume, 0, fromTime);
            }

            if (toAudio.volume < 1)
            {
                toTime += Time.deltaTime / fadeInSpeed;
                toAudio.volume = Mathf.Lerp(toStartVolume, 1, toTime);
            }

            yield return null; // Espera al siguiente frame
        }

        fromAudio.Stop(); // Detener el audio antiguo cuando ya no se escucha
    }

    public void ReproducirMusicaDeBatalla(AudioClip clip, float pitch)
    {
        kazoo.clip = clip;
        kazoo.pitch = pitch;
        kazoo.loop = true;
        kazoo.Play();
    }
}
