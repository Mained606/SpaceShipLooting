using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class smallroom_DoorOepen : XRSimpleInteractableOutline 
{
    private Animator anim;

    void Start()
    {

        anim = GetComponent<Animator>();
        if (anim == null )
        {
             anim = GetComponentInChildren<Animator>();
        }
        // Animator를 자식에서 먼저 가져옴
        anim = GetComponentInChildren<Animator>();

        // 자식에 없으면 부모에서 Animator를 가져옴
        if (anim == null)
        {
            anim = GetComponentInParent<Animator>();
        }

        // 그래도 Animator가 없으면 경고
        if (anim == null)
        {
            Debug.LogError("Animator component is missing on this object or its hierarchy!");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        anim.SetTrigger("Open");
    }
}
