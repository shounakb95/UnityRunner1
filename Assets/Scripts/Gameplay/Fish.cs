using UnityEngine;

public class Fish : MonoBehaviour
{

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            PickupFish();
        }
    }

    private void PickupFish()
    {
        anim?.SetTrigger("Pickup");
        //increment fish count
        GameStat.instance.CollectFish();
        //increment the score
        //play sfx
        //trigger an animation
    }

    public void OnShowChunk()
    {
        anim?.SetTrigger("Idle");
    }

}
