using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.Country
{
    public class CountryRepository
    {
		private GeneralMethods objgm = new GeneralMethods();
		public CountryModel AddEdit_Country(CountryModel _CountryModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _CountryModel.GetType().Name,
					StoredProcedureName = @"SP_Country_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				if (_CountryModel.ActionType == "Remote")
				{
					objQueryBuilder.AddFieldValue("@Code", _CountryModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _CountryModel.CompanyId, DataTypes.Text, false);
				}
				else
				{
					objQueryBuilder.AddFieldValue("@Id", _CountryModel.Id, DataTypes.Numeric, false);
					objQueryBuilder.AddFieldValue("@Name", _CountryModel.Name, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@CompanyId", _CountryModel.CompanyId, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Code", _CountryModel.Code, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@Isactive", _CountryModel.IsActive, DataTypes.Boolean, false);
					objQueryBuilder.AddFieldValue("@CreatedBy", _CountryModel.CreatedBy, DataTypes.Text, false);
					objQueryBuilder.AddFieldValue("@ModifiedBy", _CountryModel.ModifiedBy, DataTypes.Text, false);
				}
				objQueryBuilder.AddFieldValue("@ActionType", _CountryModel.ActionType, DataTypes.Text, false);

				return objgm.ExecuteObjectUsingSp<CountryModel>(objQueryBuilder);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public List<CountryModel> GetCountryList(string searchtxt = "", int page = 1, int pagesize = 10, string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Tbl_Mstr_Country",
					StoredProcedureName = @"SP_GetCountryData",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};
				objQueryBuilder.AddFieldValue("@Search", searchtxt, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@page", page, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@pagesize", pagesize, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@CompanyId", CompanyId, DataTypes.Text, false);
				return objgm.GetListUsingSp<CountryModel>(objQueryBuilder);
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
					TableName = "Tbl_Mstr_Country",
					StoredProcedureName = @"SP_GetCountryData_Count",
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

		public DataTable GetCountryData_Export(string searchtxt = "", string CompanyId = null)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = "Country",
					StoredProcedureName = @"SP_DownloadCountryData_Export",
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
	}
}