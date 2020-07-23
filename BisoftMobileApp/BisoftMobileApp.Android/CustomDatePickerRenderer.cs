using System;
using Android.Runtime;
using Android.Content;
using Android.OS;
using Xamarin.Forms;
using BisoftMobileApp.HelpClasses;
using Xamarin.Forms.Platform.Android;
using BisoftMobileApp.Droid;
using Android.Content.Res;
using Android.Graphics;
using Android.App;

[assembly: ExportRenderer(typeof(QRCustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace BisoftMobileApp.Droid
{
    class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context) : base(context)
        {
            
        }
        protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            DatePicker view = Element;
            var dialog = new DatePickerDialog(Context, Resource.Style.QRDatePickerCustomStyle, (o, e) =>
            {
                view.Date = e.Date;
                ((IElementController)view).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
            }, year, month, day);

            //DatePickerDialog dialog = new DatePickerDialog(Context, Resource.Style.QRDatePickerCustomStyle);//Refers to styles.xml for the custom attributes

            return dialog;
        }

        //protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        //{
        //    base.OnElementChanged(e);

        //    if (Control == null || e.NewElement == null)
        //        return;

        //    if (e.NewElement is QRCustomDatePicker customDatePicker)
        //    {
        //        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        //        {
        //            Control.BackgroundTintList = ColorStateList.ValueOf(customDatePicker.UnderlineColor.ToAndroid());
        //        }
        //        else
        //        {
        //            Control.Background.SetColorFilter(new PorterDuffColorFilter(customDatePicker.UnderlineColor.ToAndroid(), PorterDuff.Mode.SrcAtop));
        //        }
        //    }
        //}
    }
}