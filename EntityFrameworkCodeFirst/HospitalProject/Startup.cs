using HospitalProject.Models;

namespace HospitalProject
{
    public class Startup
    {
        static void Main()
        {
            HospitalContext context = new HospitalContext();

            Medicament medicament = new Medicament
            {
                Name = "Analgin"
            };
            

            context.Medicaments.Add(medicament);
            context.SaveChanges();
        }
    }
}
