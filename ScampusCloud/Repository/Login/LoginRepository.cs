using ScampusCloud.DataBase;
using ScampusCloud.Models;
using ScampusCloud.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScampusCloud.Repository.Login
{
    public class LoginRepository
    {
        private GeneralMethods objgm = new GeneralMethods();
        public UserModel Get_User(LoginModel _LoginModel)
        {
            try
            {
                QueryBuilder objQueryBuilder = new QueryBuilder
                {
                    TableName = _LoginModel.GetType().Name,
                    StoredProcedureName = @"SP_User_Login",
                    SetQueryType = QueryBuilder.QueryType.SELECT
                };

                objQueryBuilder.AddFieldValue("@username", _LoginModel.EmailId, DataTypes.Text, false);
                objQueryBuilder.AddFieldValue("@password", EncryptionDecryption.GetEncrypt(_LoginModel.Password), DataTypes.Text, false);

                return objgm.ExecuteObjectUsingSp<UserModel>(objQueryBuilder, true);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.ToString() : string.Empty, this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}