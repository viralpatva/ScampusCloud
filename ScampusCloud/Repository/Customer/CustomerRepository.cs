using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.Customer
{
	public class CustomerRepository
	{
		private GeneralMethods objgm = new GeneralMethods();
		public bool AddEdit_Customer(CustomerModel _CustomerModel)
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					TableName = _CustomerModel.GetType().Name,
					StoredProcedureName = @"SP_Customer_CURD",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};

				objQueryBuilder.AddFieldValue("@Id", _CustomerModel.Id, DataTypes.Numeric, false);
				objQueryBuilder.AddFieldValue("@CompanyId", _CustomerModel.CompanyId, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Name", _CustomerModel.CustomerName +" "+ _CustomerModel.LastName, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Address", _CustomerModel.CompanyName, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@Website", _CustomerModel.CompanySite, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@EmailId", _CustomerModel.EmailAddress, DataTypes.Text, false);
				objQueryBuilder.AddFieldValue("@PhoneNumber", _CustomerModel.ContactPhone, DataTypes.Text, false);
				//objQueryBuilder.AddFieldValue("@CreatedBy", _CustomerModel.CreatedBy, DataTypes.Text, false);
				//objQueryBuilder.AddFieldValue("@ModifiedBy", _CustomerModel.ModifiedBy, DataTypes.Text, false); 
				objQueryBuilder.AddFieldValue("@ActionType", _CustomerModel.ActionType, DataTypes.Text, false);
				
				return objgm.ExecuteObjectUsingSp<bool>(objQueryBuilder, true);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}

		public CustomerModel GetCustomerList()
		{
			try
			{
				QueryBuilder objQueryBuilder = new QueryBuilder
				{
					StoredProcedureName = @"SP_Customer_List",
					SetQueryType = QueryBuilder.QueryType.SELECT
				};

				return objgm.ExecuteObjectUsingSp<CustomerModel>(objQueryBuilder, true);
			}
			catch (Exception ex)
			{
				ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
				throw;
			}
		}
	}
}