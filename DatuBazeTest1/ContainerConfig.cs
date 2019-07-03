using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatuBazeTest1
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Domain.View.All>();
            builder.RegisterType<Repository.Retrieve.All>().As<Domain.Interfaces.IAll>();
          
            builder.RegisterType<Domain.RetrieveSpecific.ItemsWithOperations>();
            builder.RegisterType<Repository.Retrieve.ItemsWithOperationsRepository>().As<Domain.Interfaces.IItemsWithOperationsRepository>();

            builder.RegisterType<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();
            builder.RegisterType<Repository.Update.OperationConditionRepository>().As<Domain.Interfaces.IOperationConditionRepository>();
            builder.RegisterType<Repository.Retrieve.ItemsOperationsForTrueFalseCheckRepository>().As<Domain.Interfaces.IItemsOperationsForTrueFalseCheckReposytory>();
            builder.RegisterType<Repository.Update.ItemsOperationsNullRepository>().As<Domain.Interfaces.IItemsOperationsNullRepository>();

            builder.RegisterType<Domain.ArrangeInLineTable.ElapsedInLineTimes>();
            builder.RegisterType<Repository.Delete.ElapsedInLineTimesRepository>().As<Domain.Interfaces.IElapsedInLineTimesRepository>();

            builder.RegisterType<Domain.ArrangeInLineTable.SortInLineTableNr>();
            builder.RegisterType<Repository.Update.NumerationInLineRepository>().As<Domain.Interfaces.INumerationInLineRepository>();
            builder.RegisterType<Repository.Retrieve.SortedInLineTeableASCRepository>().As<Domain.Interfaces.ISortedInLineTableASCRepository>();

            builder.RegisterType<Domain.ArrangeInLineTable.InLineFirstNr>();
            builder.RegisterType<Repository.Retrieve.InLineFirstNrRepository>().As<Domain.Interfaces.IInLineFirstNrRepository>();
            builder.RegisterType<Repository.Update.OperationTableRepository>().As<Domain.Interfaces.IOperationTableRepository>();

            builder.RegisterType<Domain.IsOperationBusy.ItemsOperationsForTrueFalseCheck>();

            builder.RegisterType<Domain.RetrieveSpecific.ItemForGrid>();
            builder.RegisterType<Repository.Retrieve.ItemForGridRepository>().As<Domain.Interfaces.IItemForGridRepository>();

            builder.RegisterType<Domain.Checking.CheckIfExists>();
            builder.RegisterType<Repository.Retrieve.CheckIfExistsRepository>().As<Domain.Interfaces.ICheckIfExistRepository>();

            builder.RegisterType<Domain.Adding.AddNew>();
            builder.RegisterType<Repository.Adding.Add>().As<Domain.Interfaces.IAdd>();
            builder.RegisterType<Repository.Retrieve.DuplicateOperationsRepository>().As<Domain.Interfaces.IDuplicateOperationsRepository>();

            builder.RegisterType<Domain.Checking.CheckIfConnectionExists>();
            builder.RegisterType<Repository.Retrieve.CheckIfConnectionExistsRepository>().As<Domain.Interfaces.ICheckIfConnectionExistsRepository>();

            builder.RegisterType<Domain.StartAllOperations.Item>();
            builder.RegisterType<Repository.Retrieve.StartAllOperationsRepository>().As<Domain.Interfaces.IStartAllOperations>();
            builder.RegisterType<Repository.Retrieve.IsBusyCheckRepository>().As<Domain.Interfaces.IIsBusyCheckRepository>();
            builder.RegisterType<Repository.Update.StartTimeRepository>().As<Domain.Interfaces.IStartTimeRepository>();
            builder.RegisterType<Repository.Retrieve.PauseTimesRepository>().As<Domain.Interfaces.IPauseTimesRepository>();
            builder.RegisterType<Repository.Retrieve.TimeRepository>().As<Domain.Interfaces.ITimeRepository>();
            builder.RegisterType<Repository.Update.OperationConditionRepository>().As<Domain.Interfaces.IOperationConditionRepository>();
            builder.RegisterType<Repository.Retrieve.DataForInLineTable>().As<Domain.Interfaces.IDataForInLineTable>();

            builder.RegisterType<Domain.Adding.ItemsOperationsConnection>();
            builder.RegisterType<Repository.Adding.ItemsOperationsConnectionRepository>().As<Domain.Interfaces.IItemsOperationsConnectionRepository>();
            builder.RegisterType<Repository.Retrieve.DuplicateOperationsRepository>().As<Domain.Interfaces.IDuplicateOperationsRepository>();

            builder.RegisterType<Domain.RetrieveSpecific.OperationList>();
            builder.RegisterType<Repository.Retrieve.OperationList>().As<Domain.Interfaces.IOperationList>();

            builder.RegisterType<Domain.Checking.IsBusyCheck>();

            builder.RegisterType<Domain.StartOneOperation.StartTime>();

            builder.RegisterType<Domain.IsOperationBusy.OperationCondition>();
            builder.RegisterType<Repository.Update.OperationConditionRepository>().As<Domain.Interfaces.IOperationConditionRepository>();

            builder.RegisterType<Domain.Adding.ChangeTime>();
            builder.RegisterType<Repository.Update.ChangeTimeRepository>().As<Domain.Interfaces.IChangeTimeRepository>();

            builder.RegisterType<Domain.Delete.ItemsOperations>();
            builder.RegisterType<Repository.Delete.ItemsOperationsRepository>().As<Domain.Interfaces.IItemsOperationsRepository>();
            builder.RegisterType<Repository.Retrieve.ItemsOperationsForTrueFalseCheckRepository>().As<Domain.Interfaces.IItemsOperationsForTrueFalseCheckReposytory>();
            builder.RegisterType<Repository.Retrieve.SortedInLineTeableASCRepository>().As<Domain.Interfaces.ISortedInLineTableASCRepository>();

            builder.RegisterType<Domain.ShowingProgressByDays.Display>();
            builder.RegisterType<Repository.Retrieve.DisplayRepository>().As<Domain.Interfaces.IDisplayRepository>();
            builder.RegisterType<Repository.Update.UpdateDisplayRepository>().As<Domain.Interfaces.IUpdateDisplayRepository>();

            builder.RegisterType<Domain.Adding.Pause>();
            builder.RegisterType<Repository.Adding.PauseRepository>().As<Domain.Interfaces.IPauseRepository>();

            builder.RegisterType<Domain.Delete.Pause>();
            builder.RegisterType<Repository.Delete.DeletePauseRepository>().As<Domain.Interfaces.IDeletePauseRepository>();

            builder.RegisterType<Domain.Form2.CombiningDataForGrid>();
            builder.RegisterType<Repository.Retrieve.ItemsForForm2Repository>().As<Domain.Interfaces.IItemsForForm2Repository>();
            builder.RegisterType<Repository.Update.Form2ProgressPictureRepository>().As<Domain.Interfaces.IForm2ProgressPictureRepository>();
            builder.RegisterType<Repository.Retrieve.DataForForm2ProgressTextRepository>().As<Domain.Interfaces.IDataForForm2ProgressTxtRepository>();

            builder.RegisterType<Domain.Form2.HeaderText>();
            builder.RegisterType<Repository.Retrieve.Form2TextRepository>().As<Domain.Interfaces.IForm2TextRepository>();

            return builder.Build();
        }
    }
}
