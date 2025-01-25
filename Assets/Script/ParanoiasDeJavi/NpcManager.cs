using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public static NpcManager Instance;
    public float timeUntilAttack;
    public float countdownDuration;
    public float pumpDuration;
    public string[] textoCompleto;
    public Sprite orSprite;
    public Sprite newSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
