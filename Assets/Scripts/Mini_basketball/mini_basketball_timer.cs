using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mini_basketball_timer : MonoBehaviour
{
	[SerializeField] mini_basketball_GameManager manager;
	[SerializeField] Text timerText;
	[SerializeField] float _minutes = 1;
	[SerializeField] float _seconds = 30;
	[SerializeField] float _miliseconds = 1000;

	void Update() {
		if (!manager.gameStarted)
			return;

		if (_minutes <= 0 && _seconds <= 0 && _miliseconds <= 0) {
			timerText.text = "00:00:000";
			manager.gameStarted = false;
			return;
		}

		if(_miliseconds <= 0) {
			if(_seconds <= 0) {
				_minutes--;
				_seconds = 59;
			}
			else if(_seconds >= 0)
				_seconds--;

			_miliseconds = 1000;
		}
		_miliseconds -= Time.deltaTime * 1000;

		timerText.text = string.Format("{0:00}:{1:00}:{2:000}", _minutes, _seconds, (int)_miliseconds);
	}

	public void setTimer(float minutes = 1f, float seconds = 30,  float miliseconds = 1000)
	{
		_minutes = minutes;
		_seconds = seconds;
		_miliseconds = miliseconds;
	}
}