﻿using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AutoFormGenerator.UserControls.Controls
{
    /// <summary>
    /// Interaction logic for FloatField.xaml
    /// </summary>
    public partial class FloatField : UserControl
    {
        private string HoldValue = "";
        public bool HasUpdated { get; set; }

        public FloatField(string DisplayValue, float Value)
        {
            InitializeComponent();

            DisplayNameTextBlock.Text = DisplayValue.ToString();
            ValueTextBox.Text = Value.ToString();

            ValueTextBox.GotKeyboardFocus += (sender, args) =>
            {
                HoldValue = ValueTextBox.Text;
                HasUpdated = false;
            };

            ValueTextBox.LostKeyboardFocus += (sender, args) =>
            {
                if (HoldValue != ValueTextBox.Text)
                {
                    HasUpdated = true;
                }
            };
        }

        private void ValueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool NotBlock = true;

            NotBlock = Regex.IsMatch(e.Text, "[^0-9.]+");

            double num = 0;
            NotBlock = !double.TryParse(ValueTextBox.Text + e.Text, out num);

            e.Handled = NotBlock;
        }

        public bool Validate()
        {
            bool Valid = true;
            if (ValueTextBox.Text.Length == 0)
            {
                Valid = false;
            }

            float dummy = 0;
            if (!float.TryParse(ValueTextBox.Text, out dummy))
            {
                Valid = false;
            }

            if (Valid)
            {
                ValueTextBox.BorderBrush = Brushes.Black;
            }
            else
            {
                ValueTextBox.BorderBrush = Brushes.Red;
            }

            return Valid;
        }
    }
}
