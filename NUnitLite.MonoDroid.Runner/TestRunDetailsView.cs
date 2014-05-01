using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// View for the test result details screen
    /// </summary>
    public sealed class TestRunDetailsView: LinearLayout
    {
        private readonly TextView _descriptionTextView;

        public TestRunDetailsView(Context context, TestRunInfo testRunDetails) : base(context)
        {
            Orientation = Orientation.Vertical;

            var titleTextView = new TextView(context)
            {
                LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.FillParent, 48)
            };

            titleTextView.SetBackgroundColor(Color.Argb(255,50,50,50));
            titleTextView.SetPadding(20,0,20,0);

            titleTextView.Gravity = GravityFlags.CenterVertical;
            titleTextView.Text = testRunDetails.Description;
            titleTextView.Ellipsize = TextUtils.TruncateAt.Start;

            _descriptionTextView = new TextView(context)
            {
                LayoutParameters = new LayoutParams(
                    ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent)
                {
                    LeftMargin = 40,
                    RightMargin = 40
                }
            };

            var scrollView = new ScrollView(context)
            {
                LayoutParameters = new LayoutParams(
                    ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent)
            };

            scrollView.AddView(_descriptionTextView);

            AddView(titleTextView);
            AddView(scrollView);
        }

        public string Description
        {
            set
            {
                _descriptionTextView.Text = value;
            }
        }
    }
}