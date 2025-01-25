using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaCasa : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
