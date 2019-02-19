using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeRecoderTest.Tests
{
    public class TestMenuViewModel
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
        public void MenuTypeNode ()
        {
            var viewModel = new MenuViewModel (MenuType.Node);
            Assert.AreEqual (NodeSegue.Items, viewModel.ItemData);
        }

        [Test]
        public void MenuTypeWindow ()
        {
            var viewModel = new MenuViewModel (MenuType.Window);
            Assert.AreEqual (WindowSegue.Items, viewModel.ItemData);
        }
    }
}