using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReviewScene : MonoBehaviour
{
    public FullScreenImage fullScreenImagePanel;
    private int selectedItemIndex;

    private void Start()
    {
        selectedItemIndex = PlayerPrefs.GetInt("SelectedItemIndex", 0);
        fullScreenImagePanel.ShowFullScreenImage(Gallery.Instance.allGames[selectedItemIndex].Icon);
    }
}
