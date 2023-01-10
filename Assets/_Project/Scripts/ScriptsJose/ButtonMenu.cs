using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenu : MonoBehaviour
{
    public GameObject panel;

    public void DeactivatePanelWait()
    {
        Invoke("DeactivatePanel", 1f);
    }

    private void DeactivatePanel()
    {
        panel.SetActive(false);
    }

    public void ActivatePanelWait()
    {
        Invoke("ActivatePanel", 1f);
    }

    private void ActivatePanel()
    {
        panel.SetActive(true);
    }
}
