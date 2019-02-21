using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace PlayModeRecoderTest.Tests
{
    public class TestTestNodeWindowModel
    {
        [SetUp]
        public void SetUp ()
        {

        }

        [TearDown]
        public void TearDown ()
        {

        }

        [Test]
        public void CreateNodeCount ()
        {
            var model = new TestNodeWindowModel ();
            Assert.AreEqual (model.NodeViews.Count, 0);
            var rand = Random.Range (0, 10);
            for (var i = 0; i < rand; i++)
            {
                model.CreateNode (Vector2.zero);
            }
            Assert.AreEqual (model.NodeViews.Count, rand);
        }
    }
}