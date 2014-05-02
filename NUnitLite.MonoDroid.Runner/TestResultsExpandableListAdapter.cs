using System;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace NUnitLite.MonoDroid
{
    public class TestResultsExpandableListAdapter : BaseExpandableListAdapter, IBaseAdapter
    {
        private readonly Context _context;

        public TestResultsExpandableListAdapter(Context context)
        {
            _context = context;
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return TestRunContext.Current.GetGroupChild(groupPosition, childPosition);
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            var child = TestRunContext.Current.GetGroupChild(groupPosition, childPosition);
            return child.TestCaseName.GetHashCode();
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return TestRunContext.Current.GetGroupChildren(groupPosition).Size();
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var child = TestRunContext.Current.GetGroupChild(groupPosition, childPosition);
            return new TestResultsListView(_context, child);
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return TestRunContext.Current.GetGroupChildren(groupPosition);
        }

        public override long GetGroupId(int groupPosition)
        {
            var group = TestRunContext.Current.GetGroupParent(groupPosition);
            return group.TestCaseName.GetHashCode();
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var group = TestRunContext.Current.GetGroupParent(groupPosition);
            return new TestResultsGroupView(_context, group);
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public override int GroupCount
        {
            get
            {
                return TestRunContext.Current.GetGroupCount();
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }

    }
}