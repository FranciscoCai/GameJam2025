using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    public UnityEvent<GameObject> OnEnter;
    private bool HasEnter = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterController player) && !HasEnter)
        {
            OnEnter.Invoke(player.gameObject);
        }
    }
}
