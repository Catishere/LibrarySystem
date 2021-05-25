using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibrarySystem.MVVM.Model.DB;

namespace LibrarySystem.Controls
{
    /// <summary>
    /// Interaction logic for SuggestTextBox.xaml
    /// </summary>
    public partial class SuggestTextBox : UserControl
    {
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items",
                typeof(IEnumerable<object>),
                typeof(SuggestTextBox),
                new PropertyMetadata(null));
        
        public static readonly DependencyProperty ValueMemberProperty =
            DependencyProperty.Register("ValueMember",
                typeof(KeyValuePair<object, string>),
                typeof(SuggestTextBox));

        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected",
                typeof(object),
                typeof(SuggestTextBox),
                new PropertyMetadata(OnSelectedChanged));

        private static void OnSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            d.SetValue(TextProperty, e.NewValue);
            d.ClearValue(SelectedProperty);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text",
                typeof(object),
                typeof(SuggestTextBox),
                new PropertyMetadata(null));

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder",
                typeof(object),
                typeof(SuggestTextBox),
                new PropertyMetadata(0));

        public static readonly DependencyProperty AutoSuggestProperty =
            DependencyProperty.Register("AutoSuggest",
                typeof(object),
                typeof(SuggestTextBox),
                new PropertyMetadata(string.Empty));
        public object AutoSuggest
        {
            get => GetValue(AutoSuggestProperty);
            set => SetValue(AutoSuggestProperty, value);
        }

        public object Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public object ValueMember
        {
            get => GetValue(ValueMemberProperty);
            set => SetValue(ValueMemberProperty, value);
        }
        public object Selected
        {
            get => GetValue(SelectedProperty);
            set => SetValue(SelectedProperty, value);
        }

        public object Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public object Placeholder
        {
            get => GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public SuggestTextBox()
        {
            InitializeComponent();
        }
    }
}
