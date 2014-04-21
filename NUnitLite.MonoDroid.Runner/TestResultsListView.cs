using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// View for the test result in the list
    /// </summary>
    public sealed class TestResultsListView : LinearLayout
    {
        public TestResultsListView(Context context, TestRunInfo itemContent) : base(context)
        {
            Orientation = Orientation.Horizontal;
            
            var indicatorView = new View(context);
            indicatorView.SetBackgroundColor(itemContent.Running ? Color.Gray : itemContent.Passed ? Color.Green : Color.Red);

            var descriptionView = new TextView(context)
            {
                Ellipsize = Android.Text.TextUtils.TruncateAt.Start,
                Gravity = GravityFlags.CenterVertical,
                Text = itemContent.Description
            };

            if(itemContent.IsTestSuite)
            {
                SetBackgroundColor(Color.Argb(255,50,50,50));
                
                indicatorView.LayoutParameters = new LayoutParams(18, 18)
                {
                    LeftMargin = 14,
                    RightMargin = 14,
                    TopMargin = 14,
                    BottomMargin = 14,
                    Gravity = GravityFlags.CenterVertical
                };

                descriptionView.LayoutParameters = new LayoutParams(
                    ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent)
                {
                    BottomMargin = 14,
                    TopMargin = 14,
                    RightMargin = 14,
                    Height = 48,
                    Gravity = GravityFlags.CenterVertical
                };
            }
            else
            {
                indicatorView.LayoutParameters = new LayoutParams(
                    18, ViewGroup.LayoutParams.FillParent)
                {
                    RightMargin = 20
                };

                descriptionView.LayoutParameters = new LayoutParams(
                    ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent)
                {
                    BottomMargin = 20,
                    TopMargin = 20,
                    RightMargin = 20,
                    Gravity = GravityFlags.CenterVertical
                };
            }

            AddView(indicatorView);
            AddView(descriptionView);
        }
    }
}