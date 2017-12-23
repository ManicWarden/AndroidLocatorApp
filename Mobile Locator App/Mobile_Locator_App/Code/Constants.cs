using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms; // for the classes (Colour) being used to change the design of the forms


namespace Mobile_Locator_App.Code
{
    class Constants
    {
        public static bool IsUser = true;

        public static Color BackgroundColour = Color.FromRgb(58, 153, 215); // change to red
        public static Color MainTextColour = Color.White;                   // change to cream

        public static int LogInIconHeight = 120; // Otherwise the images default height will be used
                                                 // fine for phones but should use a larger number for tablets
    }
}
