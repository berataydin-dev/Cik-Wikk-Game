using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag(Consts.WheatTypes.GOLD_WHEAT)){
           other.gameObject?.GetComponent<GoldWheatCollectible>().collect();
        }
         if(other.CompareTag(Consts.WheatTypes.HOLY_WHEAT)){
             other.gameObject?.GetComponent<HolyWheatCollectible>().collect();
        }
         if(other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT)){
             other.gameObject?.GetComponent<RottenWheatCollectible>().collect();
        }
    }
}
