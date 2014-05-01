using System;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// List adapter used to display test results
    /// </summary>
    public class TestResultsListAdapter : BaseAdapter
    {
        private readonly Context _context;

        /// <summary>
        /// Initializes a new instance of <see cref="TestResultsListAdapter"/>
        /// </summary>
        public TestResultsListAdapter(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the number of items in the list
        /// </summary>
        public override int Count
        {
            get { return TestRunContext.Current.TestResults.Count; }
        }

        /// <summary>
        /// Gets an item from the list
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override Java.Lang.Object GetItem(int position)
        {
            return TestRunContext.Current.TestResults.ElementAt(position);
        }

        /// <summary>
        /// Gets the ID of an item in the list
        /// </summary>
        public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Gets the number of view types supported by this adapter
        /// </summary>
        public override int ViewTypeCount
        {
            get { return 2; }
        }

        /// <summary>
        /// Gets the view type for an item in the list
        /// </summary>
        public override int GetItemViewType(int position)
        {
            var result = TestRunContext.Current.TestResults[position];

            return result.IsTestSuite ? 0 : 1;
        }

        /// <summary>
        /// Gets the view for an item
        /// </summary>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var itemContent = TestRunContext.Current.TestResults[position];
            return new TestResultsListView(_context, itemContent);
        }

        /// <summary>
        /// Gets whether all items are enabled
        /// </summary>
        public override bool AreAllItemsEnabled()
        {
            return false;
        }

        /// <summary>
        /// Gets whether a specific item is enabled
        /// </summary>
        public override bool IsEnabled(int position)
        {
            return !TestRunContext.Current.TestResults[position].IsTestSuite;
        }
    }
}