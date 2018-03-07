using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RoomValue.Models
{
    public class MemberModel
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Mail { get; set; }
        public String Phone { get; set; }
    }
    public class AdminModel
    {
        public static int AddMember(Models.MemberModel member)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("AddMember", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Name", member.Name));
            sqlCommand.Parameters.Add(new SqlParameter("@Mail", member.Mail));
            sqlCommand.Parameters.Add(new SqlParameter("@Phone", member.Phone));
            sqlCommand.Parameters.Add(new SqlParameter("@Password", Common.CommonClass.Hash(member.Mail)));
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            int result = int.Parse(sqlDataReader[0].ToString());
            sqlConnection.Close();
            return result;
        }
        public static List<RoomValue.Models.MemberModel> GetAllMembersDetails()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetAllMembersDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<RoomValue.Models.MemberModel> memberList = new List<MemberModel>();
            while (sqlDataReader.Read())
            {
                MemberModel member = new MemberModel();
                member.ID = int.Parse(sqlDataReader[0].ToString());
                member.Name = sqlDataReader[1].ToString();
                memberList.Add(member);
            }
            sqlConnection.Close();
            return memberList;
        }
        public static bool RemoveMember(int ID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("RemoveMember", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            if (sqlDataReader[0].ToString().Equals("1"))
                return true;
            else
                return false;
        }
        public static List<RoomValue.Models.MemberModel> GetAllMembersFullDetails()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("GetAllMembersFullDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<RoomValue.Models.MemberModel> memberList = new List<MemberModel>();
            while (sqlDataReader.Read())
            {
                MemberModel member = new MemberModel();
                member.ID = int.Parse(sqlDataReader[0].ToString());
                member.Name = sqlDataReader[1].ToString();
                member.Mail = sqlDataReader[2].ToString();
                member.Phone = sqlDataReader[3].ToString();
                memberList.Add(member);
            }
            sqlConnection.Close();
            return memberList;
        }
    }
}