using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace TestApp
{
    public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused", typeof(bool), typeof(FocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if (!((bool)e.NewValue))
                return;
            uie.Focus();
            Keyboard.Focus(uie);
            uie.LostFocus += UieOnLostFocus;
        }

        private static void UieOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            var uie = sender as UIElement;
            if (uie == null)
                return;
            uie.LostFocus -= UieOnLostFocus;
            uie.SetValue(IsFocusedProperty, false);
        }

    }
}
