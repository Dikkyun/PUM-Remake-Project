using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    public Button toggleButton; // Assign your button in the Inspector
    public Image imageToToggle; // Assign your image in the Inspector

    private bool isImageVisible;

    void Start()
    {
        // Ensure the button and image are assigned
        if (toggleButton != null && imageToToggle != null)
        {
            // Set the initial visibility based on the image's active state
            isImageVisible = imageToToggle.gameObject.activeSelf;

            // Add a listener to the button
            toggleButton.onClick.AddListener(ToggleImageVisibility);
        }
        else
        {
            Debug.LogError("Button or Image not assigned.");
        }
    }

    void ToggleImageVisibility()
    {
        isImageVisible = !isImageVisible;
        imageToToggle.gameObject.SetActive(isImageVisible);
    }
}
