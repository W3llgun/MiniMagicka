using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReloadLevel : MonoBehaviour {

	public void showReloadLevel()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		Time.timeScale = 0;
	}

	public void reload()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
