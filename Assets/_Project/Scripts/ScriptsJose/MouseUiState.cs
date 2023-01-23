using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUiState : MonoBehaviour
{
    [SerializeField] Animator animatorObject;

    public void MouseOn()
    {
        animatorObject.SetBool("MouseOn", true);
        animatorObject.SetBool("MouseOut", false);
    }

    public void MouseOut()
    {
        animatorObject.SetBool("MouseOut", true);
        animatorObject.SetBool("MouseOn", false);
    }
}
