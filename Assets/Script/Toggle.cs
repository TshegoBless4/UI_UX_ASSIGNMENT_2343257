using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle: MonoBehaviour
{
    public GameObject objectToToggle;

    public void ToggleObject()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
    }
}
