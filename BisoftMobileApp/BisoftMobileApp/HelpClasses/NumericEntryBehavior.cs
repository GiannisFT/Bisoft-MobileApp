using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BisoftMobileApp.HelpClasses
{
    class NumericEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            //entry.TextChanged -= OnEntryTextChanged;
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x) || x == '-');

                //((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                if (!isValid)
                {
                    ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    //((Entry)sender).Text = args.OldTextValue;
                }

                if (args.NewTextValue == "00")
                {
                    ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                }
                //if (args.NewTextValue.StartsWith("0") && args.NewTextValue.Length == 2)
                //{
                //    int zero_index = args.NewTextValue.LastIndexOf('0');
                //    if (zero_index == 1)
                //    {
                //        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                //    }
                //    else
                //    {
                //        ((Entry)sender).Text = args.NewTextValue.Remove(0, 1).Insert(0, args.NewTextValue.Substring(1, 1));
                //    }
                //}

                if (args.NewTextValue.Contains('-'))
                {
                    if (!args.NewTextValue.StartsWith("0"))
                    {
                        int count = args.NewTextValue.Count(x => x == '-');
                        if (count == 1 && args.NewTextValue.IndexOf('-') != 0)
                        {
                            string s = args.NewTextValue;
                            int minus_index = s.IndexOf('-');
                            s = s.Remove(minus_index, 1).Insert(0, "-");

                            ((Entry)sender).Text = s;
                        }
                        else if (count > 1)
                        {
                            ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                        }
                    }
                    else
                    {
                        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    }
                }
            }
        }
    }
}
