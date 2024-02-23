using UnityEngine;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField]
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void FadeOut()
    {
        anim.SetTrigger("Exit");
    }

    public void FadeIn()
    {
        anim.SetTrigger("Enter");
    }

}
