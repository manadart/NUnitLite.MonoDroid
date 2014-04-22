using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// Displays the details of a run
    /// </summary>
    public abstract class TestRunDetailsActivity: Activity, ITestListener
    {
        private TestRunInfo _testRunInfo;
        private TextView _descriptionTextView;

        /// <summary>
        /// Handles the creation of the activity
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var testCaseName = Intent.GetStringExtra("TestCaseName");
            _testRunInfo = TestRunContext.Current.TestResults.First(item => item.TestCaseName == testCaseName);

            SetContentView(CreateView());
        }

        private View CreateView()
        {
            LinearLayout layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;

            TextView titleTextView = new TextView(this);
            titleTextView.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.FillParent, 48);

            titleTextView.SetBackgroundColor(Color.Argb(255,50,50,50));
            titleTextView.SetPadding(20,0,20,0);

            titleTextView.Gravity = GravityFlags.CenterVertical;
            titleTextView.Text = _testRunInfo.Description;
            titleTextView.Ellipsize = TextUtils.TruncateAt.Start;

            _descriptionTextView = new TextView(this);
            _descriptionTextView.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.WrapContent)
            {
                LeftMargin = 40,
                RightMargin = 40
            };

            SetDescription();

            ScrollView scrollView = new ScrollView(this);
            scrollView.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent);

            scrollView.AddView(_descriptionTextView);

            layout.AddView(titleTextView);
            layout.AddView(scrollView);

            return layout;
        }

        private void SetDescription()
        {
            _descriptionTextView.Text = _testRunInfo.TestResult.Message + "\r\n\r\n" +
                                        _testRunInfo.TestResult.StackTrace;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Re-run");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var testCase = (TestCase) _testRunInfo.TestResult.Test;

            // Start the test process in a background task
            Task.Factory.StartNew(() =>
            {
                testCase.Run(this);
            });

            return true;
        }

        public void TestStarted(ITest test)
        {
            RunOnUiThread(() => _descriptionTextView.Text = "Running...");
        }

        public void TestFinished(TestResult result)
        {
            _testRunInfo.TestResult = result;
            RunOnUiThread(SetDescription);
        }
    }
}