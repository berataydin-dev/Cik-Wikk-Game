using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private PlayerController _playerConrtoller;
    private void Awake(){
        _playerConrtoller=GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.TryGetComponent<Icollectible>(out var collectible)){
            collectible.collect();
        }
        
    }
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.TryGetComponent<IBoostable>(out var boostable)){
            boostable.Boost(_playerConrtoller);
        }
    }
}
