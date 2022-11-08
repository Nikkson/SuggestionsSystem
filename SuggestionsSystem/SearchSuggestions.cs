using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuggestionsSystem
{
    public static class SearchSuggestions
    {
        static string filename;
        static PropertyInfo InputProperty;
        static PropertyInfo TextBoxFocusProperty;
        static ObservableCollection<string> ResultProperty { get; set; }
        static ObservableCollection<string> SuggestionsProperty { get; set; }
        //public SearchSuggestions()
        //{
        //    filename = null;
        //    //InputProperty = null;
        //    ResultProperty = null;
        //}
        //public SearchSuggestions(object targetVM, string filename, string inputPropertyName, string textBoxFocusProperty, ObservableCollection<string> resultProperty, ObservableCollection<string> suggestionsProperty)
        //{
        //    this.filename = filename;
        //    InputProperty = targetVM.GetType().GetProperty(inputPropertyName);
        //    TextBoxFocusProperty = targetVM.GetType().GetProperty(textBoxFocusProperty);
        //    ResultProperty = resultProperty;
        //    SuggestionsProperty = suggestionsProperty;
        //}
        //public SearchSuggestions(string filename, string inputProperty, ObservableCollection<string> resultProperty)
        //{
        //    this.filename = filename;
        //    InputProperty = inputProperty;
        //    ResultProperty = resultProperty;
        //}
        //public void Test()
        //{
        //    if (ResultProperty.Count()>0) { 
        //        try { 
        //            File.AppendAllText(@"Suggestions\" + filename, InputProperty+"\n");
        //        }
        //        catch (DirectoryNotFoundException) { 
        //            Directory.CreateDirectory("Suggestions");
        //            File.AppendAllText(@"Suggestions\" + filename, InputProperty+"\n");
        //        }
                
        //    }
            
        //}
        public static void TrySaveSuggestion(object targetVM)
        {
            if (ResultProperty.Count() > 0)
            {
                try
                {
                    File.AppendAllText(@"Suggestions\" + filename, InputProperty.GetValue(targetVM).ToString() + "\n");
                }
                catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory("Suggestions");
                    File.AppendAllText(@"Suggestions\" + filename, InputProperty.GetValue(targetVM).ToString() + "\n");
                }

            }
        }
        public static void SwitchContext(object targetVM, string filename, string inputPropertyName, string textBoxFocusProperty, ObservableCollection<string> resultProperty, ObservableCollection<string> suggestionsProperty)
        {
            SearchSuggestions.filename = filename;
            InputProperty = targetVM.GetType().GetProperty(inputPropertyName);
            TextBoxFocusProperty = targetVM.GetType().GetProperty(textBoxFocusProperty);
            ResultProperty = resultProperty;
            SuggestionsProperty = suggestionsProperty;
        }
        public static void Suggest(object targetVM)
        {
            SuggestionsProperty.Clear();
            try
            {
                IEnumerable<string> lines = File.ReadAllLines(@"Suggestions\" + filename).Reverse();
                foreach (string str in lines)
                {
                    //try
                    //{
                    if (InputProperty.GetValue(targetVM) == null)
                    {
                        if (SuggestionsProperty.Count < 5 && !SuggestionsProperty.Contains(str))
                        {
                            SuggestionsProperty.Add(str);
                        }
                        else { break; }
                    }
                    else 
                    {
                        if (str.Contains(InputProperty.GetValue(targetVM).ToString()) &&
                    /*!InputPropertyProperty.GetValue(targetVM).ToString().Equals("") &&*/
                    !SuggestionsProperty.Contains(str))
                        {
                            if (SuggestionsProperty.Count < 5)
                            {
                                SuggestionsProperty.Add(str);
                            }
                            else { break; }

                        }
                    }
                    
                    //}
                }
            } catch (FileNotFoundException) { }
            
        }
        public static void Select(object targetVM, string value)
        {
            if (value != null)
            {
                InputProperty.SetValue(targetVM, value);
                FocusHelper.SetFocus(targetVM, TextBoxFocusProperty.Name);
            }
            
        }
    }
}
