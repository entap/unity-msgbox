using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entap
{
	public class MsgBoxExample : MonoBehaviour
	{
		// 開始
		void Start()
		{
			MsgBox.Show().Message("てすとてすとてすとてすと\nてすとてすとてすと")
			.Button(MsgBoxButton.Positive, "はい", () => {
				Debug.Log("はい");
			}).Button(MsgBoxButton.Negative, "いいえ", () => {
				Debug.Log("いいえ");
			});
		}
	}
}