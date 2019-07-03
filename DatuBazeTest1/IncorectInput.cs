using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;

namespace DatuBazeTest1
{
    class IncorectInput:Form1
    {
        public static Label erorrlabel = new Label();

        public static bool getInput( ComboBox comboBoxItems, ComboBox comboBoxOperations, TextBox textBoxTime, TextBox textBoxSetupTime, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;
            bool isWhiteSpace1 = textBoxTime.Text.Any(c => char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));
            bool isWhiteSpace2 = textBoxSetupTime.Text.Any(c => char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));

            bool isDigitPresent1 = textBoxTime.Text.Any(c => char.IsDigit(c));
            bool isDigitPresent2 = textBoxSetupTime.Text.Any(c => char.IsDigit(c));

            bool isWhiteSpace3 = comboBoxItems.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));
            bool isWhiteSpace4 = comboBoxOperations.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));

            if (isWhiteSpace3 || comboBoxItems.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxItems);
                return incorect = true;
            }
            else if (isWhiteSpace4 || comboBoxOperations.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxOperations);
                return incorect = true;
            }
            else if (isWhiteSpace1 || !isDigitPresent1 || textBoxTime.Text == "")
            {
                erorrlabel.Enabled = true;
                getSize(panel, textBoxTime);          
                return incorect = true;
            }
           
            else if (isWhiteSpace2 || !isDigitPresent2 || textBoxSetupTime.Text == "")
            {
                erorrlabel.Enabled = true;
                getSize(panel, textBoxSetupTime);            
                return incorect = true;
            }
           
            else if(erorrlabel.Enabled)
            {
                panel.Controls.RemoveAt(panel.Controls.Count-1);
                erorrlabel.Enabled = false;
            }
           
            return incorect;
        }
        public static bool getInput(ComboBox comboBoxItems, ComboBox comboBoxOperations, TextBox textBoxDuplicateNr, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;           
            bool isWhiteSpace1 = textBoxDuplicateNr.Text.Any(c => char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsSeparator(c) );
            bool isDigitPresent = textBoxDuplicateNr.Text.Any(c => char.IsDigit(c) );

                       
            bool isWhiteSpace3 = comboBoxItems.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));
            bool isWhiteSpace4 = comboBoxOperations.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));


            if (isWhiteSpace3 || comboBoxItems.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxItems);
                return incorect = true;
            }
            else if (isWhiteSpace4 || comboBoxOperations.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxOperations);
                return incorect = true;
            }
            else if (isWhiteSpace1 || !isDigitPresent || isDot(textBoxDuplicateNr.Text) || textBoxDuplicateNr.Text == "")
            {
                erorrlabel.Enabled = true;
                getSize(panel, textBoxDuplicateNr);
                return incorect = true;
            }
            else if (erorrlabel.Enabled)
            {
                erorrlabel.Enabled = false;
                panel.Controls.RemoveAt(panel.Controls.Count - 1);
                erorrlabel.Enabled = false;
            }

            return incorect;
        }
        public static bool getInputForConnection(ComboBox comboBoxItems, ComboBox comboBoxAllDuplicate, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;
           
            bool isWhiteSpace3 = comboBoxItems.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));
            bool isWhiteSpaceDuplicate = comboBoxAllDuplicate.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));

            if (isWhiteSpace3 || comboBoxItems.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxItems);
                return incorect = true;
            }
         
            else if (isWhiteSpaceDuplicate || comboBoxAllDuplicate.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxAllDuplicate);
                return incorect = true;
            }
            else if (erorrlabel.Enabled)
            {
                erorrlabel.Enabled = false;
                panel.Controls.RemoveAt(panel.Controls.Count - 1);
                erorrlabel.Enabled = false;
            }

            return incorect;
        }
        public static bool getInput(ComboBox comboBoxItems, TextBox textBoxDuplicateNr, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;
            bool isWhiteSpace1 = textBoxDuplicateNr.Text.Any(c => char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));
            bool isDigitPresent = textBoxDuplicateNr.Text.Any(c => char.IsDigit(c));

            bool isWhiteSpace3 = comboBoxItems.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));

            if (isWhiteSpace3 || comboBoxItems.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxItems);
                return incorect = true;
            }
           
            else if (isWhiteSpace1 || !isDigitPresent || textBoxDuplicateNr.Text == "")
            {
                erorrlabel.Enabled = true;
                getSize(panel, textBoxDuplicateNr);
                return incorect = true;
            }
            else if (erorrlabel.Enabled)
            {
                erorrlabel.Enabled = false;
                panel.Controls.RemoveAt(panel.Controls.Count - 1);
                erorrlabel.Enabled = false;
            }

            return incorect;
        }
        public static bool getInput(TextBox textBoxId, TextBox textBoxName, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;
            bool isWhiteSpace1 = textBoxId.Text.Any(c => char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));

            bool isWhiteSpace2 = textBoxName.Text.Any(c => char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));


            if (isWhiteSpace1 || textBoxId.Text == "")
            {
                erorrlabel.Enabled = true;
                getSize(panel, textBoxId);
                return incorect = true;
            }

            else if (isWhiteSpace2 || textBoxName.Text == "")
            {
                erorrlabel.Enabled = true;
                getSize(panel, textBoxName);
                return incorect = true;
            }
            else if (erorrlabel.Enabled)
            {
                erorrlabel.Enabled = false;
                panel.Controls.RemoveAt(panel.Controls.Count - 1);
                erorrlabel.Enabled = false;
            }

            return incorect;
        }
        public static bool getInput(ComboBox comboBoxItems, ComboBox comboBoxOperations, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;

            bool isWhiteSpace = comboBoxItems.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));
            bool isWhiteSpace2 = comboBoxOperations.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));
            if (isWhiteSpace || comboBoxItems.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxItems);
                return incorect = true;
            }
            else if (isWhiteSpace2 || comboBoxOperations.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxItems);
                return incorect = true;
            }
            else if (erorrlabel.Enabled)
            {
                erorrlabel.Enabled = false;
                panel.Controls.RemoveAt(panel.Controls.Count - 1);
                erorrlabel.Enabled = false;
            }

            return incorect;
        }
        public static bool getInput(ComboBox comboBoxItems, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;
           
            bool isWhiteSpace = comboBoxItems.Text.Any(c => char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));

            if (isWhiteSpace || comboBoxItems.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxItems);
                return incorect = true;
            }
            else if (erorrlabel.Enabled)
            {
                erorrlabel.Enabled = false;
                panel.Controls.RemoveAt(panel.Controls.Count - 1);
                erorrlabel.Enabled = false;
            }

            return incorect;
        }
        public static bool getInput(ComboBox comboBoxHour, ComboBox comboBoxMinutes, ComboBox comboBoxHour2, ComboBox comboBoxMinutes2, TextBox textBoxPauseName, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;

            if (comboBoxHour.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxHour);
                return incorect = true;
            }
            else if (comboBoxMinutes.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxMinutes);
                return incorect = true;
            }
            else if (comboBoxHour2.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxHour2);
                return incorect = true;
            }
            else if (comboBoxMinutes2.Text == "")
            {
                erorrlabel.Enabled = true;
                getSizeComboBox(panel, comboBoxMinutes2);
                return incorect = true;
            }
            else if (textBoxPauseName.Text == "")
            {
                erorrlabel.Enabled = true;
                getSize(panel, textBoxPauseName);
                return incorect = true;
            }
            else if (erorrlabel.Enabled)
            {
                erorrlabel.Enabled = false;
                panel.Controls.RemoveAt(panel.Controls.Count - 1);
                erorrlabel.Enabled = false;
            }

            return incorect;
        }
       
        public static bool getNameLength(TextBox textBoxName, Panel panel)
        {
            erorrlabel.Enabled = false;
            bool incorect = false;

            bool isWhiteSpace = textBoxName.Text.Any(c => char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c) || char.IsSeparator(c));

            if (isWhiteSpace || textBoxName.Text == "")
            {
                erorrlabel.Enabled = true;
                getSize(panel, textBoxName);
                return incorect = true;
            }
            else if (erorrlabel.Enabled)
            {
                erorrlabel.Enabled = false;
                panel.Controls.RemoveAt(panel.Controls.Count - 1);
                erorrlabel.Enabled = false;
            }

            return incorect;
        }
        private static void getSize(Panel panel, TextBox textBox)
        {
            erorrlabel.Enabled = true;
            erorrlabel.Location = new Point((textBox.Location.X) - 1, (textBox.Location.Y) - 1);
            erorrlabel.Size = new System.Drawing.Size((textBox.Width) + 2, (textBox.Height) + 2);
            erorrlabel.BackColor = Color.Red;
            panel.Controls.Add(erorrlabel);
        }
        private static void getSizeComboBox(Panel panel, ComboBox comboBox)
        {
            erorrlabel.Enabled = true;
            erorrlabel.Location = new Point((comboBox.Location.X) - 1, (comboBox.Location.Y) - 1);
            erorrlabel.Size = new System.Drawing.Size((comboBox.Width) + 2, (comboBox.Height) + 2);
            erorrlabel.BackColor = Color.Red;
            panel.Controls.Add(erorrlabel);
        }
        public static bool checkIfIsInDB(ComboBox comboBox)
        {
            bool check = false;
           if(comboBox.SelectedValue == null)
            {
                check = true;
            }
                return check;
        }
        private static bool isDot(string text)
        {
            bool isDot = false;
            char[] textArray = text.ToCharArray();
            for (int i = 0; i < textArray.Length; i++ )
            {
                if (textArray[i].ToString() == "."|| textArray[0].ToString() == "," || textArray[textArray.Length-1].ToString() == ",")
                {
                    isDot = true;
                }
            }
            return isDot;
        }
    }
}
