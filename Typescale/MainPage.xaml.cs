using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Typescale
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            RenderValues();

        }

        ObservableCollection<Typescale> values = new ObservableCollection<Typescale>
        {
            new Typescale("Open Sans Semi-bold 21", "H1 Heading", 36),
            new Typescale("Open Sans Semi-bold 18", "H2 Heading", 31),
            new Typescale("Open Sans Regular 18","H3 Heading", 31),
            new Typescale("Open Sans Semi-bold 16", "Subtitle 1", 28),
            new Typescale("Open Sans Regular 16", "Subtitle 2", 28),
            new Typescale("Open Sans Bold 14", "Label", 24),
            new Typescale("Open Sans Regular 10", "Caption", 17),
            new Typescale("Open Sans Regular 14", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut suscipit dapibus dui, ut gravida magna. Proin mollis felis risus, sit amet auctor sapien interdum at. Etiam molestie scelerisque odio. Aenean rutrum a felis vel facilisis. Phasellus dignissim tellus eu convallis vestibulum. Vivamus pharetra vitae mi non sodales. Fusce gravida quam non ex porttitor, sed imperdiet ex scelerisque. Sed fermentum urna velit, vel volutpat nunc ultrices nec. Integer gravida metus at nunc dapibus, ut ornare eros tincidunt. Donec vulputate porta odio, eget faucibus ipsum. Sed suscipit lectus eleifend massa egestas, in auctor libero rhoncus. Proin gravida, diam aliquam bibendum viverra, tellus risus mollis diam, ut egestas lectus nibh vitae massa. Nam nec massa et diam dictum iaculis. Maecenas facilisis dignissim elit eget lobortis. Suspendisse nisi neque, ultrices non ligula at, sollicitudin cursus enim. Vestibulum sed tempor enim, quis maximus tellus.", 24)
        };

        public void RenderValues()
        { 

            // Create Labels for each row to render out the text examples;
            for (int i = 0; i < values.Count; i++)
            {
                var row = i;
                var label1 = new CustomLabel();
                var label2 = new Label();
                var label3 = new CustomLabel();


                label1 = new CustomLabel
                {
                    Text = values[i].FontName,
                    Style = (Style)Application.Current.Resources["LabelStyles"],
                    FontSize = 10,
                    VerticalTextAlignment = TextAlignment.End,
                    Margin = new Thickness(0, 0, 5, 5),
                    FontFamily = SetFontFromDevice("OpenSans-Regular")

                };
                label2 = new Label
                {
                    Text = values[i].FontString,
                    Style = (Style)Application.Current.Resources["LabelStyles"],
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.End,
                    Margin = new Thickness(5, 0, 0, 5),
                    FontSize = values[i].FontSize,
                    LineHeight = SetLineHeightFromDevice(values[i])
                };
                label3 = new CustomLabel
                {
                    Text = values[i].LineHeight.ToString(),
                    Style = (Style)Application.Current.Resources["LabelStyles"],
                    VerticalTextAlignment = TextAlignment.End,
                    HorizontalTextAlignment = TextAlignment.Start,
                    FontSize = 10,
                    Margin = new Thickness(5, 0, 0, 5),
                    FontFamily = SetFontFromDevice("OpenSans-Regular")
                };
                Label[] labelsbefore = { label1, label2, label3 };
                labelsbefore = SetLabelFonts(labelsbefore);
                ObservableCollection<Frame> frames = FrameLabels(labelsbefore);

                int x = 0;
                foreach(Frame element in frames)
                {
                    grid.Children.Add(element, x, i);
                    x++;                   
                }
            }

        }
        //Takes each label and sets the fonts for each device
        public Label [] SetLabelFonts(Label [] labels)
        {
            if (labels[0].Text.Contains("Semi-bold"))
            {
                labels[1].FontFamily = SetFontFromDevice("OpenSans-SemiBold");
            } else if (labels[0].Text.Contains("Regular"))
            {
                labels[1].FontFamily = SetFontFromDevice("OpenSans-Regular");
            } else if (labels[0].Text.Contains("Bold 14"))
            {
                labels[1].FontFamily = SetFontFromDevice("OpenSans-Bold"); 
            }

            return labels;
        }

        //Pass in the iOS Font Family name and it will return the appropriate string for other devices
        public string SetFontFromDevice(string font)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return font;
            } else if(Device.RuntimePlatform == Device.Android)
            {
                return font + ".ttf#" + font;
            }
            else if(Device.RuntimePlatform == Device.UWP)
            {
                return "Assets/Fonts/" + font + ".ttf#" + "Open Sans";
            }
            else
            {
                return null;
            }
        }

        public double SetLineHeightFromDevice(Typescale values)
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                return values.LineHeight / values.FontSize;
            }
            else
            {
                return values.LineHeight / values.FontSize;
            }
        }

        //Creates a frame around the labels to create the borders
        public ObservableCollection<Frame> FrameLabels(Label[] labels)
        {
            ObservableCollection<Frame> frames = new ObservableCollection<Frame>();

            for(int i = 0; i < labels.Length; i++)
            {
                var frame = new Frame
                {
                    Content = labels[i],
                    BorderColor = Color.Silver,
                    Padding = 1,
                    HasShadow = false,
                    IsClippedToBounds = true,
                    CornerRadius = 0,
                    Margin = -1
                };
                frames.Add(frame);
            }
            return frames;
        }

        public class Typescale: BindableObject {
            public Typescale(string _fontName, string _fontString, float _lineHeight)
            {
                FontName = _fontName;
                FontString = _fontString;
                LineHeight = _lineHeight;
                FontSize = SetFontSize();
            }
            public string FontName;
            public string FontString;
            public float LineHeight;
            public int FontSize;

            private int SetFontSize()
            {
                return Convert.ToInt32(FontName.Substring(Math.Max(0, FontName.Length - 2)));
            }
        }

    }
}
