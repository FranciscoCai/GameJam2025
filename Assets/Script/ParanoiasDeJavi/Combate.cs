using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Combate : MonoBehaviour
{
    public static Combate Instance;
    [SerializeField] private float timeUntilAttack;
    [SerializeField] private float countdownDuration;
    [SerializeField] private float reactionTime;

    private string requiredKey;
    private bool waitingForInput = false;

    public Image image;
    public float fadeSpeed;
    public Camera playerCam;
    public GameObject battleCam;

    public DialogoCombate dialogoCombate;

    public AudioSource overworld;
    public AudioSource theme;
    public AudioSource sound;

    public GameObject zButton;
    public GameObject xButton;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        } 
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        timeUntilAttack = NpcManager.Instance.timeUntilAttack;
        countdownDuration = NpcManager.Instance.countdownDuration;
        reactionTime = NpcManager.Instance.pumpDuration;
        //theme = NpcManager.Instance.sountrack;
        StartCoroutine(StartCombat());
    }
    IEnumerator StartCombat()
    {
        GameManager.instance.modoAtaque = true;
        AudioManager.Instances.CrossfadeAudio(overworld, theme, 1.5f, 1.5f);
        dialogoCombate.bocadillo.SetActive(true);

        if(AudioManager.Instances.Ataque != null )
        {
            AudioManager.Instances.PLayAudio(AudioManager.Instances.Ataque);
            AudioManager.Instances.Ataque = null;
        }

        StartCoroutine(dialogoCombate.MostrarTextoPocoAPoco());
        while (true) 
        {
            yield return new WaitForSeconds(timeUntilAttack);
            yield return Countdown();

            requiredKey = (Random.value > 0.5f) ? "z" : "x";
            Debug.Log("¡Presiona la tecla: " + requiredKey.ToUpper() + "!");
            GameObject imageToActive = zButton;
            if(requiredKey == "z")
            {
                imageToActive = zButton;
                Debug.Log("z");
            }
            else if(requiredKey == "x")
            {
                imageToActive = xButton;
                Debug.Log("x");
            }
            Image imageToFade = imageToActive.GetComponent<Image>();
            imageToActive.SetActive(true);
            Color color = imageToFade.color;
            color.a = 1.0f;
            imageToFade.color = color;

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
                        AudioManager.Instances.CrossfadeAudio(theme, overworld, 1.5f, 1.5f);
                        Debug.Log("✅ ¡Correcto!");
                        waitingForInput = false;
                        yield return new WaitForSeconds(0.7f);
                        yield return Return();
                        yield break;
                    }
                    else
                    {
                        isEnter = true;
                        Debug.Log("❌ Fallaste");
                    }
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
        AudioManager.Instances.CrossfadeAudio(theme, overworld, 1.5f, 1.5f);
        Color color = image.color;
        color.a = 0f;
        image.color = color;
        dialogoCombate.textoUI.text = "";
        dialogoCombate.bocadillo.SetActive(false);
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
        if (GameManager.instance.modoAtaque1 != null)
        {
            GameManager.instance.modoAtaque1();
        }
        GameManager.instance.modoAtaque = false;
    }
}

