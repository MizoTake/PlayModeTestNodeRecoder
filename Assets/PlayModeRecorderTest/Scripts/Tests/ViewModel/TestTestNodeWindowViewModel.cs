using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace PlayModeRecorderTest.Tests
{
    public class TestTestNodeWindowViewModel
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
            var viewModel = new TestNodeWindowViewModel ();
            Assert.AreEqual (viewModel.NodeViews.Count, 0);
            var rand = Random.Range (0, 10);
            for (var i = 0; i < rand; i++)
            {
                viewModel.CreateNode (Vector2.zero);
            }
            Assert.AreEqual (viewModel.NodeViews.Count, rand);
        }
    }
}