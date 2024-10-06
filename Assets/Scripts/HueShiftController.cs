using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HueShiftController : MonoBehaviour
{
    private Material hueShiftMaterial;

    private void Start()
    {
        // Get the material from the SpriteRenderer
        hueShiftMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void ChangeHue(float hueShift)
    {
        // Set the hue shift value in the material
        hueShiftMaterial.SetFloat("_HueShift", hueShift);
    }
}
