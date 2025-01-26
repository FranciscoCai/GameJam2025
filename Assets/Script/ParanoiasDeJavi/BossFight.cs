using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    public int vidaJugador;
    public int vidaGotica;
    [SerializeField] private float timeUntilAttack;
    [SerializeField] private float countdownDuration;
    [SerializeField] private float reactionTime;
    private string requiredKey;
    public GameObject zButton;
    public GameObject xButton;
    private bool waitingForInput = false;
    public GameObject or;
    public Image barraGotica;
    private RectTransform imageRectG;
    public Image barrajugador;
    private RectTransform imageRectP;
    public GameObject dialogo;
    private void Awake()
    {
        StartCoroutine(StartCombat());
        or.SetActive(false);
        imageRectG = barraGotica.GetComponent<RectTransform>();
        imageRectP = barrajugador.GetComponent<RectTransform>();
        GameManager.instance.modoAtaque = true;
        dialogo.SetActive(false);
    }
    IEnumerator StartCombat()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeUntilAttack);
            yield return Countdown();

            requiredKey = (Random.value > 0.5f) ? "z" : "x";
            Debug.Log("¡Presiona la tecla: " + requiredKey.ToUpper() + "!");
            GameObject imageToActive = zButton;
            if (requiredKey == "z")
            {
                imageToActive = zButton;
                Debug.Log("z");
            }
            else if (requiredKey == "x")
            {
                imageToActive = xButton;
                Debug.Log("x");
            }
            Image ImageToFade = imageToActive.GetComponent<Image>();
            imageToActive.SetActive(true);
            Color color = ImageToFade.color;
            color.a = 1f;
            ImageToFade.color = color;
            
            waitingForInput = true;
            float timer = 0f;
            bool isEnter = false;
            
            while (timer < reactionTime)
            {
                bool cooldown = true;
                if (Input.anyKeyDown && cooldown)
                {
                    if (Input.GetKeyDown(requiredKey) && !isEnter)
                    {
                        isEnter = true;
                        Debug.Log("✅ ¡Correcto!");
                        vidaGotica--;
                        float newWidth = imageRectG.sizeDelta.x - 30;
                        newWidth = Mathf.Max(newWidth, 0);
                        imageRectG.sizeDelta = new Vector2(newWidth, imageRectG.sizeDelta.y);
                        waitingForInput = false;
                        yield return null;
                    }
                    else
                    {
                        isEnter = true;
                        Debug.Log("❌ Fallaste");
                        float newWidth = imageRectP.sizeDelta.x - 30;
                        newWidth = Mathf.Max(newWidth, 0);
                        imageRectP.sizeDelta = new Vector2(newWidth, imageRectP.sizeDelta.y);
                        vidaJugador--;
                    }
                }
                timer += Time.deltaTime;
                yield return null;
            }

            if (waitingForInput)
            {
                Debug.Log("❌ Fallaste");
                float newWidth = imageRectP.sizeDelta.x - 30;
                newWidth = Mathf.Max(newWidth, 0);
                imageRectP.sizeDelta = new Vector2(newWidth, imageRectP.sizeDelta.y);
                vidaJugador--;
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

    private void Update()
    {
        if (vidaGotica == 0)
        {
            SceneManager.LoadScene("EscenaFrancisco 3");
        } else if (vidaJugador == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
