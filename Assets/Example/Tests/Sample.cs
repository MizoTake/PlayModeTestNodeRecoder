using PlayModeTestNodeRecorder;
using UnityEngine;
using NUnit.Framework;
	
public class Sample : NodeTestScript
{
	[Test]
	public void TestMain()
	{
		Touch(new Vector2(30,20));
		Touch(new Vector2(100,50));
	}
}
