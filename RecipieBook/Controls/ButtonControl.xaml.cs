using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace RecipieBook
{
    public sealed partial class ButtonControl : UserControl
    {
        public static List<Uri> recepies = new List<Uri>();
        public string savedRecipie = "recipe saved!";

        public ButtonControl()
        {
            this.InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var _chocolateRecipeUri = ChocolateView.recipeUri;
            var _cinnamonRecipeUri = CinnamonView.recipeUri;
            var _rndRecipieUri = RndFoodView.recipeUri;

            if (recepies.Count > 0)
            {               
                if (this.Tag.ToString() == "chocolate")
                {
                    if (_chocolateRecipeUri != null && !recepies.Contains(_chocolateRecipeUri))
                    {
                        recepies.Add(_chocolateRecipeUri);
                        txt.Text = savedRecipie;
                    }
                }
                if (this.Tag.ToString() == "cinnamon")
                {
                    if (_cinnamonRecipeUri != null && !recepies.Contains(_cinnamonRecipeUri))
                    {
                        recepies.Add(_cinnamonRecipeUri);
                        txt.Text = savedRecipie;
                    }
                }
                if (this.Tag.ToString() == "rnd")
                {
                    if (_rndRecipieUri != null && !recepies.Contains(_rndRecipieUri))
                    {
                        recepies.Add(_rndRecipieUri);
                        txt.Text = savedRecipie;
                    }
                }
            }
            else
            {
                if (_chocolateRecipeUri != null && this.Tag.ToString() == "chocolate")
                {
                    recepies.Add(_chocolateRecipeUri);
                    txt.Text = savedRecipie;
                }
                if (_cinnamonRecipeUri != null && this.Tag.ToString() == "cinnamon")
                {
                    recepies.Add(_cinnamonRecipeUri);
                    txt.Text = savedRecipie;
                }
                if (_rndRecipieUri != null && this.Tag.ToString() == "rnd")
                {
                    recepies.Add(_rndRecipieUri);
                    txt.Text = savedRecipie;                    
                }
            }     
        }
    }
}