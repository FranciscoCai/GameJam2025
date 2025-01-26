using System.Collections;
using TMPro;
using UnityEngine;

public class DialogoPanaderia : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    public GameObject Dialogo;
    public string[] textoCompleto;
    public float tiempoEntreLetras = 0.1f;  // Tiempo entre cada letra
    public IEnumerator DialogoPan()
    {
        Dialogo.SetActive(true);
        textoUI.text = "";  // Asegurarse de que el texto comience vac¨ªo
        for (int i = 0; i < textoCompleto.Length; i++)
        {
            foreach (char letra in textoCompleto[i])
            {
                textoUI.text += letra;  // Agregar la letra al texto mostrado
                yield return new WaitForSeconds(tiempoEntreLetras);  // Esperar antes de agregar la siguiente letra
            }
            yield return new WaitForSeconds(3f);
            textoUI.text = "";
        }
    }
    private void Start()
    {
        StartCoroutine(DialogoPan());
    }
}
