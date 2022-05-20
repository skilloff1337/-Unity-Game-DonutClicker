using System.Collections.Generic;
using _11._Shop.Data;
using _5._DataBase.Data;
using MongoDB.Bson;

namespace _5._DataBase.Interfaces
{
    public interface IRepository
    {
        IEnumerable<PlayerData> GetAll();
        PlayerData GetById(ObjectId id);
        void Create(PlayerData item);
        void Update(PlayerData item);
        void Delete(ObjectId id);
        bool HasAccount(ObjectId id);
        PlayerData[] GetTopPlayerDonuts();
        PlayerData[] GetTopPlayerClicks();
        PlayerData[] GetTopPlayerDps();
        

        bool Connection();
    }
}