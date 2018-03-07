using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RoomValue.Models
{
    public class DetailsModel
    {
        public String EMail { get; set; }
        public String Phone { get; set; }
    }
    public class HaveToPayModel
    {
        public String ExpenditureID { get; set; }
        public String Item { get; set; }
        public String Amount { get; set; }
    }
    public class GotPaidModel
    {
        public String ExpenditureID { get; set; }
        public String Item { get; set; }
        public String TotalAmount { get; set; }
        public String PaidAmount { get; set; }
        public String Payer { get; set; }
    }
    public class ExpenditureForPersonModel
    {
        public String ExpenditureID { get; set; }
        public String Item { get; set; }
        public String TotalAmount { get; set; }
        public String Status { get; set; }
        public String PaidBy { get; set; }
        public String PaidTo { get; set; }
        public String Date { get; set; }
        public String PayType { get; set; }
        public String BalanceAmount { get; set; }
    }
    public class UserModel
    {
        public static int GetMembersCount()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetMembersCount", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            int count = int.Parse(sqlDataReader[0].ToString());
            sqlConnection.Close();
            return count;
        }
        public static List<Member> GetMembersIHaveToPay()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("MembersIHaveToPay", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@ID",System.Web.HttpContext.Current.Session["UserName"].ToString()));
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<String> list = new List<string>();
            while (sqlDataReader.Read())
            {
                list.Add(sqlDataReader[0].ToString());
            }
            sqlDataReader.Close();
            List<Member> nameList = new List<Member>();
            foreach (String temp in list)
            {
                SqlCommand sqlCommandForName = new SqlCommand("GetNameFromID", sqlConnection);
                sqlCommandForName.CommandType = CommandType.StoredProcedure;
                sqlCommandForName.Parameters.Add(new SqlParameter("@ID", temp));
                SqlDataReader sqlDataReaderForName = sqlCommandForName.ExecuteReader();
                sqlDataReaderForName.Read();
                Member tempMember = new Member();
                tempMember.ID = temp;
                tempMember.Name = sqlDataReaderForName[0].ToString();
                nameList.Add(tempMember);
                sqlDataReaderForName.Close();
            }
            sqlConnection.Close();
            return nameList;
        }
        public static List<String> GetAllMembers()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetAllMembers", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<String> list = new List<string>();
            while (sqlDataReader.Read())
            {
                list.Add(sqlDataReader[0].ToString());
            }
            sqlConnection.Close();
            return list;
        }
        internal static bool ExpenditureMade(string item, decimal perHead, List<string> checkboxList,String type)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
                sqlConnection.Open();
                foreach (String checkBoxListItem in checkboxList)
                {
                    SqlCommand sqlCommand = new SqlCommand("ExpenditureMade", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@Item", item));
                    sqlCommand.Parameters.Add(new SqlParameter("@Amount", perHead));
                    sqlCommand.Parameters.Add(new SqlParameter("@PaidTo", checkBoxListItem));
                    sqlCommand.Parameters.Add(new SqlParameter("@PayType", type));
                    sqlCommand.Parameters.Add(new SqlParameter("@PaidBy", System.Web.HttpContext.Current.Session["UserName"].ToString()));
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                }
                sqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public static List<HaveToPayModel> GetHaveToPayList(String payeeID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetHaveToPayList", sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter("@PayeeID", payeeID));
            sqlCommand.Parameters.Add(new SqlParameter("@PayerID", System.Web.HttpContext.Current.Session["UserName"].ToString()));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<HaveToPayModel> list = new List<HaveToPayModel>();
            while (sqlDataReader.Read())
            {
                HaveToPayModel haveToPay = new HaveToPayModel();
                haveToPay.ExpenditureID = sqlDataReader[0].ToString();
                haveToPay.Item = sqlDataReader[1].ToString();
                haveToPay.Amount = sqlDataReader[2].ToString();
                list.Add(haveToPay);
            }
            sqlConnection.Close();
            return list;
        }
        internal static bool CombinedPayment(string payTo)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("CombinedPayment", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@PayerID", System.Web.HttpContext.Current.Session["UserName"].ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@PayeeID", payTo));
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlConnection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        internal static bool SinglePayment(string expenditureID,string amount)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SinglePayment", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@ExpenditureID",expenditureID));
                sqlCommand.Parameters.Add(new SqlParameter("@Amount", amount));
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static List<GotPaidModel> GetGotPaidList()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetGotPaidList", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@ID", System.Web.HttpContext.Current.Session["UserName"].ToString()));
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<GotPaidModel> list = new List<GotPaidModel>();
            while (sqlDataReader.Read())
            {
                GotPaidModel tempGotPaid = new GotPaidModel();
                tempGotPaid.ExpenditureID = sqlDataReader[0].ToString();
                tempGotPaid.Item = sqlDataReader[1].ToString();
                tempGotPaid.TotalAmount = sqlDataReader[2].ToString();
                tempGotPaid.PaidAmount = sqlDataReader[3].ToString();
                tempGotPaid.Payer = sqlDataReader[4].ToString();
                list.Add(tempGotPaid);
            }
            sqlConnection.Close();
            return list;
        }
        public static int RejectApproval(String expenditureID)
        {
            try
            {

                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("RejectPayment", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@ID", System.Web.HttpContext.Current.Session["UserName"].ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@ExpenditureID", expenditureID));
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                int result = int.Parse(sqlDataReader[0].ToString());
                sqlConnection.Close();
                return result;
            }
            catch (Exception)
            {

                return 3;
            }
        }
        public static int AcceptApproval(String expenditureID)
        {
            try
            {

                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("AcceptPayment", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@ID", System.Web.HttpContext.Current.Session["UserName"].ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@ExpenditureID", expenditureID));
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                int result = int.Parse(sqlDataReader[0].ToString());
                sqlConnection.Close();
                return result;
            }
            catch (Exception)
            {

                return 3;
            }
        }
        public static DetailsModel GetUserDetails()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetUserDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@ID", System.Web.HttpContext.Current.Session["UserName"].ToString()));
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            DetailsModel details = new DetailsModel();
            details.EMail = sqlDataReader[0].ToString();
            details.Phone = sqlDataReader[1].ToString();
            sqlConnection.Close();
            return details;
        }

        public static bool UpdateUserDetails(String mail, String phone)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UpdateUserDetails", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@ID", System.Web.HttpContext.Current.Session["UserName"].ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@Mail", mail));
                sqlCommand.Parameters.Add(new SqlParameter("@Phone", phone));
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<ExpenditureForPersonModel> GetExpenditureDetails()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetExpenditureDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@ID", System.Web.HttpContext.Current.Session["UserName"].ToString()));
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<ExpenditureForPersonModel> list = new List<ExpenditureForPersonModel>();
            while (sqlDataReader.Read())
            {
                ExpenditureForPersonModel expenditureItem = new ExpenditureForPersonModel();
                expenditureItem.Date = sqlDataReader[0].ToString().Split(' ')[0];
                expenditureItem.Item = sqlDataReader[1].ToString();
                expenditureItem.TotalAmount = sqlDataReader[2].ToString();
                expenditureItem.PaidBy = GetNameByID(sqlDataReader[3].ToString());
                expenditureItem.PaidTo = GetNameByID(sqlDataReader[4].ToString());
                expenditureItem.PayType = GetFullType(sqlDataReader[5].ToString());
                expenditureItem.Status = sqlDataReader[6].ToString();
                expenditureItem.BalanceAmount = sqlDataReader[7].ToString();
                list.Add(expenditureItem);
            }
            sqlConnection.Close();
            return list;
        }
        public static String GetNameByID(String ID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetNameFromID", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            String name = sqlDataReader[0].ToString();
            sqlConnection.Close();
            return name;
        }
        public static String GetFullType(String status)
        {
            if(status.Equals("All"))
                return "All People";
            else if(status.Equals("WMe"))
                return "With Me";
            else
                return "Without Me";
        }
    }
}