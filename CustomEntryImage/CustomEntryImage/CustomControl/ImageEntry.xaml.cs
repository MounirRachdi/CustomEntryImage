﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomEntryImage.CustomControl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageEntry : ContentView
	{
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                           typeof(string),
                                                           typeof(ImageEntry),
                                                           defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder),
                                                            typeof(string), typeof(ImageEntry),
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: (bindable, oldVal, newval) =>
                                                            {
                                                                var matEntry = (ImageEntry)bindable;
                                                                matEntry.imgEntry.Placeholder = (string)newval;
                                                            });
        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword),
                                                            typeof(bool),
                                                            typeof(ImageEntry),
                                                            defaultValue: false,
                                                            propertyChanged: (bindable, oldVal, newVal) =>
                                                            {
                                                                var matEntry = (ImageEntry)bindable;
                                                                matEntry.imgEntry.IsPassword = (bool)newVal;
                                                            });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard),
                                                            typeof(Keyboard),
                                                            typeof(ImageEntry),
                                                            defaultValue: Keyboard.Default,
                                                            propertyChanged: (bindable, oldVal, newVal) =>
                                                            {
                                                                var matEntry = (ImageEntry)bindable;
                                                                matEntry.imgEntry.Keyboard = (Keyboard)newVal;
                                                            });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor),
                                                             typeof(Color),
                                                             typeof(ImageEntry),
                                                             defaultValue: Color.Accent);
        public static readonly BindableProperty LImageSourceProperty = BindableProperty.Create(nameof(LImageSource),
                                                                       typeof(ImageSource),
                                                                       typeof(ImageEntry),
                                                                       defaultBindingMode: BindingMode.TwoWay,
                                                                       propertyChanged: (bindable, oldVal, newVal) =>
                                                                       {
                                                                           var matEntry = (ImageEntry)bindable;
                                                                           matEntry.LIcon.Source = (ImageSource)newVal;
                                                                       });
        public static readonly BindableProperty RImageSourceProperty = BindableProperty.Create(nameof(RImageSource),
                                                                       typeof(ImageSource),
                                                                       typeof(ImageEntry),
                                                                       defaultBindingMode: BindingMode.TwoWay,
                                                                       propertyChanged: (bindable, oldVal, newVal) =>
                                                                       {
                                                                           var matEntry = (ImageEntry)bindable;
                                                                           matEntry.RIcon.Source = (ImageSource)newVal;
                                                                       });
        public static readonly BindableProperty ImageAlignmentProperty = BindableProperty.Create(nameof(ImageAlignment),
                                                                         typeof(eImageAlignment),
                                                                         typeof(ImageEntry),
                                                                         defaultValue: eImageAlignment.None,
                                                                         propertyChanged: OnImageAlignmentChanged);
       
        public ImageSource RImageSource
        {
            get { return (ImageSource)GetValue(RImageSourceProperty); }
            set { SetValue(RImageSourceProperty, value); }
        }
        public ImageSource LImageSource
        {
            get { return (ImageSource)GetValue(LImageSourceProperty); }
            set { SetValue(LImageSourceProperty, value); }
        }

        public Color AccentColor
        {
            get
            {
                return (Color)GetValue(AccentColorProperty);
            }
            set
            {
                SetValue(AccentColorProperty, value);
            }
        }

        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }
            set
            {
                SetValue(KeyboardProperty, value);
            }
        }

        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public eImageAlignment ImageAlignment
        {
            get => (eImageAlignment)GetValue(ImageAlignmentProperty);
            set => SetValue(ImageAlignmentProperty, value);
        }
        public event EventHandler LeftImageClicked;

        public virtual void LeftImageOn_Clicked(object sender, EventArgs e)
        {
            LeftImageClicked?.Invoke(sender, e);
        }
        public event EventHandler RightImageClicked;

        public virtual void RightImageOn_Clicked(object sender, EventArgs e)
        {
            RightImageClicked?.Invoke(sender, e);
        }
        public ImageEntry()
        {
            InitializeComponent();
            imgEntry.BindingContext = this;
            RIcon.ImageClicked += RightImageOn_Clicked;
            LIcon.ImageClicked += LeftImageOn_Clicked;
            imgEntry.Focused += async (s, a) =>
            {
                BottomBorder.HeightRequest = 2.5;
                BottomBorder.BackgroundColor = AccentColor;
                HiddenBottomBorder.BackgroundColor = AccentColor;
                if (string.IsNullOrEmpty(imgEntry.Text))
                {
                    await Task.WhenAll(

                        HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200)
                     );
                    // LeftBorder.Layout(new Rectangle(1, 1, 1, 20));
                    imgEntry.Placeholder = null;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
                }
            };
            imgEntry.Unfocused += async (s, a) =>
            {
                BottomBorder.HeightRequest = 1;
                BottomBorder.BackgroundColor = Color.Gray;
                if (string.IsNullOrEmpty(imgEntry.Text))
                {
                    await Task.WhenAll(
                        HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200)
                     );
                    imgEntry.Placeholder = Placeholder;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
                }
            };
        }

        private static void OnImageAlignmentChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as ImageEntry;
            switch (control.ImageAlignment)
            {
                case eImageAlignment.None:
                    control.LIcon.IsVisible = false;
                    control.RIcon.IsVisible = false;
                    break;
                case eImageAlignment.Left:
                    control.LIcon.IsVisible = true;
                    control.RIcon.IsVisible = false;
                    break;
                case eImageAlignment.Right:
                    control.LIcon.IsVisible = false;
                    control.RIcon.IsVisible = true;
                    break;
                case eImageAlignment.Password:
                    control.LIcon.IsVisible = true;
                    control.RIcon.IsVisible = true;
                    break;
            }
        }

        public enum eImageAlignment
        {
            Left,
            Right,
            Password,
            None
        }
    }
}