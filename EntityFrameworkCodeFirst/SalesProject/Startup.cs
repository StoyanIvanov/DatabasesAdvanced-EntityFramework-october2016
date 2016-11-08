namespace SalesProject
{
    class Startup
    {
        static void Main()
        {
            SalesContext contex = new SalesContext();

            contex.SaveChanges();
        }
    }
}
