using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.StudentCompany
{
	public class StudentCompanyRepository
	{
		private GeneralMethods objgm = new GeneralMethods();
		public StudentCompanyModel AddEdit_StudentCompany(StudentCompanyModel _StudentCompanyModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _StudentCompanyModel.GetType().Name,
					StoredProcedureName = @"SP_StudentCompany_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};

				objQueryBuilder.AddFieldValue("@Id", _StudentCompanyModel.Id, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@Name", _StudentCompanyModel.Name, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@CompanyId", _StudentCompanyModel.CompanyId, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Code", _StudentCompanyModel.Code, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Isactive", _StudentCompanyModel.IsActive, DataTypes.Boolean, false);
				objQueryBuilder.AddFieldValue("@CreatedBy", _StudentCompanyModel.CreatedBy, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ModifiedBy", _StudentCompanyModel.ModifiedBy, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ActionType", _StudentCompanyModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<StudentCompanyModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<StudentCompanyModel> GetStudentCompanyList(string searchtxt = "", int page = 1, int pagesize = 10)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_StudentCompany",
					StoredProcedureName = @"SP_GetStudentCompanyData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				return objgm.GetListUsingSp<StudentCompanyModel>(objQueryBuilder);
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
					TableName = "Tbl_Mstr_StudentCompany",
					StoredProcedureName = @"SP_GetStudentCompanyData_Count",
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

		public DataTable GetStudentCompanyData_Export(string searchtxt = "")
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "StudentCompany",
					StoredProcedureName = @"SP_DownloadStudentCompanyData_Export",
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