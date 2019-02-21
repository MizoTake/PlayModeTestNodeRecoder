using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            IViewModel viewModel = new MenuViewModel (MenuType.Node);
            Assert.IsTrue (NodeSegue.Items.SequenceEqual (viewModel.ItemData));
        }

        [Test]
        public void MenuTypeWindow ()
        {
            IViewModel viewModel = new MenuViewModel (MenuType.Window);
            Assert.IsTrue (WindowSegue.Items.SequenceEqual (viewModel.ItemData));
        }

        [Test]
        public void SelectItemTypeNode ()
        {
            var returnFlag = false;
            IViewModel viewModel = new MenuViewModel (MenuType.Node);
            Assert.IsTrue (NodeSegue.Items.SequenceEqual (viewModel.ItemData));
            viewModel.Choice (SegueProcess.Transition);
            viewModel.Choice (SegueProcess.Make);
            viewModel.Choice (SegueProcess.Delete);
            try
            {
                viewModel.Choice ("");
            }
            catch
            {
                returnFlag = true;
            }
            Assert.IsTrue (returnFlag, "何でここに来とんねん");
        }

        [Test]
        public void SelectItemTypeWindow ()
        {
            IViewModel viewModel = new MenuViewModel (MenuType.Window);
            Assert.IsTrue (WindowSegue.Items.SequenceEqual (viewModel.ItemData));
        }
    }
}