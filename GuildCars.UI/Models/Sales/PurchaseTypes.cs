using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Sales
{
    public static class PurchaseTypes
    {
        private static List<string> _purchaseTypes;

        static PurchaseTypes()
        {
            _purchaseTypes = new List<string> { "Bank Finance", "Cash", "Dealer Finance" };
        }

        public static List<string> GetPurchaseTypes()
        {
            return _purchaseTypes;
        }
    }
}