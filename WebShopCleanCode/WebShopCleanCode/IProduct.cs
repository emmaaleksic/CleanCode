namespace WebShopCleanCode
{
    public interface IProduct
    {
        string GetName();
        int GetPrice();
        int GetNrInStock();

        void PrintInfo();
        bool InStock();


    }
}
