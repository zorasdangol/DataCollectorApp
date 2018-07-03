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
using DataCollector.Interfaces;
using DataCollector.Droid.Renderers;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace DataCollector.Droid.Renderers
{
    public class MessageAndroid : IMessage
    {
            public void LongAlert(string message)
            {
                Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
            }

            public void ShortAlert(string message)
            {
                Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
            }
        
    }
}