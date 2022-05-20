using _11._Shop.Data;

namespace _11._Shop.Scripts
{
    public interface IShopSystem
    {
        ShopData[] ShopItems { get; set; }
        void BuyShopItem(int itemId);
        void CreateShopItemPrefab();
        void CalculatorDonutPerSeconds();
    }
}