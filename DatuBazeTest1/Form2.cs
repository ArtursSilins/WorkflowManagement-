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
using System.IO;
using Autofac;

namespace DatuBazeTest1
{
    public partial class Form2 : Form
    {

        public string ItemId { get; set; }

        public Autofac.IContainer container = ContainerConfig.Configure();

        public Form2()
        {
            InitializeComponent();
        }
      
        public void FormName(string itemId)
        {

            Domain.Form2.HeaderText headerText = container.Resolve<Domain.Form2.HeaderText>();
            Form2.ActiveForm.Text = headerText.Get(itemId);
         
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {

            Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = container.Resolve<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();
            itemsOperationsForTrueFalseCheck.RunAllChecking(ItemId);

            Domain.ArrangeInLineTable.ElapsedInLineTimes elapsedInLineTimes = container.Resolve<Domain.ArrangeInLineTable.ElapsedInLineTimes>();
            elapsedInLineTimes.DeleteEndTimes();

            Domain.ArrangeInLineTable.SortInLineTableNr sortInLineTableNr = container.Resolve<Domain.ArrangeInLineTable.SortInLineTableNr>();
            sortInLineTableNr.ArrangeAllInLineNr();

            Domain.ArrangeInLineTable.InLineFirstNr inLineFirstNr = container.Resolve<Domain.ArrangeInLineTable.InLineFirstNr>();
            inLineFirstNr.ArrangeNr();

            elapsedInLineTimes.DeleteStartTimes();
          
            Domain.Form2.CombiningDataForGrid combiningDataForGrid = container.Resolve<Domain.Form2.CombiningDataForGrid>();
            combiningDataForGrid.Update(ItemId);

            Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            dataGridView1.DataSource = ItemForGrid.ItemsWhenDisplayGridClicked(ItemId);

            DataGridViewColumn column = dataGridView1.Columns[2];
            column.Width = 100;
        }
        private static void getStartTimeInPause(bool time)
        {

        }
        public void GridView1Table()
        {
            Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = container.Resolve<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();
            itemsOperationsForTrueFalseCheck.RunAllChecking(ItemId);

            Domain.ArrangeInLineTable.ElapsedInLineTimes elapsedInLineTimes = container.Resolve<Domain.ArrangeInLineTable.ElapsedInLineTimes>();
            elapsedInLineTimes.DeleteEndTimes();

            Domain.ArrangeInLineTable.SortInLineTableNr sortInLineTableNr = container.Resolve<Domain.ArrangeInLineTable.SortInLineTableNr>();
            sortInLineTableNr.ArrangeAllInLineNr();

            Domain.ArrangeInLineTable.InLineFirstNr inLineFirstNr = container.Resolve<Domain.ArrangeInLineTable.InLineFirstNr>();
            inLineFirstNr.ArrangeNr();

            elapsedInLineTimes.DeleteStartTimes();


            Domain.Form2.CombiningDataForGrid combiningDataForGrid = container.Resolve<Domain.Form2.CombiningDataForGrid>();
            combiningDataForGrid.Update(ItemId);

            Domain.RetrieveSpecific.ItemForGrid ItemForGrid = container.Resolve<Domain.RetrieveSpecific.ItemForGrid>();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
            dataGridView1.DataSource = ItemForGrid.ItemsWhenDisplayGridClicked(ItemId);

            DataGridViewColumn column = dataGridView1.Columns[2];
            column.Width = 100;

        }

    }
}
