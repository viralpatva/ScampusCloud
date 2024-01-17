using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.StudentFacility
{
	public class StudentFacilityRepository
	{
		private GeneralMethods objgm = new GeneralMethods();
		public StudentFacilityModel AddEdit_StudentFacility(StudentFacilityModel _StudentFacilityModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _StudentFacilityModel.GetType().Name,
					StoredProcedureName = @"SP_StudentFacility_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};

				objQueryBuilder.AddFieldValue("@Id", _StudentFacilityModel.Id, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@Name", _StudentFacilityModel.Name, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@CompanyId", _StudentFacilityModel.CompanyId, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Code", _StudentFacilityModel.Code, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Isactive", _StudentFacilityModel.IsActive, DataTypes.Boolean, false);
				objQueryBuilder.AddFieldValue("@CreatedBy", _StudentFacilityModel.CreatedBy, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ModifiedBy", _StudentFacilityModel.ModifiedBy, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@ActionType", _StudentFacilityModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<StudentFacilityModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<StudentFacilityModel> GetStudentFacilityList(string searchtxt = "", int page = 1, int pagesize = 10)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_StudentFacility",
					StoredProcedureName = @"SP_GetStudentFacilityData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				return objgm.GetListUsingSp<StudentFacilityModel>(objQueryBuilder);
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
					TableName = "Tbl_Mstr_StudentFacility",
					StoredProcedureName = @"SP_GetStudentFacilityData_Count",
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

		public DataTable GetStudentFacilityData_Export(string searchtxt = "")
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "StudentFacility",
					StoredProcedureName = @"SP_DownloadStudentFacilityData_Export",
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