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

namespace ScampusCloud.Repository.StudentDepartment
{
    public class StudentDepartmentRepository
    {
		private GeneralMethods objgm = new GeneralMethods();
		public StudentDepartmentModel AddEdit_StudentDepartment(StudentDepartmentModel _StudentDepartmentModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _StudentDepartmentModel.GetType().Name,
					StoredProcedureName = @"SP_StudentDepartment_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_StudentDepartmentModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Code", _StudentDepartmentModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _StudentDepartmentModel.CompanyId, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _StudentDepartmentModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@Name", _StudentDepartmentModel.Name, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CollegeId", _StudentDepartmentModel.CollegeId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _StudentDepartmentModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _StudentDepartmentModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _StudentDepartmentModel.IsActive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _StudentDepartmentModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _StudentDepartmentModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _StudentDepartmentModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<StudentDepartmentModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<StudentDepartmentModel> GetStudentDepartmentList(string searchtxt = "", int page = 1, int pagesize = 10, string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_StudentDepartment",
					StoredProcedureName = @"SP_GetStudentDepartmentData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.GetListUsingSp<StudentDepartmentModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public string GetAllCount(string searchtxt = "", string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_StudentDepartment",
					StoredProcedureName = @"SP_GetStudentDepartmentData_Count",
					SetQueryType = QueryBuilder.QueryType.SELECT,
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.ExcecuteScalarUsingSp(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}

		}

		public DataTable GetStudentDepartmentData_Export(string searchtxt = "", string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "StudentDepartment",
					StoredProcedureName = @"SP_DownloadStudentDepartmentData_Export",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.ExecuteDataTableUsingSp(objQueryBuilder, true);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}
		public List<SelectListItem> BindDropDown(string CompanyId)
		{
			QueryBuilder objQueryBuilder = new QueryBuilder
			{
				TableName = "tbl_Campus",
				StoredProcedureName = @"Sps_Load_College_Dropdown",
				SetQueryType = QueryBuilder.QueryType.SELECT,
			};
			objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
			return objgm.GetListUsingSp<SelectListItem>(objQueryBuilder);
		}
	}
}