using PlayModeTestNodeRecorder;
using UnityEngine;
using NUnit.Framework;
	
public class Sample : NodeTestScript
{
	[Test]
	public void TestMain()
	{
		Touch(Vector2.zero);
		Touch(Vector2.zero);
		Touch(Vector2.zero);
	}
}
