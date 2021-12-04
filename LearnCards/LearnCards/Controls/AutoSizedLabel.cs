using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearnCards.Controls
{

    // Sourse: https://defuncart.com/blog/tech/2019/02/16/learning-xamarin-07-autosize-label.html
    public class AutoSizedLabel : Label 
    {
        /// <summary>
        /// Autosizes the label's font size with regards to it's container size.
        /// </summary>
        private void AutoFontSize()
        {
            //determine the text height for the min font size
            double lowerFontSize = 10;
            double lowerTextHeight = TextHeightForFontSize(lowerFontSize);

            //determine the text height for the max font size
            double upperFontSize = 100;
            double upperTextHeight = TextHeightForFontSize(upperFontSize);

            //start a loop which'll find the optimal font size
            while (upperFontSize - lowerFontSize > 1)
            {
                //determine current average font size and calculate corresponding text height
                double fontSize = (lowerFontSize + upperFontSize) / 2;
                double textHeight = TextHeightForFontSize(upperFontSize);

                //if the calculated height is out of bounds, update max values, else update min values
                if (textHeight > Height)
                {
                    upperFontSize = fontSize; upperTextHeight = textHeight;
                }
                else
                {
                    lowerFontSize = fontSize; lowerTextHeight = textHeight;
                }
            }

            //finally set the correct font size
            FontSize = lowerFontSize;
        }

        /// <summary>
        /// Determines the text height for the label with a given font size.
        /// </summary>
        private double TextHeightForFontSize(double fontSize)
        {
            FontSize = fontSize;
            return OnMeasure(Width, Double.PositiveInfinity).Request.Height;
        }

        /// <summary>
        /// Callback when the size of the element is set during a layout cycle.
        /// </summary>
        protected override void OnSizeAllocated(double width, double height)
        {
            //call base implementation
            base.OnSizeAllocated(width, height);

            //update font size
            AutoFontSize();
        }

        /// <summary>
        /// The label's text.
        /// </summary>
        new public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); AutoFontSize(); }
        }
    }
}
