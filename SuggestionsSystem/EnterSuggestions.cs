using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SuggestionsSystem
{
    public class EnterSuggestions
    {
        string filename;
        PropertyInfo InputProperty;
        PropertyInfo FocusProperty;
        ObservableCollection<string> SuggestionsProperty { get; set; }

        public EnterSuggestions(string filename)
        {
            this.filename = filename;
        }

        public void SwitchContext(object targetVM, object model, string inputPropertyName, string focusPropertyName, ObservableCollection<string> suggestionsProperty)
        {
            InputProperty = model.GetType().GetProperty(inputPropertyName);
            FocusProperty = targetVM.GetType().GetProperty(focusPropertyName);
            SuggestionsProperty = suggestionsProperty;
        }
        public void SwitchContext(object targetVM, string inputPropertyName)
        {
            InputProperty = targetVM.GetType().GetProperty(inputPropertyName);
        }
        public void SetSuggestionsProperty(ObservableCollection<string> suggestionsProperty)
        {
            SuggestionsProperty = suggestionsProperty;
        }
        public void TrySaveSuggestion(object model, Func<string, bool> validation, string modelPropertyToValidate)
        {
            try
            {
                string suggestionString = "";
                PropertyInfo[] properties = model.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.Equals(modelPropertyToValidate))
                    {
                        if (!validation(property.GetValue(model).ToString()))
                        {
                            MessageBox.Show("Validation failed!");
                            return;
                        }
                    }
                    suggestionString = suggestionString + property.Name + ":" + property.GetValue(model) + "#";
                }
                File.AppendAllText(@"Suggestions\" + filename, suggestionString + "\n");
            } catch (System.NullReferenceException e) { }
            
        }
        public void SuggestAll(IEnumerable<string> propertiesNotToShow)
        {
            SuggestionsProperty.Clear();
            try
            {
                IEnumerable<string> lines = File.ReadAllLines(@"Suggestions\" + filename).Reverse();
                foreach (string str in lines)
                {
                    
                        if (SuggestionsProperty.Count < 5)
                        {
                            string cleanString = str;
                            foreach (string propertyToHide in propertiesNotToShow)
                            {
                                Regex hidePropertyRegex = new Regex(propertyToHide + @":(.+?)#");
                                Match hideMatch = hidePropertyRegex.Matches(str)[0];
                                string hideStr = hideMatch.Groups[0].ToString();
                                cleanString = cleanString.Replace(hideStr, "");
                            }
                            string newStr = cleanString.Replace('#', ' ');
                            SuggestionsProperty.Add(newStr);
                        }
                        else { break; }

                    
                }
            }
            catch (FileNotFoundException) { }
        }
        public void Suggest(object model, IEnumerable<string> propertiesNotToShow)
        {
            SuggestionsProperty.Clear();
            string currentPropertyName = InputProperty.Name;
            Regex regex = new Regex(currentPropertyName + @":(.+?)#");
            try
            {
                IEnumerable<string> lines = File.ReadAllLines(@"Suggestions\" + filename).Reverse();
                foreach (string str in lines)
                {
                    Match match = regex.Matches(str)[0];
                    string foundPropertyValue = match.Groups[1].ToString();
                    if (foundPropertyValue.Contains(InputProperty.GetValue(model).ToString()) &&
                        !InputProperty.GetValue(model).ToString().Equals("") &&
                        !SuggestionsProperty.Contains(str))
                    {
                        if (SuggestionsProperty.Count < 5)
                        {
                            string cleanString = str;
                            foreach (string propertyToHide in propertiesNotToShow)
                            {
                                Regex hidePropertyRegex = new Regex(propertyToHide + @":(.+?)#");
                                Match hideMatch = hidePropertyRegex.Matches(str)[0];
                                string hideStr = hideMatch.Groups[0].ToString();
                                cleanString = cleanString.Replace(hideStr, "");
                            }
                            string newStr = cleanString.Replace('#', ' ');
                            SuggestionsProperty.Add(newStr);
                        }
                        else { break; }

                    }
                }
            }
            catch (FileNotFoundException) { }

        }
        public void Select(object targetVM, object model, string value)
        {
            if (value!=null)
            {
                char[] separators = { ' ' };
                string[] splitValue = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                IEnumerable<string> lines = File.ReadAllLines(@"Suggestions\" + filename).Reverse();
                string fullObject = null;
                foreach (string objectString in lines)
                {
                    bool isCorrectObject = true;
                    foreach (string partOfObject in splitValue)
                    {
                        if (!objectString.Contains(partOfObject))
                        {
                            isCorrectObject = false;
                            break;
                        }
                    }
                    if (isCorrectObject)
                    {
                        fullObject = objectString.Replace(" ", " #");
                        break;
                    }
                }

                PropertyInfo[] properties = model.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Regex regex = new Regex(property.Name + @":(.+?)#");
                    Match match = regex.Matches(fullObject)[0];
                    string propertyValue = match.Groups[1].ToString();
                    try
                    {
                        property.SetValue(model, propertyValue);
                    }
                    catch (ArgumentException)
                    {
                        int intVal = Int32.Parse(propertyValue);
                        property.SetValue(model, intVal);
                    }

                }
                FocusHelper.SetFocus(targetVM, FocusProperty.Name);
            }
            
        }
    }
}
