using UnityEngine;

public class EggCollectible : MonoBehaviour,Icollectible
{
   public void collect()
    {
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}
