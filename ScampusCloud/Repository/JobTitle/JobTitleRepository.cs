using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.JobTitle
{
    public class JobTitleRepository
    {
		private GeneralMethods objgm = new GeneralMethods();
		public JobTitleModel AddEdit_JobTitle(JobTitleModel _JobTitleModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _JobTitleModel.GetType().Name,
					StoredProcedureName = @"SP_JobTitle_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_JobTitleModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Code", _JobTitleModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _JobTitleModel.CompanyId, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _JobTitleModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@Name", _JobTitleModel.Name, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _JobTitleModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _JobTitleModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _JobTitleModel.IsActive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _JobTitleModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _JobTitleModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _JobTitleModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<JobTitleModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<JobTitleModel> GetJobTitleList(string searchtxt = "", int page = 1, int pagesize = 10)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_JobTitle",
					StoredProcedureName = @"SP_GetJobTitleData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				return objgm.GetListUsingSp<JobTitleModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public string GetAllCount(string searchtxt = "")
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_JobTitle",
					StoredProcedureName = @"SP_GetJobTitleData_Count",
					SetQueryType = QueryBuilder.QueryType.SELECT,
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				return objgm.ExcecuteScalarUsingSp(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}

		}

		public DataTable GetJobTitleData_Export(string searchtxt = "")
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "JobTitle",
					StoredProcedureName = @"SP_DownloadJobTitleData_Export",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				return objgm.ExecuteDataTableUsingSp(objQueryBuilder, true);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}
	}
}