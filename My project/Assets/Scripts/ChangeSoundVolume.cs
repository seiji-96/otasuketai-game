using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// "AudioSource"コンポーネントがアタッチされていない場合アタッチ
[RequireComponent(typeof(AudioSource))]
public class ChangeSoundVolume : MonoBehaviour
{
	private AudioSource audioSource;
	public GameObject slider1;
	public GameObject slider2;
	private Slider sound1;
	private Slider sound2;

	private void Start()
	{
		// "AudioSource"コンポーネントを取得
		audioSource = gameObject.GetComponent<AudioSource>();
		sound1 = slider1.GetComponent<Slider>();
		sound2 = slider2.GetComponent<Slider>();
		sound1.value = PlayerPrefs.GetFloat("Sound", 0.5f);
		sound2.value = PlayerPrefs.GetFloat("Sound", 0.5f);
	}

	/// <summary>
	/// スライドバー値の変更イベント
	/// </summary>
	/// <param name="newSliderValue">スライドバーの値(自動的に引数に値が入る)</param>
	public void SoundSliderOnValueChange(float newSliderValue)
	{
		// 音楽の音量をスライドバーの値に変更
		audioSource.volume = newSliderValue;
		PlayerPrefs.SetFloat("Sound", newSliderValue);
		sound1.value = PlayerPrefs.GetFloat("Sound", 0.2f);
		sound2.value = PlayerPrefs.GetFloat("Sound", 0.2f);
	}
}
