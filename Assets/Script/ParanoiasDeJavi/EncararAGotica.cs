using System.Collections;
using UnityEngine;

public class EncararAGotica : MonoBehaviour
{
    public GameObject coll;
    public GameObject boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Overworld"))
        {
            PlayerMov playerMov = gameObject.GetComponent<PlayerMov>();
            playerMov.speed = 0;
            StartCoroutine(activarBoss());
        }
    }
    IEnumerator activarBoss()
    {
        yield return new WaitForSeconds(4);
        coll.SetActive(false);
        boss.SetActive(true);
    }
}
