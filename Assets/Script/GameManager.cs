using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public delegate void CambioModo();
    public CambioModo modoAtaque2;
    public CambioModo modoAtaque1;
    public bool modoAtaque;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyUp(KeyCode.Escape))
        //{
        //    modoAtaque2();
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    modoAtaque1();
        //}
    }
}
