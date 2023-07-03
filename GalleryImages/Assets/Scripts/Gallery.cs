using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public static class ButtonExtension
{
	public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
	{
		button.onClick.AddListener (delegate() {
			OnClick (param);
		});
	}
}

public class Gallery : MonoBehaviour
{
	public static Gallery Instance {get; private set;}

	[Serializable]
	public struct Game
	{
		// public string Name;
		// public string Description;
		public Sprite Icon;
		public string IconUrl;
	}

	[HideInInspector] public Game[] allGames;
	[SerializeField] Sprite defaultIcon;

    private int selectedItemIndex;


	void Start ()
	{
		Instance = this;
		StartCoroutine (GetGames ());
	}

void DrawUI()
{
    StartCoroutine(CreateButtonTemplatesWithDelay());
}

IEnumerator CreateButtonTemplatesWithDelay()
{
    GameObject buttonTemplate = transform.GetChild(0).gameObject;

    int N = allGames.Length;

    for (int i = 0; i < N; i++)
    {
        GameObject g = Instantiate(buttonTemplate, transform);
        g.transform.GetChild(0).GetComponent<Image>().sprite = allGames[i].Icon;

        g.GetComponent<Button>().AddEventListener(i, ItemClicked);

        yield return new WaitForSeconds(0.5f);
    }

    Destroy(buttonTemplate);
}



	void ItemClicked(int itemIndex)
    {
        selectedItemIndex = itemIndex;
		PlayerPrefs.SetInt("SelectedItemIndex", selectedItemIndex);
        SceneManager.LoadScene("Review");
    }


	//***************************************************
	IEnumerator GetGames ()
	{
		string url = "http://ferz2022.tmweb.ru/wp-content/uploads/2023/5/apps.json";

		UnityWebRequest request = UnityWebRequest.Get (url);
		request.chunkedTransfer = false;
		yield return request.Send ();

		if (request.isNetworkError) {

		} else {
			if (request.isDone) {
				allGames = JsonHelper.GetArray<Game> (request.downloadHandler.text);
				StartCoroutine (GetGamesIcones ());
			}
		}
	}

	IEnumerator GetGamesIcones ()
	{
		for (int i = 0; i < allGames.Length; i++) {
			WWW w = new WWW (allGames [i].IconUrl);
			yield return w;

			if (w.error != null) {

				allGames [i].Icon = defaultIcon;
			} else {
				if (w.isDone) {
					Texture2D tx = w.texture;
					allGames [i].Icon = Sprite.Create (tx, new Rect (0f, 0f, tx.width, tx.height), Vector2.zero, 10f);
				}
			}
		}

		DrawUI ();	
	}

}
