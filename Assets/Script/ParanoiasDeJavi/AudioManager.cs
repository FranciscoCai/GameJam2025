using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instances;

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

    public void playAudio(AudioSource clip)
    {
        clip.Play();
    }
}
