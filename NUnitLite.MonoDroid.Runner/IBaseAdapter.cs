using System;
using Android.Widget;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// This has common methods that are shared by
    /// <see cref="BaseAdapter"/> and <see cref="BaseExpandableListAdapter"/>
    /// </summary>
    public interface IBaseAdapter
    {
        void NotifyDataSetInvalidated();

        void NotifyDataSetChanged();
    }
}