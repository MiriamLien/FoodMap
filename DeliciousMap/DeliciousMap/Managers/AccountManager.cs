using DeliciousMap.Helpers;
using DeliciousMap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DeliciousMap.Managers
{
    public class AccountManager
    {
        public bool TryLogin(string account, string password)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;

            AccountModel member = this.GetAccount(account);

            if (member == null)
                return false;

            if (string.Compare(member.Account, account, true) == 0)
                isAccountRight = true;

            if (member.Password == password)
                isPasswordRight = true;

            bool result = (isAccountRight && isPasswordRight);

            if (result)
            {
                {
                    member.Password = null;
                    HttpContext.Current.Session["MemberAccount"] = member;
                };
            }

            return result;
        }
        public bool IsLogined()
        {
            AccountModel account = GetCurrentUser();
            return account != null;
        }
        public AccountModel GetCurrentUser()
        {
            AccountModel account = HttpContext.Current.Session["MemberAccount"] as AccountModel;
            return account;
        }
        public void Logout()
        {
            HttpContext.Current.Session.Remove("MemberAccount");
        }
        //增刪修查
        public AccountModel GetAccount(string account)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = @"SELECT *
                                 FROM accounts
                                 WHERE Account = @Account";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@account", account);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            AccountModel member = BuildAccountModel(reader);
                            return member;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.GetAccount", ex);
                throw;
            }
        }
        private AccountModel BuildAccountModel(SqlDataReader reader)
        {
            AccountModel model = new AccountModel()
            {
                ID = (Guid)reader["ID"],
                Account = reader["Account"] as string,
                Password = reader["PWD"] as string,
                UserLevel = (UserLevelEnum)reader["UserLevel"]
            };
            return model;
        }
        public List<AccountModel> GetAccountList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = @"SELECT *
                                   FROM [accounts]";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                commandText += "WHERE [Account] LIKE '%'+@keyword+'%'";
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();

                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }
                        SqlDataReader reader = command.ExecuteReader();
                        List<AccountModel> list = new List<AccountModel>();

                        while (reader.Read())
                        {
                            AccountModel member = this.BuildAccountModel(reader);
                            member.Password = null;
                            list.Add(member);
                        }
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.GetAccountList", ex);
                throw;
            }
        }
        public AccountModel GetAccount(Guid id)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = @"SELECT *
                                 FROM accounts
                                 WHERE ID = @id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            AccountModel member = this.BuildAccountModel(reader);
                            return member;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.GetAccount", ex);
                throw;
            }
        }
        public void CreateAccount(AccountModel member)
        {
            if (this.GetAccount(member.Account) != null)
                throw new Exception("已存在相同的帳號");

            //2.新增資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = @"INSERT INTO accounts
                                     (ID, Account, PWD, UserLevel)
                                  VALUES
                                     (@id, @account, @pwd, @UserLevel)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        member.ID = Guid.NewGuid();

                        command.Parameters.AddWithValue("@id", member.ID);
                        command.Parameters.AddWithValue("@account", member.Account);
                        command.Parameters.AddWithValue("@pwd", member.Password);
                        command.Parameters.AddWithValue("@UserLevel", (int)member.UserLevel);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.CreateAccount", ex);
                throw;
            }
        }
        public void UpdateAccount(AccountModel member)
        {
            if (this.GetAccount(member.Account) == null)
                throw new Exception("帳號不存在");

            //2.編輯資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = @"UPDATE accounts
                                   SET  PWD = @pwd
                                        UserLevel = @UserLevel
                                   WHERE ID = @id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@id", member.ID);
                        command.Parameters.AddWithValue("@pwd", member.Password);
                        command.Parameters.AddWithValue("@UserLevel", (int)member.UserLevel);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.UpdateAccount", ex);
                throw;
            }
        }
        public void DeleteAccounts(List<Guid> ids)
        {
            //1.半段是否有傳入 id
            if (ids == null || ids.Count == 0)
                throw new Exception("帳號不存在");

            List<string> param = new List<string>();
            for (var i = 0; i < ids.Count; i++)
            {
                param.Add("@id" + i);
            }
            string insql = string.Join(",", param);

            //2.編輯資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"DELETE accounts
                                   WHERE ID IN ({insql})";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        for (var i = 0; i < ids.Count; i++)
                        {
                            command.Parameters.AddWithValue("@id" + i, ids[i]);
                        }
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountManager.DeleteAccounts", ex);
                throw;
            }
        }
    }
}