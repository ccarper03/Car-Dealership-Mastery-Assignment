using GuildCars.Models.Tables;
using System.Collections.Generic;

namespace GuildCars.Data.Interfaces
{
    public interface IPurchaseLogRepository
    {
        IEnumerable<PurchaseLog> GetPurchaseLogs();
        void Insert(PurchaseLog PurchaseLog);
    }
}
