using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.TryGetComponent<Icollectible>(out var collectible)){
            collectible.collect();
        }
        
    }
}
