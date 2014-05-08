using System;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// View for the groups (classes) in the list
    /// </summary>
    public sealed class TestResultsGroupView : LinearLayout
    {
        public TestResultsGroupView(Context context, TestRunInfo testRunInfo) : base(context)
        {
            var indicatorView = new View(context)
            {
                LayoutParameters = new LayoutParams(18, 18)
                {
                    LeftMargin = 60,
                    RightMargin = 14,
                    TopMargin = 14,
                    BottomMargin = 14,
                    Gravity = GravityFlags.CenterVertical
                }
            };
            indicatorView.SetBackgroundColor(
                testRunInfo.Running ? Color.Gray : testRunInfo.Passed ? Color.Green : Color.Red);
            AddView(indicatorView);

            var container = new LinearLayout(context)
            {
                Orientation = Orientation.Vertical,
                LayoutParameters =
                    new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent)
            };
            AddView(container);

            var text1 = new TextView(context)
            {
                Text = testRunInfo.Description,
                Ellipsize = TextUtils.TruncateAt.Marquee,
                LayoutParameters =
                    new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            };
            text1.SetTextSize(ComplexUnitType.Sp, 22);
            text1.SetSingleLine(true);
            text1.SetPadding(2, 2, 2, 2);
            container.AddView(text1);

            var text2 = new TextView(context)
            {
                Text = testRunInfo.TestCaseName,
                Ellipsize = TextUtils.TruncateAt.Marquee,
                LayoutParameters =
                    new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            };
            text2.SetTextSize(ComplexUnitType.Sp, 14);
            text2.SetSingleLine(true);
            text2.SetPadding(2, 2, 2, 2);
            container.AddView(text2);
        }
    }
}