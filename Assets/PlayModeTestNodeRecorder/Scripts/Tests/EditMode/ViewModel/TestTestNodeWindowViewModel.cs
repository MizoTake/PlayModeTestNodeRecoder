using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace PlayModeTestNodeRecorder.Tests.EditMode
{
    using ViewModel = TestNodeWindowViewModel;
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
            var viewModel = new ViewModel ();
            Assert.AreEqual (viewModel.NodeViews.Count, 0);
            var rand = Random.Range (0, 10);
            for (var i = 0; i < rand; i++)
            {
                viewModel.CreateNode (NodeType.Touch, Vector2.zero);
            }
            Assert.AreEqual (viewModel.NodeViews.Count, rand);
        }

        [Test]
        public void CreateLine ()
        {
            var viewModel = new ViewModel ();
            Assert.AreEqual (viewModel.LineViews.Count, 0);

            viewModel.CreateNode (NodeType.Touch, Vector2.zero);
            viewModel.CreateNode (NodeType.Touch, Vector2.one * 200f);
            var nodeOne = viewModel.NodeViews[0];
            var nodeTwo = viewModel.NodeViews[1];
            viewModel.CreateLine (nodeOne, Vector2.zero);
            Assert.AreEqual (viewModel.LineViews.Last (), viewModel.LastCreatedLine);

            viewModel.ConnectNode (Vector2.one * 200f);
            Assert.AreEqual (null, viewModel.LastCreatedLine);
            Assert.AreEqual (nodeOne.EndLine, viewModel.LastCreatedLine);
            Assert.AreEqual (nodeOne.EndLine, nodeTwo.BeginLine);
            Assert.AreNotEqual (nodeOne.BeginLine, nodeOne.EndLine);
        }

        [Test]
        public void CreateLineButAlreadyCreatedLine ()
        {
            var viewModel = new ViewModel ();
            viewModel.CreateNode (NodeType.Touch, Vector2.zero);
            viewModel.CreateNode (NodeType.Touch, Vector2.one * 200f);
            var node = viewModel.NodeViews[0];
            viewModel.CreateLine (node, Vector2.zero);
            var lastLine = viewModel.LineViews[0];
            viewModel.ConnectNode (Vector2.one * 200f);
            var targetLine = node.BeginLine;

            viewModel.CreateLine (node, Vector2.zero);
            Assert.IsTrue (viewModel.LineViews.Contains (targetLine) == false);
        }

        [Test]
        public void ConnectNodeConnection ()
        {
            var viewModel = new ViewModel ();
            viewModel.CreateNode (NodeType.Touch, Vector2.zero);
            viewModel.CreateNode (NodeType.Touch, Vector2.one * 200f);
            var node = viewModel.NodeViews[0];
            viewModel.CreateLine (node, Vector2.zero);
            var lastLine = viewModel.LineViews[0];
            viewModel.ConnectNode (Vector2.one * 200f);
            Assert.AreEqual (node.BeginLine, lastLine);
            Assert.AreEqual (viewModel.LastCreatedLine, null);
        }

        [Test]
        public void ConnectNodeDisConnection ()
        {
            var viewModel = new ViewModel ();
            viewModel.CreateNode (NodeType.Touch, Vector2.zero);
            viewModel.CreateNode (NodeType.Touch, Vector2.one * 200f);
            var node = viewModel.NodeViews[0];
            viewModel.CreateLine (node, Vector2.zero);
            var lastLine = viewModel.LineViews[0];
            viewModel.ConnectNode (Vector2.one * 100f);
            Assert.AreEqual (node.BeginLine, null);
            Assert.AreEqual (viewModel.LastCreatedLine, null);
        }

        [Test]
        public void ClickOnNode ()
        {
            var viewModel = new ViewModel ();
            viewModel.CreateNode (NodeType.Touch, Vector2.zero);
            var node = viewModel.NodeViews[0];
            var selectedNode = viewModel.ClickOnNode (Vector2.zero);
            Assert.AreEqual (node, selectedNode);
        }
    }
}