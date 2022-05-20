namespace _11._Shop.Scripts
{
    public class ShopPriceCalculation
    {
        public double CostItem(double level, double firstCost, float multiplyCost) =>
            level == 0 ? firstCost:
            level is >= 1 and <= 10 ? level * firstCost * multiplyCost * level:
            level is >= 11 and <= 50 ? level * firstCost * multiplyCost * 20 * level:
            level is >= 51 and <= 100 ? level * firstCost * multiplyCost * 40 * level:
            level is >= 101 and <= 200 ? level * firstCost * multiplyCost * 100 * level:
            level is >= 201 and <= 500 ? level * firstCost * multiplyCost * 500 * level:
            level is >= 501 and <= 1_000 ? level * firstCost * multiplyCost * 1000 * level:
            level is >= 1_001 and <= 2_000 ? level * firstCost * multiplyCost * 2000 * level:
            level * firstCost * multiplyCost * 10_000 * level;
    }
}