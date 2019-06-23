/*
{}
*/

using System.Collections;
using PlayModeTestNodeRecorder;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Sample : NodeTestScript
{
	[Test]
	public void TestMain()
	{
		
		Touch(new Vector2(163.8398f ,338.6602f));
	}
	
	[UnityTest]
	public IEnumerator TestLoadSceneClickButton()
	{
		SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
		yield return new WaitForSeconds(3f);
//		var button = GameObject.Find("Button").GetComponent<Button>();
//		button.onClick.AddListener(() =>
//		{
//			Debug.Log("clicked");
//		});
		yield return new WaitForSeconds(1f);
		Touch(new Vector2(206.6094f,340.1602f));
		yield return new WaitForSeconds(1f);
	}
}
