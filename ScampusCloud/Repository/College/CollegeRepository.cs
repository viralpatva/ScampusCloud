using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ScampusCloud.Repository.College
{
	public class CollegeRepository
	{
		private GeneralMethods objgm = new GeneralMethods();
		public CollegeModel AddEdit_College(CollegeModel _CollegeModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _CollegeModel.GetType().Name,
					StoredProcedureName = @"SP_College_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};

				objQueryBuilder.AddFieldValue("@Id", _CollegeModel.Id, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@Name", _CollegeModel.Name, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@CompanyId", _CollegeModel.CompanyId, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ScampusId", _CollegeModel.ScampusId, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@Code", _CollegeModel.Code, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Isactive", _CollegeModel.IsActive, DataTypes.Boolean, false);
				objQueryBuilder.AddFieldValue("@CreatedBy", _CollegeModel.CreatedBy, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ModifiedBy", _CollegeModel.ModifiedBy, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ActionType", _CollegeModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<CollegeModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<CollegeModel> GetCollegeList(string searchtxt = "", int page = 1, int pagesize = 10)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_College",
					StoredProcedureName = @"SP_GetCollegeData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				return objgm.GetListUsingSp<CollegeModel>(objQueryBuilder);
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
					TableName = "Tbl_Mstr_College",
					StoredProcedureName = @"SP_GetCollegeData_Count",
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

		public DataTable GetCollegeData_Export(string searchtxt = "")
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "College",
					StoredProcedureName = @"SP_DownloadCollegeData_Export",
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
		public List<SelectListItem> BindCampusDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_Campus",
				StoredProcedureName = @"Sps_Load_Campus_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}
	}
}