using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightSaber : XRGrabInteractableOutline
{
    [SerializeField] private GameObject blade;
    [SerializeField] private bool isActive = false;
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("라이트 세이버에 리지드 바디가 없습니다.");
        }
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        blade.SetActive(false);
        base.OnSelectExiting(args);

    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        isActive = !isActive;

        //블레이드 셋 엑티브 SFX 추가 
        if (isActive)
        {
            // 소드 On
            AudioManager.Instance.Play("SwordOn", false, 1.4f, 0.7f);
        }
        else
        {
            // 소드 Off
            AudioManager.Instance.Play("SwordOn", false, 3f, 0.7f);
        }

        blade.SetActive(isActive);
    }
}
