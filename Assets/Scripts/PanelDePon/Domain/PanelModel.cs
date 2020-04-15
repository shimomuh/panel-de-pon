using System.Collections.Generic;
using SupportDomain;

namespace PanelDePon.Domain
{
    public class PanelModel
    {
        private static List<string> defaultMarks = new List<string>()
        {
            "Sun", "Cloud", "Rain", "Moon", "Thunder"
        };

        private static string sixthMark = "Snow";

        private static string specialMark = "Rainbow";

        public static PanelModel buildErasablePanel()
        {
            var model =  new PanelModel();
            model.erasable = true;
            model.isRisingUp = true;
            return model;
        }

        public string Mark { get; private set; }

        public bool IsRisingUp { get { return isRisingUp; } }

        private bool erasable;

        private bool isRisingUp = true;

        private List<string> selectableMarks;

        public PanelModel() {
            selectableMarks = defaultMarks;
        }

        public PanelModel(int markNum = 5)
        {
            if (markNum == 6) {
                selectableMarks = defaultMarks.DeepCopyAndAddAndReturn(sixthMark);
                return;
            }
            selectableMarks = defaultMarks;
        }

        public PanelModel(int markNum = 5, float specialMarkRatio = 0.05f)
        {
            // TODO
        }

        public void SetMarkRandomly()
        {
            Mark = selectableMarks.RandomTake();
        }

        public void SetMarkRandomlyExceptFor(string mark)
        {
            Mark = selectableMarks.Extract(mark).RandomTake();
        }

        public void SetMarkRandomlyExceptFor(List<string> marks)
        {
            Mark = selectableMarks.Extract(marks).RandomTake();
        }

        public void ShowUpCompletely()
        {
            erasable = true;
        }

        public void DropDown()
        {
            erasable = false;
        }

        public void Swip()
        {
            erasable = false;
        }

        public void StopRisingUp()
        {
            isRisingUp = false;
        }

        public void RisingUp()
        {
            isRisingUp = true;
        }
    }
}
