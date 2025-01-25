using System.Collections;
using UnityEngine;

public class GiroComienzo : MonoBehaviour
{
    [SerializeField] private Acercamiento acercamiento;
    [SerializeField] private PlayerMov playerMov;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject barraVida;
    [SerializeField] private GameObject play;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EmpezarCorutina()
    {
        StartCoroutine(RotarGradualmente(-90));
    }
    private IEnumerator RotarGradualmente(float angulo)
    {
        float anguloInicial = transform.eulerAngles.y;
        float anguloFinal = anguloInicial + angulo;
        float tiempoDeRotacion = 1f; // Tiempo que tarda en rotar

        float tiempoTranscurrido = 0;
        play.SetActive(false);
        while (tiempoTranscurrido < tiempoDeRotacion)
        {
            float anguloActual = Mathf.Lerp(anguloInicial, anguloFinal, tiempoTranscurrido / tiempoDeRotacion);
            transform.rotation = Quaternion.Euler(0, anguloActual, 0);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, anguloFinal, 0); // Asegura que llegue al ¨¢ngulo final
        ActivarElementos();
    }
    private void ActivarElementos()
    {
        characterController.enabled = true;
        playerMov.enabled = true;
        acercamiento.enabled = true;
        barraVida.SetActive(true);
    }
}
