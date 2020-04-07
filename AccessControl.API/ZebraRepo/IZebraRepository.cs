using System.Collections.Generic;
using System.Threading.Tasks;
using AccessControl.API.ZebraModels;

namespace AccessControl.API.ZebraRepo
{
    public interface IZebraRepository
    {
         Task<bool> AddRmaReceiving(RMA_Receiving receiving);
         Task<Tray_Detail> GetTrayDetail(string tray_ID);
         Task<List<Tray_Detail>> GetAvaliableTray();
         Task<Zebra_Station> GetStation(int station_ID);
 
         Task<Zebra_User> GetUser(string user_ID);
         void UpdateUser(Zebra_User user);
         void DeleteUser(string user_ID);

         Task<List<Zebra_User>> GetUsers();

         
    }
}