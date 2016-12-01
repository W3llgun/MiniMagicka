using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDisplay : MonoBehaviour {
	public Text elementName;
	public Image elementImage;
	public Slider lifeSlider;

	private void Awake()
	{
		lifeSlider = GetComponentInChildren<Slider>();
		lifeSlider.value = 1;
		GameObject currentElem = transform.Find("CurrentElement").gameObject;
		elementName = currentElem.GetComponentInChildren<Text>();
		elementImage = currentElem.GetComponentInChildren<Image>();
	}

	public void updateElement(Element elem, Sprite sprt)
	{
		if(elem == null)
		{
			elementName.text = "";
			elementImage.sprite = null;
			elementImage.enabled = false;
		}
		else
		{
			elementName.text = "" + System.Enum.GetName(typeof(elementType), elem.type);
			elementImage.enabled = true;
			if(sprt)
				elementImage.sprite = sprt;
			else
				elementImage.sprite = null;
		}
	}

	public void updateLife(float percentLife)
	{
		lifeSlider.value = percentLife;
	}
}
