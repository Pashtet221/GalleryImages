using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FullScreenImage : MonoBehaviour
{
    public Image fullScreenImage;
    public Button backButton;

    public void ShowFullScreenImage(Sprite sprite)
    {
        fullScreenImage.sprite = sprite;
        gameObject.SetActive(true);
    }
}
