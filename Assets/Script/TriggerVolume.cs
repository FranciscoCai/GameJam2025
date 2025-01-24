using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    public UnityEvent<GameObject> OnEnter;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterController player))
        {
            OnEnter.Invoke(player.gameObject);
        }
    }
}
