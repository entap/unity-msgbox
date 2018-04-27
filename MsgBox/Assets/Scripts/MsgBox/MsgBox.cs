using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

namespace Entap
{
	/// <summary>
	/// メッセージボックスのボタン
	/// </summary>
	public enum MsgBoxButton
	{
		Positive,
		Negative,
		Warning,
		Info,
		Purchase
	}

	/// <summary>
	/// メッセージボックス
	/// </summary>
	public class MsgBox : MonoBehaviour
	{
		[SerializeField] RawImage Guard;
		[SerializeField] GameObject Box;
		[SerializeField] GameObject MessagePrototype;
		[SerializeField] GameObject ImagePrototype;
		[SerializeField] GameObject[] ButtonPrototypes;
		[SerializeField] GameObject ContentGroup;
		[SerializeField] GameObject ButtonGroup;
		List<GameObject> ContentList = new List<GameObject>();
		List<GameObject> ButtonList = new List<GameObject>();
		bool IsVisible;

		/// <summary>
		/// 開始
		/// </summary>
		void Awake()
		{
			Init();
			Hide();
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Update()
		{
		}

		/// <summary>
		/// メッセージボックスを取得する。
		/// </summary>
		/// <returns>メッセージボックスのインスタンス</returns>
		static MsgBox Find()
		{
			var msgBoxes = GameObject.FindObjectsOfType<MsgBox>();
			if (msgBoxes.Length > 1) {
				throw new UnityException("MsgBox must be only one.");
			}
			return msgBoxes[0];
		}

		/// <summary>
		/// メッセージボックスを初期化する。
		/// </summary>
		/// <returns>メッセージボックスのインスタンス</returns>
		MsgBox Init()
		{
			// コンテンツとボタンを削除
			foreach (var content in ContentList) {
				GameObject.Destroy(content);
			}
			foreach (var button in ButtonList) {
				GameObject.Destroy(button);
			}
			ContentList.Clear();
			ButtonList.Clear();

			// プロトタイプを隠す
			MessagePrototype.SetActive(false);
			ImagePrototype.SetActive(false);
			foreach (var buttonPrototype in ButtonPrototypes) {
				buttonPrototype.SetActive(false);
			}

			return this;
		}

		/// <summary>
		/// メッセージボックスを表示する。
		/// </summary>
		/// <returns>メッセージボックスのインスタンス</returns>
		static public MsgBox Show()
		{
			var msgBox = Find().Init();
			msgBox.IsVisible = true;
			return msgBox;
		}

		/// <summary>
		/// メッセージボックスを非表示にする。
		/// </summary>
		/// <returns>メッセージボックスのインスタンス</returns>
		static public MsgBox Hide(bool animated = true)
		{
			var msgBox = Find().Init();
			msgBox.IsVisible = false;
			if (!animated) {
				msgBox.Guard.color = new Color(0, 0, 0, 0);
				msgBox.Box.GetComponent<RectTransform>().localScale = Vector3.one;
			}
			return msgBox;
		}

		/// <summary>
		/// メッセージボックスにメッセージを追加する。
		/// </summary>
		/// <returns>メッセージボックスのインスタンス</returns>
		/// <param name="text">メッセージの文字列</param>
		/// <param name="color">メッセージの色</param>
		public MsgBox Message(string text, Color color)
		{
			var g = GameObject.Instantiate(MessagePrototype, ContentGroup.transform);
			var gText = g.GetComponent<Text>();
			gText.text = text;
			gText.color = color;
			g.SetActive(true);
			ContentList.Add(g);

			return this;
		}

		/// <summary>
		/// メッセージボックスにメッセージを追加する。
		/// </summary>
		/// <returns>メッセージボックスのインスタンス</returns>
		/// <param name="text">メッセージの文字列</param>
		public MsgBox Message(string text)
		{
			return Message(text, Color.black);
		}

		/// <summary>
		/// メッセージボックスに画像を追加する。
		/// </summary>
		/// <returns>メッセージボックスのインスタンス</returns>
		public MsgBox Image(Texture2D texture)
		{
			var g = GameObject.Instantiate(ImagePrototype, ContentGroup.transform);
			var gImage = g.GetComponent<Image>();
			gImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
			gImage.SetNativeSize();
			g.SetActive(true);
			ContentList.Add(g);

			return this;
		}

		/// <summary>
		/// メッセージボックスにボタンを追加する。
		/// </summary>
		/// <returns>メッセージボックスのインスタンス</returns>
		/// <param name="type">ボタンの種類</param>
		/// <param name="text">ボタンの文字列</param>
		/// <param name="action">ボタンを押した時の処理</param>
		public MsgBox Button(MsgBoxButton type, string text, UnityAction action)
		{
			var g = GameObject.Instantiate(ButtonPrototypes[(int)type], ButtonGroup.transform);
			var gButton = g.GetComponentInChildren<Button>();
			var gText = g.GetComponentInChildren<Text>();
			gButton.onClick.AddListener(() => {
				Hide();
				action();
			});
			gText.text = text;
			g.SetActive(true);
			ButtonList.Add(g);

			return this;
		}
	}
}
