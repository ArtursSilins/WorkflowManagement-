using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Threading;
using System.Deployment.Application;
using System.IO;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using Autofac;


namespace DatuBazeTest1
{
    public delegate void LabelSliderDelegate(int counter, int fadingSpeed);
    public delegate void LabelStartPositionDelegate();
    public partial class Form1 : Form
    {
        LabelSliderDelegate LabelSliderDelegate;
        LabelStartPositionDelegate LabelStartPositionDelegate;

        bool labelRed = false;
        public const int mw = 0xA1;
        public const int ht = 0x2;
        public string ItemId { get; set; }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern int ReleaseCapture();
        //@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Initial Catalog = Database1;Integrated Security=True";
        // @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
        // @"Data Source=(LocalDB)\MSSQLLocalDB;Database=Database1;Integrated Security=True";
        public static Graphics graphic;
        public static SqlConnection connection;
        public static string connectionString;

        public Autofac.IContainer container = ContainerConfig.Configure();

        public Form1()
        {
            InitializeComponent();

            LabelSliderDelegate = new LabelSliderDelegate(positionChange);
            LabelStartPositionDelegate = new LabelStartPositionDelegate(labelStartPosition);

            this.comboBoxHour.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxMinutes.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxHour2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxMinutes2.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            radiobuttonAddConnection();
            radiobuttonConnection();

            radioButtonDeleteDuplicateYes.Enabled = false;
            radioButtonDeleteDuplicateNo.Enabled = false;

            comboBoxHour.Items.AddRange(Domain.PauseDropLists.PauseTimeSet.Hour());
            comboBoxMinutes.Items.AddRange(Domain.PauseDropLists.PauseTimeSet.Minutes());
            comboBoxHour2.Items.AddRange(Domain.PauseDropLists.PauseTimeSet.Hour());
            comboBoxMinutes2.Items.AddRange(Domain.PauseDropLists.PauseTimeSet.Minutes());

            try
            {

                Domain.View.All All = container.Resolve<Domain.View.All>();

                dataGridViewPause.DataSource = All.PauseView();
                dataGridViewPause.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewPause.BackgroundColor = Color.White;
                dataGridViewPause.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                dataGridViewPause.AutoResizeColumns();

                if (All.Items().Rows.Count != 0)
                {
                    comboBoxItems.ValueMember = "Id";
                    comboBoxItems.DisplayMember = "Name";
                    comboBoxItems.DataSource = All.Items();
                }
                else
                {
                    comboBoxItems.DataSource = null;
                    comboBoxOperations.DataSource = null;
                }

                ComboboxDropListItems(comboBoxAllOperations, All.Operations());

                ComboboxDropListItems(comboBoxDuplicate, All.DuplicateName());

                ComboboxDropListItems(comboBoxAllDuplicate, All.DuplicateName());

                checkBoxDuplicate.Checked = false;


                Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                if (itemsWithOperations.Items().Rows.Count != 0)
                {
                    comboBoxItems2.ValueMember = "Id";
                    comboBoxItems2.DisplayMember = "Name";
                    comboBoxItems2.DataSource = itemsWithOperations.Items();
                }
                else
                {
                    comboBoxItems2.DataSource = null;
                    comboBoxOperations2.DataSource = null;
                }

                ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                try
                {
                    if (ItemForGrid.View(comboBoxItems.SelectedValue.ToString()).Rows.Count == 0)
                    {

                    }
                    else
                    {
                        dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView2.BackgroundColor = Color.White;
                        dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                        dataGridView2.AutoResizeColumns();
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {

                }

                Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = container.Resolve<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();
                itemsOperationsForTrueFalseCheck.Checking();

                Domain.ArrangeInLineTable.ElapsedInLineTimes elapsedInLineTimes = container.Resolve<Domain.ArrangeInLineTable.ElapsedInLineTimes>();
                elapsedInLineTimes.DeleteEndTimes();

                Domain.ArrangeInLineTable.SortInLineTableNr sortInLineTableNr = container.Resolve<Domain.ArrangeInLineTable.SortInLineTableNr>();
                sortInLineTableNr.ArrangeAllInLineNr();

                Domain.ArrangeInLineTable.InLineFirstNr inLineFirstNr = container.Resolve<Domain.ArrangeInLineTable.InLineFirstNr>();
                inLineFirstNr.ArrangeNr();

                elapsedInLineTimes.DeleteStartTimes();
            }
            catch (System.NullReferenceException)
            {


            }

        }

        private void buttonAddItem_Click(object sender, EventArgs e)
        {

            if (IncorectInput.getInput(textBoxId, textBoxName, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }

            Domain.Checking.CheckIfExists check = container.Resolve<Domain.Checking.CheckIfExists>();
            if (!check.IfItemExists(textBoxId.Text))
            {

                var addNew = container.Resolve<Domain.Adding.AddNew>();
                addNew.ItemToDisplay(textBoxId.Text, textBoxName.Text);

                addNew.AddItem(textBoxId.Text, textBoxName.Text);


                var addAll = new Repository.Retrieve.All();
                Domain.View.All getAll = new Domain.View.All(addAll);
                comboBoxItems.ValueMember = "Id";
                comboBoxItems.DisplayMember = "Name";
                comboBoxItems.DataSource = getAll.Items();

                Label1.Text = "Item Added!";

                var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                labelSliderThread.Start();
            }
            else
            {
                MessageBox.Show("Id already exist!", "Message", MessageBoxButtons.OK);
            }
        }

        private void buttonAddOperation_Click(object sender, EventArgs e)
        {

            if (IncorectInput.getInput(textBoxId, textBoxName, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }

            Domain.Checking.CheckIfExists check = container.Resolve<Domain.Checking.CheckIfExists>();
            if (!check.IfOperationExists(textBoxId.Text))
            {

                Domain.Adding.AddNew addNew = container.Resolve<Domain.Adding.AddNew>();
                addNew.AddOperation(textBoxId.Text, textBoxName.Text);


                var addAll = new Repository.Retrieve.All();
                Domain.View.All getAll = new Domain.View.All(addAll);
                comboBoxAllOperations.ValueMember = "Id";
                comboBoxAllOperations.DisplayMember = "Name";
                comboBoxAllOperations.DataSource = getAll.Operations();


                Label1.Text = "Operation Added!";

                var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                labelSliderThread.Start();
            }
            else
            {
                MessageBox.Show("Id already exist!", "Message", MessageBoxButtons.OK);
            }

        }

        private void buttonAddConnection_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxItems, comboBoxAllOperations, textBoxTime, textBoxSetupTime, panel2))
            {
                labelRed = true;
                return;
            }
            else if (radioButton1.Checked)
            {
                if (IncorectInput.getInputForConnection(comboBoxItems, comboBoxAllDuplicate, panel2))
                {
                    labelRed = true;
                    return;
                }
                else
                {
                    if (labelRed)
                    {
                        panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                        labelRed = false;
                    }
                }
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxItems) || IncorectInput.checkIfIsInDB(comboBoxAllOperations))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }


            Domain.Checking.CheckIfConnectionExists checkIfConnectionExists = container.Resolve<Domain.Checking.CheckIfConnectionExists>();


            if (radioButton1.Checked)
            {
                if (!checkIfConnectionExists.CheckDuplicate(comboBoxItems.SelectedValue.ToString(), comboBoxAllDuplicate.SelectedValue.ToString()))
                {
                    Domain.Adding.ItemsOperationsConnection itemsOperationsConnection = container.Resolve<Domain.Adding.ItemsOperationsConnection>();
                    itemsOperationsConnection.AddWithDuplicate(comboBoxItems.SelectedValue.ToString(), comboBoxAllDuplicate.SelectedValue.ToString(), textBoxTime.Text, textBoxSetupTime.Text);

                    Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
                   
                    ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));

                    Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                    if (itemsWithOperations.Items().Rows.Count != 0)
                    {
                        comboBoxItems2.ValueMember = "Id";
                        comboBoxItems2.DisplayMember = "Name";
                        comboBoxItems2.DataSource = itemsWithOperations.Items();
                    }
                    else
                    {
                        comboBoxItems2.DataSource = null;
                        comboBoxOperations2.DataSource = null;
                    }

                    ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                    Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                    dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.BackgroundColor = Color.White;
                    dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                    dataGridView2.AutoResizeColumns();
                    dataGridView2.Refresh();

                    Label1.Text = "Connection Added!";

                    var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                    labelSliderThread.Start();
                }
                else
                {
                    MessageBox.Show("Connection already exist!", "Message", MessageBoxButtons.OK);
                }

            }
            else
            {
                if (!checkIfConnectionExists.Check(comboBoxItems.SelectedValue.ToString(), comboBoxAllOperations.SelectedValue.ToString()))
                {
                    Domain.Adding.ItemsOperationsConnection itemsOperationsConnection = container.Resolve<Domain.Adding.ItemsOperationsConnection>();
                    itemsOperationsConnection.Add(comboBoxItems.SelectedValue.ToString(), comboBoxAllOperations.SelectedValue.ToString(), textBoxTime.Text, textBoxSetupTime.Text);

                    Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
                  
                    ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));

                    Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                    if (itemsWithOperations.Items().Rows.Count != 0)
                    {
                        comboBoxItems2.ValueMember = "Id";
                        comboBoxItems2.DisplayMember = "Name";
                        comboBoxItems2.DataSource = itemsWithOperations.Items();
                    }
                    else
                    {
                        comboBoxItems2.DataSource = null;
                        comboBoxOperations2.DataSource = null;
                    }

                    ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                    Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                    dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.BackgroundColor = Color.White;
                    dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                    dataGridView2.AutoResizeColumns();
                    dataGridView2.Refresh();


                    Label1.Text = "Connection Added!";

                    var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                    labelSliderThread.Start();
                }
                else
                {
                    MessageBox.Show("Connection already exist!", "Message", MessageBoxButtons.OK);
                }


            }

        }

        private void buttonStartOneOperation_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxItems2, comboBoxOperations2, textBoxCount, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxItems2) || IncorectInput.checkIfIsInDB(comboBoxOperations2))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }
            try
            {
                Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck1 = container.Resolve<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();
                itemsOperationsForTrueFalseCheck1.Checking();

                Domain.ArrangeInLineTable.ElapsedInLineTimes elapsedInLineTimes = container.Resolve<Domain.ArrangeInLineTable.ElapsedInLineTimes>();
                elapsedInLineTimes.DeleteEndTimes();

                Domain.ArrangeInLineTable.SortInLineTableNr sortInLineTableNr = container.Resolve<Domain.ArrangeInLineTable.SortInLineTableNr>();
                sortInLineTableNr.ArrangeAllInLineNr();

                Domain.ArrangeInLineTable.InLineFirstNr inLineFirstNr = container.Resolve<Domain.ArrangeInLineTable.InLineFirstNr>();
                inLineFirstNr.ArrangeNr();

                elapsedInLineTimes.DeleteStartTimes();

                Domain.Checking.IsBusyCheck isBusyCheck = container.Resolve<Domain.Checking.IsBusyCheck>();
                if (!isBusyCheck.GetOperationBool(comboBoxOperations2.SelectedValue.ToString()))
                {

                    Domain.StartOneOperation.StartTime startTime = container.Resolve<Domain.StartOneOperation.StartTime>();
                    startTime.AddStartTime(comboBoxItems2.SelectedValue.ToString(), comboBoxOperations2.SelectedValue.ToString(), textBoxCount.Text);

                    Domain.IsOperationBusy.OperationCondition operationCondition = container.Resolve<Domain.IsOperationBusy.OperationCondition>();
                    operationCondition.OperationIsBusy(comboBoxItems2.SelectedValue.ToString(), comboBoxOperations2.SelectedValue.ToString());

                    Label1.Text = "Item added!";

                    var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                    labelSliderThread.Start();
                }
                else
                {
                    Domain.Checking.CheckIfExists check = container.Resolve<Domain.Checking.CheckIfExists>();

                    if (!check.IfInLineItemExists(comboBoxItems2.SelectedValue.ToString(), comboBoxOperations2.SelectedValue.ToString()))
                    {

                        Domain.StartOneOperation.StartTime startTime = container.Resolve<Domain.StartOneOperation.StartTime>();
                        startTime.AddInLineStartTime(comboBoxItems2.SelectedValue.ToString(), comboBoxOperations2.SelectedValue.ToString(), textBoxCount.Text);

                        Label1.Text = "Item added in line!";
                        var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                        labelSliderThread.Start();
                    }
                    else
                    {

                        Domain.StartOneOperation.StartTime startTime = container.Resolve<Domain.StartOneOperation.StartTime>();
                        startTime.AddInLineStartTimeIfEntryExist(comboBoxItems2.SelectedValue.ToString(), comboBoxOperations2.SelectedValue.ToString(), textBoxCount.Text);


                        Label1.Text = "Item added in line!";

                        var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                        labelSliderThread.Start();
                    }

                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }

            Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = container.Resolve<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();
            itemsOperationsForTrueFalseCheck.Checking();
        }

        private void buttonTime_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxItems, comboBoxOperations, textBoxTime, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxItems) || IncorectInput.checkIfIsInDB(comboBoxOperations))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }

            Domain.Adding.ChangeTime changeTime = container.Resolve<Domain.Adding.ChangeTime>();
            changeTime.SetTime(textBoxTime.Text, comboBoxItems.SelectedValue.ToString(), comboBoxOperations.SelectedValue.ToString());

            Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
            dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            dataGridView2.AutoResizeColumns();

            dataGridView2.Refresh();

            Label1.Text = "Time changed!";

            var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
            labelSliderThread.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();
            form2.Visible = true;

        }
        private void buttonDeletOperation_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxAllOperations, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxAllOperations))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }

            Domain.Checking.IsBusyCheck isBusyCheck = container.Resolve<Domain.Checking.IsBusyCheck>();
            if (isBusyCheck.GetOperationBool(comboBoxAllOperations.SelectedValue.ToString()))
            {
                MessageBox.Show("Can't delet while runing!", "Message", MessageBoxButtons.OK);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Do you want to delete: " + comboBoxAllOperations.Text + "?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {



                Domain.Delete.ItemsOperations itemsOperations = container.Resolve<Domain.Delete.ItemsOperations>();

                itemsOperations.DeleteOperation(comboBoxOperations.SelectedValue.ToString());

                Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
               
                ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));

                Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                if (itemsWithOperations.Items().Rows.Count != 0)
                {
                    comboBoxItems2.ValueMember = "Id";
                    comboBoxItems2.DisplayMember = "Name";
                    comboBoxItems2.DataSource = itemsWithOperations.Items();
                }
                else
                {
                    comboBoxItems2.DataSource = null;
                    comboBoxOperations2.DataSource = null;
                }

                ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                var addAll = new Repository.Retrieve.All();
                Domain.View.All getAll = new Domain.View.All(addAll);

                ComboboxDropListItems(comboBoxAllOperations, getAll.Operations());

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.BackgroundColor = Color.White;
                dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                dataGridView2.AutoResizeColumns();
                dataGridView2.Refresh();

            }

        }
        private void buttonDeletItem_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxItems, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxItems))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }

            DialogResult dialogResult = MessageBox.Show("Do you want to delete: " + comboBoxItems.Text + "?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {


                Domain.Delete.ItemsOperations itemsOperations = container.Resolve<Domain.Delete.ItemsOperations>();
                itemsOperations.DeleteItem(comboBoxItems.SelectedValue.ToString());

                Repository.Retrieve.All all = new Repository.Retrieve.All();
                Domain.View.All items = new Domain.View.All(all);

                if (items.Items().Rows.Count != 0)
                {
                    comboBoxItems.DisplayMember = "Name";
                    comboBoxItems.ValueMember = "Id";
                    comboBoxItems.DataSource = items.Items();
                }
                else
                {

                    comboBoxOperations.DataSource = null;
                    comboBoxItems.DataSource = null;

                }


                Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                if (itemsWithOperations.Items().Rows.Count != 0)
                {
                    comboBoxItems2.ValueMember = "Id";
                    comboBoxItems2.DisplayMember = "Name";
                    comboBoxItems2.DataSource = itemsWithOperations.Items();
                }
                else
                {
                    comboBoxItems2.DataSource = null;
                    comboBoxOperations2.DataSource = null;
                }

                ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.BackgroundColor = Color.White;
                dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                dataGridView2.AutoResizeColumns();
                dataGridView2.Refresh();

                Label1.Text = "Item deleted!";

                var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                labelSliderThread.Start();
            }


        }
        private void buttonDeleteConnection_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxItems, comboBoxOperations, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxItems) || IncorectInput.checkIfIsInDB(comboBoxOperations))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }

            Domain.Checking.IsBusyCheck isBusyCheck = container.Resolve<Domain.Checking.IsBusyCheck>();
            Domain.Delete.ItemsOperations itemsOperations = container.Resolve<Domain.Delete.ItemsOperations>();

            if (radioButtonDeleteDuplicateYes.Checked)
            {
                itemsOperations.DeleteDuplicateConnection(comboBoxItems.SelectedValue.ToString(), comboBoxAllDuplicate.SelectedValue.ToString());

                Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
              
                ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));

                Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                if (itemsWithOperations.Items().Rows.Count != 0)
                {
                    comboBoxItems2.ValueMember = "Id";
                    comboBoxItems2.DisplayMember = "Name";
                    comboBoxItems2.DataSource = itemsWithOperations.Items();
                }
                else
                {
                    comboBoxItems2.DataSource = null;
                    comboBoxOperations2.DataSource = null;
                }

                ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.BackgroundColor = Color.White;
                dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                dataGridView2.AutoResizeColumns();
                dataGridView2.Refresh();

                Label1.Text = "Connection deleted!";

                var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                labelSliderThread.Start();

            }
            else
            {
                if (isBusyCheck.GetItemBool(comboBoxItems.SelectedValue.ToString()))
                {
                    MessageBox.Show("Can't delete while runing!", "Message", MessageBoxButtons.OK);
                }
                else
                {
                                       
                    itemsOperations.DeleteConnection(comboBoxItems.SelectedValue.ToString(), comboBoxOperations.SelectedValue.ToString());

                    Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
                 
                    ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));

                    Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                    if (itemsWithOperations.Items().Rows.Count != 0)
                    {
                        comboBoxItems2.ValueMember = "Id";
                        comboBoxItems2.DisplayMember = "Name";
                        comboBoxItems2.DataSource = itemsWithOperations.Items();
                    }
                    else
                    {
                        comboBoxItems2.DataSource = null;
                        comboBoxOperations2.DataSource = null;
                    }

                    ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());
                }
                       

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.BackgroundColor = Color.White;
                dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                dataGridView2.AutoResizeColumns();
                dataGridView2.Refresh();

                Label1.Text = "Connection deleted!";

                var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                labelSliderThread.Start();

            }

        }
        
        private void comboBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {

            Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
          
            ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));

            Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
            dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            dataGridView2.AutoResizeColumns();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static void Move_Form1(IntPtr Handle, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, mw, ht, 0);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Move_Form1(Handle, e);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            label14.Visible = false;
            panel2.Visible = true;

        }

        private void display_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;

            Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = container.Resolve<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();
            itemsOperationsForTrueFalseCheck.Checking();

            Domain.ArrangeInLineTable.ElapsedInLineTimes elapsedInLineTimes = container.Resolve<Domain.ArrangeInLineTable.ElapsedInLineTimes>();
            elapsedInLineTimes.DeleteEndTimes();

            Domain.ArrangeInLineTable.SortInLineTableNr sortInLineTableNr = container.Resolve<Domain.ArrangeInLineTable.SortInLineTableNr>();
            sortInLineTableNr.ArrangeAllInLineNr();

            Domain.ArrangeInLineTable.InLineFirstNr inLineFirstNr = container.Resolve<Domain.ArrangeInLineTable.InLineFirstNr>();
            inLineFirstNr.ArrangeNr();

            elapsedInLineTimes.DeleteStartTimes();


            Domain.ShowingProgressByDays.Display display = container.Resolve<Domain.ShowingProgressByDays.Display>();
            DataTable dataTable = display.SetItemsForDisplay();

            DateTime dateTime = DateTime.Now;
            double day = 0;

            int[] rowNr = new int[1000];
            int rowCount = 1;
            foreach (DataRow row in dataTable.Rows)
            {
                day = 0;

                rowNr[rowCount] = rowCount;
                foreach (DataColumn dc in dataTable.Columns)
                {

                    DayOfWeek dayOfWeek = dateTime.AddDays(day).DayOfWeek;
                    if (dc.ColumnName != "Id" && dc.ColumnName != "Item")
                    {
                        if (dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Saturday)
                        {
                            dc.ColumnName = (dateTime.AddDays(day)).ToString("   #yyyy-MM-dd#");
                        }
                        else
                        {
                            dc.ColumnName = (dateTime.AddDays(day)).ToString("    yyyy-MM-dd");
                        }
                        day++;

                    }

                }
                rowCount++;
            }
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                dataGridView3.Visible = false;
                label14.Visible = true;
                label14.Text = "No data to display!";
            }
            else
            {

                label14.Visible = false;
                dataGridView3.Visible = true;
                dataGridView3.DataSource = dataTable;

                for (int i = 0; i < rowNr.Length; i++)
                {
                    if (int.Parse(rowNr.GetValue(i).ToString()) != 0)
                    {
                        dataGridView3[0, int.Parse(rowNr.GetValue(i).ToString()) - 1].Value = "Show";
                    }
                }
                dataGridView3.BackgroundColor = Color.LightGray;
                dataGridView3.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                comboBoxAllDuplicate.Enabled = false;
            else
                comboBoxAllDuplicate.Enabled = true;

        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        public void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2 form2 = new Form2();
            if (dataGridView3.Columns[0].Index == e.ColumnIndex)
            {
                int s = dataGridView3.CurrentCell.RowIndex;
                ItemId = dataGridView3[1, s].Value.ToString();
                form2.ItemId = dataGridView3[1, s].Value.ToString();

                form2.Show();

                form2.GridView1Table();

                form2.FormName(ItemId);

            }

        }

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            string path = "";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                path = saveFileDialog1.FileName;

                Repository.Retrieve.ExcelRepository excelRepository = new Repository.Retrieve.ExcelRepository();
                Domain.SaveTo.Excel excel = new Domain.SaveTo.Excel();
                excel.CreateFile(excelRepository, path);

                MessageBox.Show("Excel file created , you can find the file " + path + "");
            }
            else
            {
                return;
            }

        }

        public void radiobuttonConnection()
        {
            if (radioButton2.Checked)
                comboBoxAllDuplicate.Enabled = false;
            else
                comboBoxAllDuplicate.Enabled = true;

        }
        public void radiobuttonAddConnection()
        {
            if (radioButtonAddConnection.Checked)
            {
                buttonTime.Enabled = false;
                buttonChangeSetupTime.Enabled = false;
                buttonDeletOperation.Enabled = false;
                buttonDeletItem.Enabled = false;
                buttonDeletConnection.Enabled = false;
                comboBoxOperations.Enabled = false;
                buttonDeleteDuplicate.Enabled = false;
                buttonDeleteDuplicateOperation.Enabled = false;
            }

        }
        private void radioButtonAddConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (radioButtonAddConnection.Checked)
            {
                comboBoxItems.Enabled = true;
                comboBoxAllOperations.Enabled = true;
                comboBoxOperations.Enabled = false;
                buttonAddConnection.Enabled = true;
                buttonTime.Enabled = false;
                buttonChangeSetupTime.Enabled = false;
                buttonDeletOperation.Enabled = false;
                buttonDeletItem.Enabled = false;
                buttonDeletConnection.Enabled = false;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                textBoxSetupTime.Enabled = true;
                textBoxTime.Enabled = true;
                radioButtonDeleteDuplicateYes.Enabled = false;
                radioButtonDeleteDuplicateNo.Enabled = false;
                buttonDeleteDuplicate.Enabled = false;
                comboBoxDeleteDuplicateOperation.Visible = false;
                buttonDeleteDuplicateOperation.Enabled = false;

                if (radioButton1.Checked == true)
                {
                    comboBoxAllOperations.Enabled = false;
                    comboBoxAllDuplicate.Enabled = true;
                }
                else
                {
                    comboBoxAllDuplicate.Enabled = false;
                    comboBoxAllOperations.Enabled = true;
                }

            }
            else
            {
                buttonAddConnection.Enabled = false;
            }
        }

        private void radioButtonChangeTime_CheckedChanged(object sender, EventArgs e)
        {
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (radioButtonChangeTime.Checked)
            {
                comboBoxItems.Enabled = true;
                comboBoxOperations.Enabled = true;
                buttonAddConnection.Enabled = false;
                buttonTime.Enabled = true;
                buttonChangeSetupTime.Enabled = false;
                buttonDeletOperation.Enabled = false;
                buttonDeletItem.Enabled = false;
                buttonDeletConnection.Enabled = false;
                comboBoxAllOperations.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                comboBoxAllDuplicate.Enabled = false;
                textBoxSetupTime.Enabled = false;
                textBoxTime.Enabled = true;
                radioButtonDeleteDuplicateYes.Enabled = false;
                radioButtonDeleteDuplicateNo.Enabled = false;
                buttonDeleteDuplicate.Enabled = false;
                comboBoxDeleteDuplicateOperation.Visible = false;
                buttonDeleteDuplicateOperation.Enabled = false;
            }
            else
            {
                buttonTime.Enabled = false;
            }
        }

        private void buttonChangeSetupTime_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxItems, comboBoxOperations, textBoxSetupTime, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxItems) || IncorectInput.checkIfIsInDB(comboBoxOperations))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }

            Domain.Adding.ChangeTime changeTime = container.Resolve<Domain.Adding.ChangeTime>();
            changeTime.SetSetupTime(textBoxSetupTime.Text, comboBoxItems.SelectedValue.ToString(), comboBoxOperations.SelectedValue.ToString());

            Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
            dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            dataGridView2.AutoResizeColumns();
            dataGridView2.Refresh();

            Label1.Text = "SetupTime changed!";

            var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
            labelSliderThread.Start();
        }

        private void radioButtonChangeSetupTime_CheckedChanged(object sender, EventArgs e)
        {
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (radioButtonChangeSetupTime.Checked)
            {
                comboBoxOperations.Enabled = true;
                buttonAddConnection.Enabled = false;
                buttonTime.Enabled = false;
                buttonChangeSetupTime.Enabled = true;
                buttonDeletOperation.Enabled = false;
                buttonDeletItem.Enabled = false;
                buttonDeletConnection.Enabled = false;
                comboBoxAllOperations.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                comboBoxAllDuplicate.Enabled = false;
                textBoxSetupTime.Enabled = true;
                textBoxTime.Enabled = false;
                comboBoxItems.Enabled = true;
                radioButtonDeleteDuplicateYes.Enabled = false;
                radioButtonDeleteDuplicateNo.Enabled = false;
                buttonDeleteDuplicate.Enabled = false;
                comboBoxDeleteDuplicateOperation.Visible = false;
                buttonDeleteDuplicateOperation.Enabled = false;
            }
            else
            {

            }
        }

        private void radioButtonDeletOperation_CheckedChanged(object sender, EventArgs e)
        {
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (radioButtonDeletOperation.Checked)
            {
                comboBoxItems.Enabled = false;
                comboBoxOperations.Enabled = false;
                textBoxTime.Enabled = false;
                textBoxSetupTime.Enabled = false;
                comboBoxAllDuplicate.Enabled = false;
                comboBoxAllOperations.Enabled = true;
                buttonAddConnection.Enabled = false;
                buttonTime.Enabled = false;
                buttonChangeSetupTime.Enabled = false;
                buttonDeletItem.Enabled = false;
                buttonDeletConnection.Enabled = false;
                buttonDeletOperation.Enabled = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButtonDeleteDuplicateYes.Enabled = false;
                radioButtonDeleteDuplicateNo.Enabled = false;
                buttonDeleteDuplicate.Enabled = false;
                comboBoxDeleteDuplicateOperation.Visible = false;
                buttonDeleteDuplicateOperation.Enabled = false;
            }
        }

        private void radioButtonDeletItem_CheckedChanged(object sender, EventArgs e)
        {
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (radioButtonDeletItem.Checked)
            {
                comboBoxItems.Enabled = true;
                comboBoxOperations.Enabled = false;
                textBoxTime.Enabled = false;
                textBoxSetupTime.Enabled = false;
                comboBoxAllOperations.Enabled = false;
                comboBoxAllDuplicate.Enabled = false;
                buttonAddConnection.Enabled = false;
                buttonTime.Enabled = false;
                buttonChangeSetupTime.Enabled = false;
                buttonDeletOperation.Enabled = false;
                buttonDeletItem.Enabled = true;
                buttonDeletConnection.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButtonDeleteDuplicateYes.Enabled = false;
                radioButtonDeleteDuplicateNo.Enabled = false;
                buttonDeleteDuplicate.Enabled = false;
                comboBoxDeleteDuplicateOperation.Visible = false;
                buttonDeleteDuplicateOperation.Enabled = false;
            }
        }

        private void radioButtonDeletConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (radioButtonDeletConnection.Checked)
            {
                comboBoxItems.Enabled = true;
                comboBoxOperations.Enabled = true;
                textBoxTime.Enabled = false;
                textBoxSetupTime.Enabled = false;
                comboBoxAllOperations.Enabled = false;
                comboBoxAllDuplicate.Enabled = false;
                buttonAddConnection.Enabled = false;
                buttonTime.Enabled = false;
                buttonChangeSetupTime.Enabled = false;
                buttonDeletOperation.Enabled = false;
                buttonDeletItem.Enabled = false;
                buttonDeletConnection.Enabled = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButtonDeleteDuplicateYes.Enabled = true;
                radioButtonDeleteDuplicateNo.Enabled = true;
                buttonDeleteDuplicate.Enabled = false;
                comboBoxDeleteDuplicateOperation.Visible = false;
                buttonDeleteDuplicateOperation.Enabled = false;
            }
            if (radioButtonDeleteDuplicateYes.Checked)
            {
                comboBoxOperations.Enabled = false;
                comboBoxAllDuplicate.Enabled = true;
            }
        }

        private void buttonAddPause_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxHour, comboBoxMinutes, comboBoxHour2, comboBoxMinutes2, textBoxPauseName, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }

            Domain.Adding.Pause pause = container.Resolve<Domain.Adding.Pause>();
            pause.AddPause(textBoxPauseName.Text, comboBoxHour.Text, comboBoxMinutes.Text,
                comboBoxHour2.Text, comboBoxMinutes2.Text);

            Domain.View.All All = container.Resolve<Domain.View.All>();
            dataGridViewPause.DataSource = All.PauseView();
            dataGridViewPause.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPause.BackgroundColor = Color.White;
            dataGridViewPause.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            dataGridViewPause.AutoResizeColumns();

            Label1.Text = "Pause Added!";

            var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
            labelSliderThread.Start();
        }

        private void buttonDeletPause_Click(object sender, EventArgs e)
        {
            var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };

            foreach (DataGridViewRow row in dataGridViewPause.SelectedRows)
            {
                int rows = row.Index;
                string pauseName = dataGridViewPause[0, rows].Value.ToString();

                Domain.Delete.Pause pause = container.Resolve<Domain.Delete.Pause>();
                pause.Delete(pauseName);

                Repository.Retrieve.All all = new Repository.Retrieve.All();
                Domain.View.All All = new Domain.View.All(all);
                dataGridViewPause.DataSource = All.PauseView();
                dataGridViewPause.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewPause.BackgroundColor = Color.White;
                dataGridViewPause.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                dataGridViewPause.AutoResizeColumns();

                Label1.Text = "Pause Deleted!";

                labelSliderThread.Start();

                return;
            }
            Label1.Text = "Press row to Delete!";

            labelSliderThread.Start();

        }

        private void buttonRunItem_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxRunItems, textBoxRunItemsCount, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxRunItems))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }

            Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = container.Resolve<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();
            itemsOperationsForTrueFalseCheck.RunAllChecking(comboBoxRunItems.SelectedValue.ToString());

            Domain.ArrangeInLineTable.ElapsedInLineTimes elapsedInLineTimes = container.Resolve<Domain.ArrangeInLineTable.ElapsedInLineTimes>();
            elapsedInLineTimes.DeleteEndTimes();

            Domain.ArrangeInLineTable.SortInLineTableNr sortInLineTableNr = container.Resolve<Domain.ArrangeInLineTable.SortInLineTableNr>();
            sortInLineTableNr.ArrangeAllInLineNr();

            Domain.ArrangeInLineTable.InLineFirstNr inLineFirstNr = container.Resolve<Domain.ArrangeInLineTable.InLineFirstNr>();
            inLineFirstNr.ArrangeNr();

            elapsedInLineTimes.DeleteStartTimes();


            Domain.StartAllOperations.Item item = container.Resolve<Domain.StartAllOperations.Item>();
            item.Run(comboBoxRunItems.SelectedValue.ToString(), textBoxRunItemsCount.Text);

            item.RunWithDuplicates(comboBoxRunItems.SelectedValue.ToString(), textBoxRunItemsCount.Text);


            Label1.Text = "Item added!";

            var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
            labelSliderThread.Start();

        }

        private void comboBoxItems2_SelectedIndexChanged(object sender, EventArgs e)
        {

            Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
          
            ComboboxDropListItems(comboBoxOperations2, operation.List(comboBoxItems2.SelectedValue.ToString()));
        }
        private void positionChange(int counter, int fadingSpeed)
        {
            if (Label1.ForeColor.B == 255) return;

            Label1.ForeColor = Color.FromArgb(Label1.ForeColor.R + fadingSpeed, Label1.ForeColor.G + fadingSpeed, Label1.ForeColor.B + fadingSpeed);
            if (Label1.ForeColor.R % 2 == 0 && counter != 3 || Label1.ForeColor.R % 3 == 0 && counter != 3)
            {

                Label1.ForeColor = Color.FromArgb(Label1.ForeColor.R - fadingSpeed, Label1.ForeColor.G - fadingSpeed, Label1.ForeColor.B - fadingSpeed);
                Label1.Location = new Point(Label1.Location.X - 1, Label1.Location.Y);

            }

            Label1.Location = new Point(Label1.Location.X + 2, Label1.Location.Y);


            if (Label1.ForeColor.R >= Panel1.BackColor.R)
            {

                Label1.ForeColor = Panel1.BackColor;

            }

            Label1.Refresh();
        }
        private void labelStartPosition()
        {
            Label1.ForeColor = Color.Black;
            Label1.Location = new Point(693, 39);
        }
        public void LabelSlider()
        {
            Invoke(LabelStartPositionDelegate);

            int counter = 0;
            int fadingSpeed = 2;

            while (Label1.ForeColor.R != Panel1.BackColor.R)
            {
                if (counter == 4)
                {
                    counter = 0;
                }
                Thread.Sleep(10);
                Invoke(LabelSliderDelegate, counter, fadingSpeed);
                counter++;
            }
        }

        private void buttonAddDuplicateName_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getNameLength(textBoxName, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }

            var addNew = container.Resolve<Domain.Adding.AddNew>();

            addNew.AddDuplicateName(textBoxName.Text);


            var addAll = new Repository.Retrieve.All();
            Domain.View.All getAll = new Domain.View.All(addAll);
            comboBoxDuplicate.ValueMember = "Id";
            comboBoxDuplicate.DisplayMember = "Name";
            comboBoxDuplicate.DataSource = getAll.DuplicateName();

            comboBoxAllDuplicate.ValueMember = "Id";
            comboBoxAllDuplicate.DisplayMember = "Name";
            comboBoxAllDuplicate.DataSource = getAll.DuplicateName();

            Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();

            dataGridView2.DataSource = ItemForGrid.DuplicateView(comboBoxDuplicate.SelectedValue.ToString());
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            dataGridView2.AutoResizeColumns();

            Label1.Text = "Duplicate Name Added!";

            var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
            labelSliderThread.Start();

            textBoxName.Text = "";
        }

        private void checkBoxDuplicateName_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxDuplicate.Checked)
            {
                comboBoxDuplicate.Enabled = false;
                buttonAddDuplicateOperation.Enabled = false;
                buttonAddDuplicateName.Enabled = false;
                textBoxId.Enabled = true;
                buttonAddItem.Enabled = true;
                buttonAddOperation.Enabled = true;

                radioButtonAddDuplicateName.Enabled = false;
                radioButtonAddDuplicateOperation.Enabled = false;

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                try
                {
                    if (ItemForGrid.View(comboBoxItems.SelectedValue.ToString()).Rows.Count == 0)
                    {

                    }
                    else
                    {
                        dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView2.BackgroundColor = Color.White;
                        dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                        dataGridView2.AutoResizeColumns();
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {

                }
            }
            else
            {
                comboBoxDuplicate.Enabled = true;
                buttonAddDuplicateName.Enabled = true;
                textBoxId.Enabled = false;
                buttonAddItem.Enabled = false;
                buttonAddOperation.Enabled = false;

                radioButtonAddDuplicateName.Enabled = true;
                radioButtonAddDuplicateOperation.Enabled = true;

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                try
                {
                    if (comboBoxDuplicate.SelectedValue == null)
                    {

                    }
                    else
                    {
                        dataGridView2.DataSource = ItemForGrid.DuplicateView(comboBoxDuplicate.SelectedValue.ToString());
                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView2.BackgroundColor = Color.White;
                        dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                        dataGridView2.AutoResizeColumns();


                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {

                }
                if (radioButtonAddDuplicateOperation.Checked)
                {
                    buttonAddDuplicateOperation.Enabled = true;
                    buttonAddDuplicateName.Enabled = false;
                    textBoxId.Enabled = true;
                    buttonAddItem.Enabled = false;
                    buttonAddOperation.Enabled = false;

                    try
                    {
                        if (ItemForGrid.DuplicateView(comboBoxItems.SelectedValue.ToString()).Rows.Count == 0)
                        {

                        }
                        else
                        {
                            dataGridView2.DataSource = ItemForGrid.DuplicateView(comboBoxDuplicate.SelectedValue.ToString());
                            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView2.BackgroundColor = Color.White;
                            dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                            dataGridView2.AutoResizeColumns();
                        }
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {

                    }
                }
            }
        }

        private void radioButtonAddDuplicateName_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonAddDuplicateName.Checked)
            {
                buttonAddDuplicateName.Enabled = false;
                textBoxId.Enabled = true;
            }
            else
            {

                buttonAddDuplicateName.Enabled = true;
                textBoxId.Enabled = false;

            }
        }
        private void radioButtonAddDuplicateOperation_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonAddDuplicateOperation.Checked)
            {
                buttonAddDuplicateName.Enabled = true;
                textBoxId.Enabled = false;
                buttonAddDuplicateOperation.Enabled = false;
            }
            else
            {
                comboBoxDuplicate.Enabled = true;
                buttonAddDuplicateName.Enabled = false;
                textBoxId.Enabled = true;
                buttonAddDuplicateOperation.Enabled = true;
            }
        }

        private void buttonAddDuplicateOperation_Click(object sender, EventArgs e)
        {

            if (IncorectInput.getInput(textBoxId, textBoxName, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }

            Domain.Checking.CheckIfExists check = container.Resolve<Domain.Checking.CheckIfExists>();
            if (!check.IfItemExists(textBoxId.Text))
            {

                var addNew = container.Resolve<Domain.Adding.AddNew>();


                addNew.AddDuplicateOperation(textBoxId.Text, textBoxName.Text, comboBoxDuplicate.SelectedValue.ToString());

                int comboBoxValue = int.Parse(comboBoxDuplicate.SelectedValue.ToString());

                var addAll = new Repository.Retrieve.All();
                Domain.View.All getAll = new Domain.View.All(addAll);
                comboBoxDuplicate.ValueMember = "Id";
                comboBoxDuplicate.DisplayMember = "Name";
                comboBoxDuplicate.DataSource = getAll.DuplicateName();

                if (comboBoxDuplicate.SelectedValue != null)
                {
                    comboBoxDuplicate.SelectedValue = comboBoxValue;
                }
                               
                Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
               
                ComboboxDropListItems(comboBoxDeleteDuplicateOperation, operation.DuplicateList(comboBoxAllDuplicate.SelectedValue.ToString()));

                ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));

                Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                if (itemsWithOperations.Items().Rows.Count != 0)
                {
                    comboBoxItems2.ValueMember = "Id";
                    comboBoxItems2.DisplayMember = "Name";
                    comboBoxItems2.DataSource = itemsWithOperations.Items();
                }
                else
                {
                    comboBoxItems2.DataSource = null;
                    comboBoxOperations2.DataSource = null;
                }

                ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();

                dataGridView2.DataSource = ItemForGrid.DuplicateView(comboBoxDuplicate.SelectedValue.ToString());
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.BackgroundColor = Color.White;
                dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                dataGridView2.AutoResizeColumns();

                Label1.Text = "Duplicate Name Added!";

                var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                labelSliderThread.Start();

                textBoxName.Text = "";
                textBoxId.Text = "";

            }
            else
            {
                MessageBox.Show("Id already exist!", "Message", MessageBoxButtons.OK);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                comboBoxAllOperations.Enabled = false;
            }
            else
            {
                comboBoxAllOperations.Enabled = true;
            }

        }

        private void comboBoxDuplicate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();

            dataGridView2.DataSource = ItemForGrid.DuplicateView(comboBoxDuplicate.SelectedValue.ToString());
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            dataGridView2.AutoResizeColumns();
        }

        private void radioButtonDeleteDuplicateYes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDeleteDuplicateYes.Checked == true)
            {
                comboBoxAllDuplicate.Enabled = true;
                comboBoxOperations.Enabled = false;
            }
            else
            {
                comboBoxAllDuplicate.Enabled = false;
                comboBoxOperations.Enabled = true;
            }
        }

        private void radioButtonDeleteDuplicate_CheckedChanged(object sender, EventArgs e)
        {
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (radioButtonDeleteDuplicate.Checked)
            {
                comboBoxItems.Enabled = false;
                comboBoxOperations.Enabled = false;
                textBoxTime.Enabled = false;
                textBoxSetupTime.Enabled = false;
                comboBoxAllOperations.Enabled = false;
                comboBoxAllDuplicate.Enabled = true;
                buttonAddConnection.Enabled = false;
                buttonTime.Enabled = false;
                buttonChangeSetupTime.Enabled = false;
                buttonDeletOperation.Enabled = false;
                buttonDeletItem.Enabled = false;
                buttonDeletConnection.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButtonDeleteDuplicateYes.Enabled = false;
                radioButtonDeleteDuplicateNo.Enabled = false;
                buttonDeleteDuplicate.Enabled = true;
                comboBoxDeleteDuplicateOperation.Visible = false;
                buttonDeleteDuplicateOperation.Enabled = false;
            }
        }

        private void buttonDeleteDuplicate_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxAllDuplicate, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxAllDuplicate))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }

            Domain.Checking.IsBusyCheck isBusyCheck = container.Resolve<Domain.Checking.IsBusyCheck>();
            if (isBusyCheck.GetDuplicateOperationBool(comboBoxAllDuplicate.SelectedValue.ToString()))
            {
                MessageBox.Show("Can't delet while runing!", "Message", MessageBoxButtons.OK);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Do you want to delete: " + comboBoxAllDuplicate.Text + "?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                Domain.Delete.ItemsOperations itemsOperations = container.Resolve<Domain.Delete.ItemsOperations>();

                itemsOperations.DeleteDuplicate(comboBoxAllDuplicate.SelectedValue.ToString());

                Domain.View.All All = container.Resolve<Domain.View.All>();

                ComboboxDropListItems(comboBoxDuplicate, All.DuplicateName());
                ComboboxDropListItems(comboBoxAllDuplicate, All.DuplicateName());

                Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                if (itemsWithOperations.Items().Rows.Count != 0)
                {
                    comboBoxItems2.ValueMember = "Id";
                    comboBoxItems2.DisplayMember = "Name";
                    comboBoxItems2.DataSource = itemsWithOperations.Items();
                }
                else
                {
                    comboBoxItems2.DataSource = null;
                    comboBoxOperations2.DataSource = null;
                }

                ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                var addAll = new Repository.Retrieve.All();
                Domain.View.All getAll = new Domain.View.All(addAll);
               
                ComboboxDropListItems(comboBoxAllOperations, getAll.Operations());

                Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
               
                ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));

                Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.BackgroundColor = Color.White;
                dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                dataGridView2.AutoResizeColumns();
                dataGridView2.Refresh();

                Label1.Text = "Duplicate deleted!";

                var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                labelSliderThread.Start();

            }
        }

        private void radioButtonDeleteDuplicateOperation_CheckedChanged(object sender, EventArgs e)
        {
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (radioButtonDeleteDuplicateOperation.Checked)
            {
                comboBoxItems.Enabled = false;
                comboBoxOperations.Enabled = false;
                textBoxTime.Enabled = false;
                textBoxSetupTime.Enabled = false;
                comboBoxAllOperations.Enabled = false;
                comboBoxAllDuplicate.Enabled = true;
                comboBoxDeleteDuplicateOperation.Visible = true;
                buttonAddConnection.Enabled = false;
                buttonTime.Enabled = false;
                buttonChangeSetupTime.Enabled = false;
                buttonDeletOperation.Enabled = false;
                buttonDeletItem.Enabled = false;
                buttonDeletConnection.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButtonDeleteDuplicateYes.Enabled = false;
                radioButtonDeleteDuplicateNo.Enabled = false;
                buttonDeleteDuplicate.Enabled = false;
                buttonDeleteDuplicateOperation.Enabled = true;
            }

            Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
          
            ComboboxDropListItems(comboBoxDeleteDuplicateOperation, operation.DuplicateList(comboBoxAllDuplicate.SelectedValue.ToString()));
        }

        private void buttonDeleteDuplicateOperation_Click(object sender, EventArgs e)
        {
            if (IncorectInput.getInput(comboBoxAllDuplicate, panel2))
            {
                labelRed = true;
                return;
            }
            if (labelRed)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                labelRed = false;
            }
            if (IncorectInput.checkIfIsInDB(comboBoxAllDuplicate))
            {
                MessageBox.Show("Item not faund!", "Message", MessageBoxButtons.OK); return;
            }

            Domain.Checking.IsBusyCheck isBusyCheck = container.Resolve<Domain.Checking.IsBusyCheck>();
            if (isBusyCheck.GetDuplicateOperationBool(comboBoxAllDuplicate.SelectedValue.ToString()))
            {
                MessageBox.Show("Can't delet while runing!", "Message", MessageBoxButtons.OK);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Do you want to delete: " + comboBoxDeleteDuplicateOperation.Text + "?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                Domain.Delete.ItemsOperations itemsOperations = container.Resolve<Domain.Delete.ItemsOperations>();

                itemsOperations.DeleteOperation(comboBoxDeleteDuplicateOperation.SelectedValue.ToString());

                Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
              
                ComboboxDropListItems(comboBoxDeleteDuplicateOperation, operation.DuplicateList(comboBoxAllDuplicate.SelectedValue.ToString()));

                Domain.View.All All = container.Resolve<Domain.View.All>();

                ComboboxDropListItems(comboBoxDuplicate, All.DuplicateName());
                ComboboxDropListItems(comboBoxAllDuplicate, All.DuplicateName());

                Domain.RetrieveSpecific.ItemsWithOperations itemsWithOperations = container.Resolve<Domain.RetrieveSpecific.ItemsWithOperations>();
                if (itemsWithOperations.Items().Rows.Count != 0)
                {
                    comboBoxItems2.ValueMember = "Id";
                    comboBoxItems2.DisplayMember = "Name";
                    comboBoxItems2.DataSource = itemsWithOperations.Items();
                }
                else
                {
                    comboBoxItems2.DataSource = null;
                    comboBoxOperations2.DataSource = null;
                }

                ComboboxDropListItems(comboBoxOperations, operation.List(comboBoxItems.SelectedValue.ToString()));
              
                ComboboxDropListItems(comboBoxRunItems, itemsWithOperations.Items());

                var addAll = new Repository.Retrieve.All();
                Domain.View.All getAll = new Domain.View.All(addAll);

                ComboboxDropListItems(comboBoxAllOperations, getAll.Operations());

                if (checkBoxDuplicate.Checked)
                {
                    Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();

                    dataGridView2.DataSource = ItemForGrid.DuplicateView(comboBoxDuplicate.SelectedValue.ToString());
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.BackgroundColor = Color.White;
                    dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                    dataGridView2.AutoResizeColumns();
                }
                else
                {
                    Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
                    dataGridView2.DataSource = ItemForGrid.View(comboBoxItems.SelectedValue.ToString());
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.BackgroundColor = Color.White;
                    dataGridView2.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                    dataGridView2.AutoResizeColumns();
                    dataGridView2.Refresh();
                }
               
                Label1.Text = "Duplicate operation deleted!";

                var labelSliderThread = new Thread(LabelSlider) { IsBackground = true };
                labelSliderThread.Start();

            }
        }

        private void comboBoxAllDuplicate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Domain.RetrieveSpecific.OperationList operation = container.Resolve<Domain.RetrieveSpecific.OperationList>();
          
            ComboboxDropListItems(comboBoxDeleteDuplicateOperation, operation.DuplicateList(comboBoxAllDuplicate.SelectedValue.ToString()));
        }
        private void ComboboxDropListItems(ComboBox comboBox, DataTable dataForComboBox)
        {
            if (dataForComboBox.Rows.Count != 0)
            {
                comboBox.ValueMember = "Id";
                comboBox.DisplayMember = "Name";
                comboBox.DataSource = dataForComboBox;
            }
            else
            {
                comboBox.DataSource = null;
            }
        }
      
    }
}
