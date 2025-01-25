using UnityEngine;
using TMPro;
using System.Collections;

public class DialogoCombate : MonoBehaviour
{
    public TextMeshProUGUI textoUI;  // Referencia al componente TextMeshProUGUI (para UI)
    public string[] textoCompleto;
    public float tiempoEntreLetras = 0.1f;  // Tiempo entre cada letra
    public GameObject bocadillo;


    public IEnumerator MostrarTextoPocoAPoco()
    {
        textoUI.text = "";  // Asegurarse de que el texto comience vac¨ªo
        int randomText = Random.Range(0, textoCompleto.Length);
        foreach (char letra in textoCompleto[randomText])
        {
            textoUI.text += letra;  // Agregar la letra al texto mostrado
            yield return new WaitForSeconds(tiempoEntreLetras);  // Esperar antes de agregar la siguiente letra
        }
    }
}
