using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Combate : MonoBehaviour
{
    public float timeUntilAttack;
    public float countdownDuration;
    public float reactionTime;

    private string requiredKey;
    private bool waitingForInput = false;

    public Image image;
    public float fadeSpeed;
    public Camera playerCam;
    public GameObject battleCam;

    void Start()
    {
        //GameManager.instance.modoAtaque = true;
        StartCoroutine(StartCombat());
    }

    IEnumerator StartCombat()
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeUntilAttack);
            yield return Countdown();

            requiredKey = (Random.value > 0.5f) ? "z" : "x";
            Debug.Log("¡Presiona la tecla: " + requiredKey.ToUpper() + "!");

            waitingForInput = true;
            float timer = 0f;

            while (timer < reactionTime)
            {
                if (Input.GetKeyDown(requiredKey))
                {
                    Debug.Log("✅ ¡Correcto!");
                    waitingForInput = false;
                    yield return Return();
                    yield break;
                }
                timer += Time.deltaTime;
                yield return null;
            }

            if (waitingForInput)
            {
                Debug.Log("❌ Fallaste");
            }

            waitingForInput = false;
        }
    }

    IEnumerator Countdown()
    {
        float timer = countdownDuration;
        while (timer > 0)
        {
            Debug.Log("Cuenta atrás: " + Mathf.Ceil(timer));
            timer -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("¡GO!");
    }

    IEnumerator Return()
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (color.a < 1f) // Cambié `<=` por `<` para evitar loops infinitos
        {
            color.a += fadeSpeed * Time.deltaTime;

            if (color.a > 1f) // Asegurar que no pase de 1
                color.a = 1f;

            image.color = color;
            yield return null;
        }
        color.a = 1f;
        image.color = color;

        if (playerCam != null)
        {
            playerCam.enabled = true; ;
        }

        while (image.color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;

        if (battleCam != null)
        {
            battleCam.SetActive(false);
        }

        GameManager.instance.modoAtaque = false;
    }
}

