using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickHideOrUnhideMenu : MonoBehaviour
{
	[SerializeField] GameObject menuToHide;
	[SerializeField] GameObject menuToUnhide;

	public void Hide()
	{
		menuToHide.SetActive(false);
		menuToUnhide.SetActive(true);
	}

	public void Unhide()
	{
		menuToUnhide.SetActive(false);
		menuToHide.SetActive(true);
	}

}
