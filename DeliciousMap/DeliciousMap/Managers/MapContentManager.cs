using DeliciousMap.Helpers;
using DeliciousMap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DeliciousMap.Managers
{
    public class MapContentManager
    {
        public List<MapContentModels> GetMapList(string keyword, int pageSize, int pageIndex, out int totalrows)
        {
            int skip = pageSize * (pageIndex - 1); //計算跳頁
            if (skip < 0)
                skip = 0;

            string whereCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(keyword))
                whereCondition = "AND Title LIKE '%' + @keyword + '%'";


            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"  SELECT TOP {pageSize} *
                     FROM [FoodContents]
                     WHERE 
                        IsEnable = 'true' AND
                        ID NOT IN (
                            SELECT TOP {skip} ID
                            FROM [FoodContents]
                            WHERE  IsEnable = 'true' 
                                   {whereCondition}
                            ORDER BY CreateDate DESC
                        )
                        {whereCondition}
                     ORDER BY CreateDate DESC ";
            string commandCountText =
                                 $@"SELECT COUNT (ID)
                                     FROM [FoodContents]
                                  WHERE 
                                     IsEnable = 'true'
                                     {whereCondition}";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))  // 參數化查詢  
                            command.Parameters.AddWithValue("@keyword", keyword);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // 將資料庫內容轉為自定義型別清單
                        List<MapContentModels> retList = new List<MapContentModels>();
                        while (reader.Read())
                        {
                            MapContentModels info = this.BuildMapContentModel(reader);
                            retList.Add(info);
                        }
                        reader.Close();

                        //取得總比數
                        command.CommandText = commandCountText;
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }
                        totalrows = (int)command.ExecuteScalar();

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMapList", ex);
                throw;
            }
        }

        public List<MapContentModels> GetAdminMapList(string keyword)
        {
            string whereCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(keyword))
                whereCondition = " WHERE Title LIKE '%'+@keyword+'%' ";

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM FoodContents 
                    {whereCondition}
                    ORDER BY CreateDate DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<MapContentModels> retList = new List<MapContentModels>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            MapContentModels info = this.BuildMapContentModel(reader);
                            retList.Add(info);
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetAdminMapList", ex);
                throw;
            }
        }

        public List<MapContentModels> GetMapList(List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
                return new List<MapContentModels>();

            List<string> idTextList = new List<string>();
            for (int i = 0; i < ids.Count; i++)
            {
                idTextList.Add("@id" + i);
            }
            string whereCondition = string.Join(", ", idTextList);


            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM FoodContents 
                    WHERE ID IN ({whereCondition}) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        for (int i = 0; i < ids.Count; i++)
                        {
                            command.Parameters.AddWithValue("@id" + i, ids[i]);
                        }


                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // 將資料庫內容轉為自定義型別清單
                        List<MapContentModels> retList = new List<MapContentModels>();
                        while (reader.Read())
                        {
                            MapContentModels info = this.BuildMapContentModel(reader);
                            retList.Add(info);
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMapList", ex);
                throw;
            }
        }

        private MapContentModels BuildMapContentModel(SqlDataReader reader)
        {
            var model = new MapContentModels()
            {
                ID = (Guid)reader["ID"],   //指定欄位名稱
                Title = reader["Title"] as string,
                Body = reader["Body"] as string,
                CoverImage = reader["CoverImage"] as string,
                IsEnable = (bool)reader["IsEnable"],
                Longitude = (double)reader["Longitude"],
                Latitude = (double)reader["Latitude"],

                //管理用欄位
                CreateDate = (DateTime)reader["CreateDate"],
                CreateUser = (Guid)reader["CreateUser"],
                UpdateDate = reader["UpdateDate"] as DateTime?,
                UpdateUser = reader["UpdateUser"] as Guid?,
                DeleteDate = reader["DeleteDate"] as DateTime?,
                DeleteUser = reader["DeleteUser"] as Guid?,
            };
            return model;
        }

        public MapContentModels GetMap(Guid id)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM FoodContents 
                    WHERE ID = @id ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        MapContentModels model = new MapContentModels();
                        if (reader.Read())
                        {
                            model = this.BuildMapContentModel(reader);
                        }

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMap", ex);
                throw;
            }
        }

        public void CreateMapContent(MapContentModels Model, Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText = @"INSERT INTO [FoodContents]
                                   (ID, Title, Body, Longitude, Latitude, CoverImage, IsEnable, CreateDate, CreateUser)
                                  VALUES
                                   (@ID, @Title, @Body, @Longitude, @Latitude ,@CoverImage, @IsEnable, @CreateDate, @CreateUser)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        Model.ID = Guid.NewGuid();
                        conn.Open();

                        command.Parameters.AddWithValue("@ID", Model.ID);
                        command.Parameters.AddWithValue("@Title", Model.Title);
                        command.Parameters.AddWithValue("@Body", Model.Body);
                        command.Parameters.AddWithValue("@Latitude", Model.Latitude);
                        command.Parameters.AddWithValue("@Longitude", Model.Longitude);
                        command.Parameters.AddWithValue("@CoverImage", Model.CoverImage);
                        command.Parameters.AddWithValue("@IsEnable", Model.IsEnable);
                        command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        command.Parameters.AddWithValue("@CreateUser", UserID);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.CreateMapContent", ex);
                throw;
            }
        }
        public void UpdateMapContent(MapContentModels Model)
        {
        }
        public void DeleteMapContent(List<Guid> idList)
        {
            if (idList == null || idList.Count == 0)
                throw new Exception("ID List is required");

            List<string> idTextList = new List<string>();
            for (int i = 0; i < idList.Count; i++)
            {
                idTextList.Add("@id" + i);
            }
            string whereCondition = string.Join(",", idTextList);

            string connStr = ConfigHelper.GetConnectionString();
            string commandText = $@"DELETE [FoodContents]
                                   WHERE
                                       ID IN ({whereCondition});";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        for (int i = 0; i < idList.Count; i++)
                        {
                            command.Parameters.AddWithValue("@Id" + i, idList[i]);
                        }
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.DeleteMapContent", ex);
                throw;
            }
        }
    }
}