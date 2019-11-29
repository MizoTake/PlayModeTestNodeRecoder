using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeTestNodeRecorder.Tests.EditMode
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
            IChoosable viewModel = new MenuViewModel (MenuType.Node);
            Assert.IsTrue (NodeSegue.Items.SequenceEqual (viewModel.ItemData));
        }

        [Test]
        public void MenuTypeWindow ()
        {
            IChoosable viewModel = new MenuViewModel (MenuType.Window);
            Assert.IsTrue (WindowSegue.Items.SequenceEqual (viewModel.ItemData));
        }

        [Test]
        public void SelectItemTypeWindow ()
        {
            IChoosable viewModel = new MenuViewModel (MenuType.Window);
            Assert.IsTrue (WindowSegue.Items.SequenceEqual (viewModel.ItemData));
        }
    }
}