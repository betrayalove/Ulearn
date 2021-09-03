// Вставьте сюда финальное содержимое файла AccountingModel.cs

using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get => price;
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                CreateNewTotal();
            }
        }

        public int NightsCount
        {
            get => nightsCount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                CreateNewTotal();
            }
        }

        public double Discount
        {
            get => discount;
            set
            {
                discount = value;
                Notify(nameof(Discount));
                CreateNewTotal();
            }
        }

        public double Total
        {
            get => total;
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                total = value;
                Notify(nameof(Total));
                CreateNewDiscount();
            }
        }
        public void CreateNewTotal()
        {
            if (Price * NightsCount * (1 - Discount / 100) < 0)
                throw new ArgumentException();
            total = Price * NightsCount * (1 - Discount / 100);
            Notify(nameof(Total));
        }

        public void CreateNewDiscount()
        {
            discount = ((-1) * Total / (Price * NightsCount) + 1) * 100;
            Notify(nameof(Discount));
        }
    }
}