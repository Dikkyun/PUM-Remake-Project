using UnityEngine;
using UnityEngine.Playables;

public class GenericTrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    public Animator animator;

    // Use this for initialization
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }
     void Update()
    {
       // if (animator.SetBool == true)
       // {
           
     //   }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            timeline.Stop();
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            timeline.Play();
            //timeline.Stop();
            //timeline.Pause();
            // animator.SetBool("isIdle", true);//
        }

    }
}