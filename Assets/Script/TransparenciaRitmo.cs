using UnityEngine;
using UnityEngine.UI;
public class TransparenciaRitmo : MonoBehaviour
{
    public Image imageToFade; // La referencia al componente Image
    public float fadeSpeed = 1f; // Velocidad de desvanecimiento

    void Update()
    {
        // Si el fade est¨¢ activo, reduce la transparencia
        if (imageToFade != null)
        {
            Color color = imageToFade.color;
            color.a -= fadeSpeed * Time.deltaTime;
            color.a = Mathf.Clamp01(color.a); // Limita el alfa entre 0 y 1
            imageToFade.color = color;

            // Detener el fade si el alfa llega a 0
            if (color.a <= 0)
            {
                gameObject.SetActive(false);
                Debug.Log("El desvanecimiento ha terminado.");
            }
        }
    }
    private void Start()
    {
        imageToFade = GetComponent<Image>();
    }

    // M¨¦todo para iniciar el fade manualmente
}
