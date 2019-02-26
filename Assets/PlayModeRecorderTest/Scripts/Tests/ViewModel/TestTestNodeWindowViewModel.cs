using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void CreateLineCount ()
        {
            var viewModel = new TestNodeWindowViewModel ();
            Assert.AreEqual (viewModel.LineViews.Count, 0);
            viewModel.CreateNode (Vector2.zero);
            var node = viewModel.NodeViews[0];
            var rand = Random.Range (0, 10);
            for (var i = 0; i < rand; i++)
            {
                viewModel.CreateLine (node, Vector2.zero, node);
            }
            Assert.AreEqual (viewModel.LineViews.Last (), viewModel.LastCreatedLine);
            Assert.AreEqual (node.StartLine, viewModel.LastCreatedLine);
            Assert.AreEqual (viewModel.LineViews.Count, rand);
        }

        [Test]
        public void ConnectNode ()
        {
            var viewModel = new TestNodeWindowViewModel ();
            viewModel.CreateNode (Vector2.zero);
            var node = viewModel.NodeViews[0];
            viewModel.CreateLine (node, Vector2.zero, node);
            viewModel.ConnectNode (Vector2.zero);
            Assert.AreEqual (node.EndLine, viewModel.LineViews[0]);
            Assert.AreEqual (viewModel.LastCreatedLine, null);
        }

        [Test]
        public void ClickOnNode ()
        {
            var viewModel = new TestNodeWindowViewModel ();
            viewModel.CreateNode (Vector2.zero);
            var node = viewModel.NodeViews[0];
            var selectedNode = viewModel.ClickOnNode (Vector2.zero);
            Assert.AreEqual (node, selectedNode);
        }
    }
}