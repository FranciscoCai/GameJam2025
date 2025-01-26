using System.Collections;
using TMPro;
using UnityEngine;

public class GiroComienzo : MonoBehaviour
{
    [SerializeField] private Acercamiento acercamiento;
    [SerializeField] private PlayerMov playerMov;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject barraVida;
    [SerializeField] private GameObject play;
    public TextMeshProUGUI textoUI;
    public GameObject Dialogo;
    public string textoCompleto;
    public float tiempoEntreLetras = 0.1f;  // Tiempo entre cada letra
    private bool CorutinaEmpezado = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EmpezarCorutina()
    {if(CorutinaEmpezado) { return; }
    CorutinaEmpezado=true;
        StartCoroutine(DialogoMadre());
    }
    public IEnumerator DialogoMadre()
    {
        Dialogo.SetActive(true);
        textoUI.text = "";  // Asegurarse de que el texto comience vac¨ªo
        play.SetActive(false);
        foreach (char letra in textoCompleto)
        {
            textoUI.text += letra;  // Agregar la letra al texto mostrado
            yield return new WaitForSeconds(tiempoEntreLetras);  // Esperar antes de agregar la siguiente letra
        }
        StartCoroutine(RotarGradualmente(-90));
    }
    private IEnumerator RotarGradualmente(float angulo)
    {
        float anguloInicial = transform.eulerAngles.y;
        float anguloFinal = anguloInicial + angulo;
        float tiempoDeRotacion = 1f; // Tiempo que tarda en rotar

        float tiempoTranscurrido = 0;
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
        Dialogo.SetActive(false);
    }
}
