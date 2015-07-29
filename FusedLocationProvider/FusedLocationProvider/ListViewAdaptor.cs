using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FusedLocationProvider.Lib;

namespace FusedLocationProvider
{
    public class ListViewAdapter : BaseAdapter<GPXData>
    {
        private List<GPXData> mItems;
        private Context mContext;

        public ListViewAdapter(Context context, List<GPXData> items)
        {
            mItems = items;
            mContext = context;
        }

        public override GPXData this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listViewRow, null, false);
            }
            TextView tvLat = row.FindViewById<TextView>(Resource.Id.txtLat);
            tvLat.Text = mItems[position].StartLat.ToString();

            TextView tvLog = row.FindViewById<TextView>(Resource.Id.txtLog);
            tvLog.Text = mItems[position].StartLog.ToString();

            TextView tvCondition = row.FindViewById<TextView>(Resource.Id.txtCondition);
            tvCondition.Text = mItems[position].RoadCondition.ToString();
            return row;
        }
    }
}