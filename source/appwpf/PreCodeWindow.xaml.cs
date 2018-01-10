/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
using System;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FiftyEightBits.PreCode
{
    /// <summary>
    /// Interaction logic for PreCodeWindow.xaml
    /// </summary>
    public partial class PreCodeWindow : Window
    {

        private Mode applicationMode;
        private const int TAB_SIZE = 4;
        public enum Mode
        {
            WLW,
            StandAlone
        } ;

        /// <summary>
        /// Public Code property used to return formatted code to Windows Live Writer or the Clipboard.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Our strategy interface for user settings, Windows Live Writer, or WinForms/WPF
        /// </summary>
        private readonly IPreCodeSettings userSettings;

     
        /// <summary>
        /// PreCodeWindow
        /// </summary>
        public PreCodeWindow()
        {
            //No Different to loading from the Window.Resources section - although this was a test for
            //Special settings (since we can't use App.Xaml in a WPF app that is also compiled as a class library)
            //Resources.MergedDictionaries.Add(new ResourceDictionary { { "SpecialSettingsKey", new SpecialSettings() } });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Themes/Black.xaml") });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Styles/BuilderStyles.xaml") });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Styles/LibraryStyles.xaml") });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Styles/ExpanderStyle.xaml") });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Styles/ScrollViewer.xaml") });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Styles/TextBoxBase.xaml") });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Styles/CheckBoxStyle.xaml") });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Styles/ComboBoxStyle.xaml") });
            //Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/FiftyEightBits.PreCode;component/Styles/ToolBarButtons.xaml") });
        }

        /// <summary>
        /// Constructor that takes IPreCodeSettings and Mode
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="mode"></param>
        public PreCodeWindow(IPreCodeSettings settings, Mode mode)
            : this()
        {
            userSettings = settings;
            applicationMode = mode;

            InitializeComponent();
        }


        /// <summary>
        /// OnInitialized
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            ComboBox_HighLighterClass.ItemsSource = GetCodeClassList();
            
            if (applicationMode == Mode.StandAlone)
            {
                Button_Ok.Content = "Copy to Clipboard and Close";
                Button_Ok.Padding = new Thickness(5, 4, 5, 4);
            }
            else
            {
                Button_Ok.Content = "Ok";
                Button_Ok.Padding = new Thickness(20, 4, 20, 4);
            }

            Settings_Load();
        }

        /// <summary>
        /// Enable drag for the Window (from locations other than the menu or borders). Useful
        /// for borderless and custom shape windows.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);

            DragMove();
        }

        /// <summary>
        /// OnClosing
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            Settings_Save();
        }


        /// <summary>
        /// Button_Ok_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TextBox_Code.Text))
            {
                //Clean tabs out
                SwapTabsForSpaces();

                Code = TextBox_Code.Text;

                //Encode
                if (CheckBox_HtmlEncode.IsChecked.HasValue && CheckBox_HtmlEncode.IsChecked.Value)
                    Code = HttpUtility.HtmlEncode(Code);

                //Swap line endings for </br>
                if (CheckBox_LineEndings.IsChecked.HasValue && CheckBox_LineEndings.IsChecked.Value)
                    Code = Code.Replace(Environment.NewLine, "<br />");

                //Do the rest...
                if (ComboBox_SurroundWith.SelectedIndex != 0)
                {
                    switch (ComboBox_SurroundWith.SelectedIndex)
                    {
                        case 1:
                            if (ComboBox_HighLighterClass.SelectedIndex == 0)
                            {
                                Code = String.Format("<pre>\n\r{0}\n\r</pre>", Code);
                            }
                            else
                            {
                                Code = String.Format("<pre class=\"{0}\">\n{1}\n</pre>", GetClassOptions(), Code);
                            }
                            break;
                        case 2:
                            if (ComboBox_HighLighterClass.SelectedIndex == 0)
                            {
                                Code = String.Format("<textarea rows=\"{0}\">\n{1}\n</textarea>", TextBox_Code.LineCount,
                                                     Code);
                            }
                            else
                            {
                                Code = String.Format("<textarea class=\"{0}\" rows=\"{1}\">\n{2}\n</textarea>",
                                                     GetClassOptions(), TextBox_Code.LineCount, Code);

                            }
                            break;
                        case 3:
                            Code = String.Format("<blockquote>\n{0}\n</blockquote>", Code);
                            break;
                    }
                }

                DialogResult = true;
            }

            Close();
        }

        /// <summary>
        /// Button_Cancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        
        /// <summary>
        /// Button_FixIndentation_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_FixIndentation_Click(object sender, RoutedEventArgs e)
        {
            SwapTabsForSpaces();

            if (TextBox_Code.LineCount > 1)
            {
                string[] lines = ToArray(TextBox_Code.Text);
                int firsLineOffset = GetLeadingWhiteSpaceCount(lines[0]);
                if (firsLineOffset > 0)
                    DeDent(firsLineOffset);

                lines = ToArray(TextBox_Code.Text);
                int lastLineOffset = GetLeadingWhiteSpaceCount(lines[lines.Length - 1]);
                if (lastLineOffset > 0)
                    DeDent(lastLineOffset);
            }

            TextBox_Code.Focus();
        }

        /// <summary>
        /// Button_InDent_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_InDent_Click(object sender, RoutedEventArgs e)
        {
            bool isFirstLine = true;
            int firstLine = 0, lastLine = 0;
            int savedSelectionLength = TextBox_Code.SelectionLength;
            int newSelectionStart = TextBox_Code.SelectionStart;
            int charCount = 0;

            SwapTabsForSpaces();

            string[] lines = ToArray(TextBox_Code.Text);

            GetLineRange(lines, ref firstLine, ref lastLine);

            for (int i = 0; i < lines.Length; i++)
            {
                if (i >= firstLine && i <= lastLine)
                {
                    lines[i] = " ".PadLeft(TAB_SIZE) + lines[i];
                    charCount += lines[i].Length + Environment.NewLine.Length;
                    if (isFirstLine)
                    {
                        newSelectionStart = (charCount - (lines[i].Length + Environment.NewLine.Length)) + GetLeadingWhiteSpaceCount(lines[i]);
                        isFirstLine = false;
                    }
                    else
                    {
                        savedSelectionLength += TAB_SIZE;
                    }
                }
                else
                {
                    charCount += lines[i].Length + Environment.NewLine.Length;
                }
            }

            TextBox_Code.Text = ToText(lines);
            TextBox_Code.SelectionStart = newSelectionStart;
            TextBox_Code.SelectionLength = savedSelectionLength;
            TextBox_Code.Focus();
        }

        /// <summary>
        /// Button_DeDent_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_DeDent_Click(object sender, RoutedEventArgs e)
        {
            bool isFirstLine = true;
            int dedentedAmount = 0;
            int firstLine = 0, lastLine = 0;
            int savedSelectionLength = TextBox_Code.SelectionLength;
            int newSelectionStart = TextBox_Code.SelectionStart;
            int charCount = 0;

            SwapTabsForSpaces();

            string[] lines = ToArray(TextBox_Code.Text);

            GetLineRange(lines, ref firstLine, ref lastLine);

            for (int i = 0; i < lines.Length; i++)
            {
                if (i >= firstLine && i <= lastLine)
                {
                    int leadingCount = GetLeadingWhiteSpaceCount(lines[i]);

                    if (leadingCount >= TAB_SIZE)
                    {
                        lines[i] = lines[i].Substring(TAB_SIZE);
                        dedentedAmount = TAB_SIZE;
                    }
                    else if (leadingCount > 0)
                    {
                        lines[i] = lines[i].Substring(leadingCount);
                        dedentedAmount = leadingCount;
                    }

                    charCount += lines[i].Length + Environment.NewLine.Length;

                    if (isFirstLine)
                    {
                        newSelectionStart = (charCount - (lines[i].Length + Environment.NewLine.Length)) + GetLeadingWhiteSpaceCount(lines[i]);
                        isFirstLine = false;
                    }
                    else
                    {
                        if (savedSelectionLength > dedentedAmount)
                        {
                            savedSelectionLength -= dedentedAmount;
                        }

                    }

                    dedentedAmount = 0;
                }
                else
                {
                    charCount += lines[i].Length + Environment.NewLine.Length;
                }

            }

            TextBox_Code.Text = ToText(lines);
            TextBox_Code.SelectionStart = newSelectionStart;
            TextBox_Code.SelectionLength = savedSelectionLength;
            TextBox_Code.Focus();
        }


        /// <summary>
        /// ComboBox_SurroundWith_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SurroundWith_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SetSyntaxOptionsPanel();
        }


        /// <summary>
        /// Button_CheckForUpdate_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CheckForUpdate_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpdateWindow {Owner = this};
            window.ShowDialog();
        }


        /// <summary>
        /// Button_About_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_About_Click(object sender, RoutedEventArgs e)
        {
            var window = new AboutWindow {Owner = this};
            window.ShowDialog();
        }

        
        /// <summary>
        /// TextBox_Code_PreviewKeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Code_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                //CaretIndex resets to zero after the insert - so we need to store the value here.
                int position = TextBox_Code.CaretIndex;
                TextBox_Code.Text = TextBox_Code.Text.Insert(position, String.Empty.PadLeft(TAB_SIZE));
                TextBox_Code.CaretIndex = position + TAB_SIZE;
                e.Handled = true;


            }
        }

        /// <summary>
        /// Helper method to get current line, or line range of current selection
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        private void GetLineRange(string[] lines, ref int first, ref int last)
        {
            try
            {
                bool foundFirst = false;
                bool foundLast = false;
                int startPosition = TextBox_Code.SelectionStart;
                int stopPosition = startPosition + TextBox_Code.SelectionLength;
                int charCount = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    charCount += lines[i].Length + Environment.NewLine.Length; //Add two bytes for line endings
                    if (startPosition < charCount && !foundFirst)
                    {
                        first = i;
                        foundFirst = true;
                    }

                    if (stopPosition < charCount && !foundLast)
                    {
                        last = i;
                        foundLast = true;
                    }

                    if (foundFirst && foundLast)
                        break;
                }
            }
            catch (Exception ex)
            {
                HandleException("GetLineRange()", ex);
            }
        }

        /// <summary>
        /// Determine the amount of whitespace at the begining of a line/string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int GetLeadingWhiteSpaceCount(string input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException("input");

                char[] line = input.ToCharArray();
                int count = 0;
                foreach (char c in line)
                {
                    if (Char.IsWhiteSpace(c))
                        count++;
                    else
                        break;
                }

                return count;
            }
            catch (Exception ex)
            {
                HandleException("GetLeadingWhiteSpaceCount()", ex);
                return 0;
            }
        }

        /// <summary>
        /// Reduce the indent of all lines in <see cref="TextBox_Code"/>.
        /// </summary>
        /// <param name="amount"></param>
        private void DeDent(int amount)
        {
            try
            {
                string[] lines = ToArray(TextBox_Code.Text);
                for (int i = 0; i < lines.Length; i++)
                {
                    int currentOffset = GetLeadingWhiteSpaceCount(lines[i]);
                    if (currentOffset >= amount)
                        lines[i] = lines[i].Substring(amount);
                }

                TextBox_Code.Text = ToText(lines);
            }
            catch (Exception ex)
            {
                HandleException("DeDent()", ex);
            }
        }

        /// <summary>
        /// Helper method to replace tabs for spaces
        /// </summary>
        private void SwapTabsForSpaces()
        {
            // swap any tab whitespaces for spaces since amount will always be in spaces.
            TextBox_Code.Text = TextBox_Code.Text.Replace("\t", " ".PadLeft(TAB_SIZE));
        }


        /// <summary>
        /// Helper method to convert the <see cref="TextBox_Code"/> text to an array of lines. We're
        /// emulating the old Winforms text box - although working with lines is also a handy way to 
        /// fix indentation.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] ToArray(string text)
        {
            if (String.IsNullOrEmpty(text) == false)
                return text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            else
            {
                return new string[0];
            }
        }


        /// <summary>
        /// Helper method to convert back to text from the array of lines.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string ToText(string[] lines)
        {
            if (lines != null && lines.Length > 0)
            {
                var sb = new StringBuilder(lines.Length * 200);

                for (int i = 0; i < lines.Length; i++ )
                {
                    if (i < lines.Length - 1)
                        sb.AppendLine(lines[i]);
                    else
                    {
                        sb.Append(lines[i]);
                    }
                }
                
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// Helper method to build the formatted class attributes.
        /// </summary>
        /// <returns></returns>
        private string GetClassOptions()
        {
            var sb = new StringBuilder();

            sb.Append("brush: ");

            sb.Append(((SyntaxClass)ComboBox_HighLighterClass.SelectedItem).ClassAttribute);

            sb.Append(";");

            if (CheckBox_ShowRuler.IsChecked.Value)
                sb.Append(" ruler: true;");

            if (CheckBox_DoNotShowGutter.IsChecked.Value)
                sb.Append(" gutter: false;");
            
            if (CheckBox_DoNotShowToolBar.IsChecked.Value)
                sb.Append(" toolbar: false;");

            if (CheckBox_CollapseBlock.IsChecked.Value)
                sb.Append(" collapse: true;");

            if (CheckBox_DisableAutoLinks.IsChecked.Value)
                sb.Append(" auto-links: false;");

            if (CheckBox_TurnOffSmartTabs.IsChecked.Value)
                sb.Append(" smart-tabs: false;");

            int startLine = Int32.Parse(TextBox_LineCountStart.Text);
            if (startLine > 1)
                sb.AppendFormat(" first-line: {0};", startLine);

            if (!String.IsNullOrEmpty(TextBox_HighLightLines.Text.Trim()))
                sb.AppendFormat(" highlight: [{0}];", TextBox_HighLightLines.Text.Trim());

            return sb.ToString();
        }


        /// <summary>
        /// Load user settings
        /// </summary>
        private void Settings_Load()
        {
            try
            {
                ComboBox_HighLighterClass.SelectedIndex = ComboBox_HighLighterClass.Items.IndexOf(((SyntaxClassCollection)ComboBox_HighLighterClass.ItemsSource)[userSettings.SyntaxClassAttribute]);
                
                ComboBox_SurroundWith.SelectedIndex = userSettings.SurroundWith;
                
                CheckBox_HtmlEncode.IsChecked = userSettings.HtmlEncode;
                
                CheckBox_LineEndings.IsChecked = userSettings.UseBrs;

                SetSyntaxOptionsPanel();
                
                CheckBox_DoNotShowGutter.IsChecked = userSettings.DoNotDisplayGutter;
               
                CheckBox_DoNotShowToolBar.IsChecked = userSettings.DoNotShowToolBar;
               
                CheckBox_CollapseBlock.IsChecked = userSettings.CollapseBlock;
               
                CheckBox_ShowRuler.IsChecked = userSettings.ShowRuler;
               
                CheckBox_DisableAutoLinks.IsChecked = userSettings.DoNotAutoLink;
                
                CheckBox_TurnOffSmartTabs.IsChecked = userSettings.DoNotUseSmartTabs;
               
                TextBox_LineCountStart.Text = userSettings.LineCountStart.ToString();
            }
            catch (Exception ex)
            {
                HandleException("LoadSettings()", ex);
            }
        }

        private void SetSyntaxOptionsPanel()
        {
            if (ComboBox_SurroundWith.SelectedIndex == 0)
                StackPanel_SyntaxOptions.IsEnabled = false;
            else
                StackPanel_SyntaxOptions.IsEnabled = true;
        }


        /// <summary>
        /// Save user settings
        /// </summary>
        private void Settings_Save()
        {
            try
            {
                userSettings.SurroundWith = ComboBox_SurroundWith.SelectedIndex;
                
                userSettings.HtmlEncode = CheckBox_HtmlEncode.IsChecked.Value;
                
                userSettings.UseBrs = CheckBox_LineEndings.IsChecked.Value;

                var selected = ComboBox_HighLighterClass.SelectedItem as SyntaxClass;
                if (selected != null)
                    userSettings.SyntaxClassAttribute = selected.ClassAttribute;

                userSettings.DoNotDisplayGutter = CheckBox_DoNotShowGutter.IsChecked.Value;

                userSettings.DoNotShowToolBar = CheckBox_DoNotShowToolBar.IsChecked.Value;

                userSettings.CollapseBlock = CheckBox_CollapseBlock.IsChecked.Value;

                userSettings.ShowRuler = CheckBox_ShowRuler.IsChecked.Value;

                userSettings.DoNotAutoLink = CheckBox_DisableAutoLinks.IsChecked.Value;

                userSettings.DoNotUseSmartTabs = CheckBox_TurnOffSmartTabs.IsChecked.Value;

                userSettings.LineCountStart = Int32.Parse(TextBox_LineCountStart.Text);

            }
            catch (Exception ex)
            {
                HandleException("SaveSettings()", ex);
            }
        }

        private static void HandleException(string member, Exception ex)
        {
            MessageBox.Show(String.Format("We're sorry but an error occurred in the PreCode plugin at {0}. Error message: {1}", member, ex.Message));
        }

        /// <summary>
        /// Helper method to create a collection of syntaxhighlighter code/language names and alias attributes
        /// </summary>
        /// <returns></returns>
        private SyntaxClassCollection GetCodeClassList()
        {
            SyntaxClassCollection items = new SyntaxClassCollection();
            items.Add(new SyntaxClass("None", "none"));
            items.Add(new SyntaxClass("Bash", "bash"));
            items.Add(new SyntaxClass("C++", "cpp"));
            items.Add(new SyntaxClass("C#", "csharp"));
            items.Add(new SyntaxClass("CSS", "css"));
            items.Add(new SyntaxClass("Delphi", "delphi"));
            items.Add(new SyntaxClass("Diff", "diff"));
            items.Add(new SyntaxClass("Erlang", "erl"));
            items.Add(new SyntaxClass("F#", "fsharp"));
            items.Add(new SyntaxClass("Groovy", "groovy"));
            items.Add(new SyntaxClass("Java", "java"));
            items.Add(new SyntaxClass("JavaScript", "js"));
            items.Add(new SyntaxClass("Perl", "perl"));
            items.Add(new SyntaxClass("PHP", "php"));
            items.Add(new SyntaxClass("Plain Text", "plain"));
            items.Add(new SyntaxClass("PowerShell", "ps"));
            items.Add(new SyntaxClass("Python", "py"));
            items.Add(new SyntaxClass("Ruby", "ruby"));
            items.Add(new SyntaxClass("Scala", "scala"));
            items.Add(new SyntaxClass("SQL", "sql"));
            items.Add(new SyntaxClass("Visual Basic", "vb"));
            items.Add(new SyntaxClass("XML/HTML", "xml"));

            return items;
        }
    }
}
