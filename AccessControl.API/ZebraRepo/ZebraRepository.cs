using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccessControl.API.Domain;
using AccessControl.API.Repositories.DbConnection;
using AccessControl.API.ZebraModels;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace AccessControl.API.ZebraRepo
{
    public class ZebraRepository : ConnectionRepositoryBase, IZebraRepository
    {

        public ZebraRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory, 1)
        { }
        public async Task<bool> AddRmaReceiving(RMA_Receiving receiving)
        {
            if (await base.DbConnection.GetAsync<RMA_Receiving>(receiving.RMA_No) == null)
            {
                List<RMA_Receiving> uploadList = new List<RMA_Receiving>();
                var idFromDb = await GetAvaliableTray();
                var count = (receiving.Qty / 14) + 1;
                if (await GetAvaliableTrayCount() >= (receiving.Qty / 14) + 1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        RMA_Receiving uploadReceiving = new RMA_Receiving();
                        if (receiving.Qty > 14)
                        {
                            uploadReceiving.Qty = receiving.Qty - (receiving.Qty - 14);
                        }
                        else
                        {
                            uploadReceiving.Qty = receiving.Qty;
                        }

                        if (idFromDb != null)
                        {
                            uploadReceiving.Tray_ID = idFromDb[i].Tray_ID;
                            uploadReceiving.CreateDate = DateTime.Now;
                            uploadReceiving.CreateBy = receiving.CreateBy;
                            uploadReceiving.PN= receiving.PN;
                            uploadReceiving.RMA_No = receiving.RMA_No;
                            idFromDb[i].IsEmpty = false;
                            idFromDb[i].LastModifyBy = receiving.CreateBy;
                            idFromDb[i].LastModifyDate = DateTime.Now;
                            idFromDb[i].Tray_Item_Count = uploadReceiving.Qty;
                            idFromDb[i].Current_Station_ID = 0;
                            uploadList.Add(uploadReceiving);
                        }
                        receiving.Qty -= 14;
                    }
                    base.DbConnection.Insert(uploadList);
                    if(idFromDb.Count > count){
                        idFromDb.RemoveAt(idFromDb.Count - 1);
                    }
                    base.DbConnection.Update(idFromDb);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }

        public void DeleteUser(string user_ID)
        {
            base.DbConnection.Delete(user_ID);
        }

        public async Task<List<Tray_Detail>> GetAvaliableTray()
        {
            string query = @"Select * From dbo.Zebra_Tray_Detail WHERE IsEmpty = @isEmpty Order By Tray_ID ASC";

            var result = await base.DbConnection.QueryAsync<Tray_Detail>(query, new
            {
                @isEmpty = true,

            });
            if (result != null)
            {
                return result.ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetAvaliableTrayCount()
        {
            string query = @"Select Count(*) From dbo.Zebra_Tray_Detail WHERE IsEmpty = @isEmpty";

            return await base.DbConnection.QuerySingleAsync<int>(query, new
            {
                @isEmpty = true
            });
        }

        public async Task<Zebra_Station> GetStation(int station_ID)
        {

            return await base.DbConnection.GetAsync<Zebra_Station>(station_ID);

        }

        public async Task<Tray_Detail> GetTrayDetail(string tray_ID)
        {

            return await base.DbConnection.GetAsync<Tray_Detail>(tray_ID);
        }

        public async Task<Zebra_User> GetUser(string user_ID)
        {
            syncUserID();
            throw new NotImplementedException();
        }

        public async Task<List<Zebra_User>> GetUsers()
        {
            syncUserID();
            var usersFromDb = await base.DbConnection.GetAllAsync<Zebra_User>();
            
            return usersFromDb.ToList();
        }

        public void UpdateUser(Zebra_User user)
        {
            base.DbConnection.Update(user);
        }
        public async void syncUserID(){
            string query = @"Select User_ID FROM dbo.User_Permission_View WHERE Group_ID = @id";

            var result = await base.DbConnection.QueryAsync<Zebra_User>(query, new{
                @id = "gp000099"
            });

            base.DbConnection.Insert(result);
        }
    }
}