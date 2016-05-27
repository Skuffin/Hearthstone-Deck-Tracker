using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Importing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDTTests.DeckImport
{
    [TestClass]
    public class DeckImportTest
    {
        [TestMethod]
        public void FullScreenCapture()
        {
            var bmp = new Bitmap("DeckImport/TestFiles/ScreenCaptureTest.bmp");

            var deck = ConstructedDeckImporter.Import(bmp);
        }

    }
}
