using PlayModeTestNodeRecorder;
using UnityEngine;
using NUnit.Framework;
	
public class Sample : NodeTestScript
{
	[Test]
	public void TestMain()
	{
		Touch(new Vector2(156.0456f ,1448.669f));
		Touch(new Vector2(718.6312f ,707.4525f));
		Touch(new Vector2(648.8213f ,1311.103f));
	}
}
