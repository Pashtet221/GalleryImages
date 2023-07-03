using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScratchHandler : MonoBehaviour
{
    void Start()
    {
        var image = GetComponent<Image>();
        var imageRect = image.rectTransform.rect;
        var imageWidth = imageRect.width;
        var imageHeight = imageRect.height;

        var aspectRatio = imageWidth / imageHeight;

        var camera = Camera.main;
        var viewportWidth = camera.pixelWidth;
        var viewportHeight = camera.pixelHeight;

        var scaleFactor = Mathf.Min(viewportWidth / imageWidth, viewportHeight / imageHeight);

        var scaledWidth = imageWidth * scaleFactor;
        var scaledHeight = imageHeight * scaleFactor;

        var imageRectTransform = image.rectTransform;
        imageRectTransform.sizeDelta = new Vector2(scaledWidth, scaledHeight);
    }
}
