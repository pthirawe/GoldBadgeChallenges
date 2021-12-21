using _02_KomodoClaims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaims.UI
{
    public class ClaimsUI
    {
        private readonly ClaimRepository _claimRepo;

        public ClaimsUI()
        {
            _claimRepo = new ClaimRepository();
        }

        public void Run()
        {
            Seed();
            DisplayMenu();
        }
        //Menu
        public void DisplayMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(
                    "Welcome to Komodo Claims Processing\n" +
                    "Please select from available options:\n" +
                    "1. View All Claims\n" +
                    "2. Handle Next Claim\n" +
                    "3. Enter New Claim\n" +
                    "0. Exit"
                    );
                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "0":
                        return;
                    case "1":
                        ListAllClaims();
                        break;
                    case "2":
                        ProcessNextClaim();
                        break;
                    case "3":
                        EnterNewClaim();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        break;
                }
            }
        }
        //List Claims
        public void ListAllClaims()
        {
            Console.WriteLine("                       ============Active Claims============");
            Console.WriteLine("=ID=|=Type=|=======Description=======|==Amount==|Incident Date|Claim Date|Is Valid");
            foreach (Claim claim in _claimRepo.ListClaims())
            {
                DisplayClaimList(claim);
            }
            WaitForKey();
        }
        //Process claim
        public void ProcessNextClaim()
        {
            DisplayClaimData(_claimRepo.DisplayNextClaim());
            Console.WriteLine("Would you like to handle this claim now?");

            if(!ConfirmSelection())
            {
                Console.WriteLine("Cancelled.  Returning to main menu.");
                WaitForKey();
                return;
            }
            _claimRepo.ProcessClaim();
        }

        //New Claim
        public void EnterNewClaim()
        {
            int newID;
            ClaimType type;
            string description;
            double damageValue;
            DateTime incidentDate = new DateTime();
            DateTime claimDate = new DateTime();
            Claim newClaim;

            Console.Write("Enter claim ID: ");
            if(!Int32.TryParse(Console.ReadLine(), out newID))
            {
                Console.WriteLine("Invalid ID. Returning to main menu.");
                WaitForKey();
                return;
            }
            Console.Write("Enter claim Type (Car/Home/Theft): ");
            switch(Console.ReadLine().ToLower())
            {
                case "car":
                    type = ClaimType.Car;
                    break;
                case "home":
                    type = ClaimType.Home;
                    break;
                case "theft":
                    type = ClaimType.Theft;
                    break;
                default:
                    Console.WriteLine("Invalid claim type. Returning to main menu.");
                    return;
            }
            Console.Write("Enter claim description: ");
            description = Console.ReadLine();
            Console.Write("Enter claim amount: ");
            if(!Double.TryParse(Console.ReadLine(), out damageValue))
            {
                Console.WriteLine("Invalid amount. Returning to main menu.");
                WaitForKey();
                return;
            }
            Console.Write("Enter Date of Incident: ");
            try
            {
                incidentDate = DateTime.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to convert input to a date. Returning to main menu.");
                WaitForKey();
                return;
            }
            Console.Write("Enter Date of Claim: ");
            try
            {
                claimDate = DateTime.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to convert input to a date. Returning to main menu.");
                WaitForKey();
                return;
            }
            newClaim = new Claim(newID, type, description, damageValue, incidentDate, claimDate);
            if(!_claimRepo.AddToQueue(newClaim))
            {
                Console.WriteLine("Error adding claim to queue.  Returning to main menu.");
                WaitForKey();
                return;
            }
        }

        //Helper Methods
        //Wait for Key Press
        private void WaitForKey()
        {
            Console.WriteLine("Press Any Key to Continue.");
            Console.ReadKey();
        }
        //Display Claim Information
        public void DisplayClaimData(Claim claimToShow)
        {
            if (claimToShow == null)
            {
                Console.WriteLine("Claim does not exist.");
                return;
            }
            Console.WriteLine(
                $"Claim ID: {string.Format("{0:0000}", claimToShow.ID)} \n" +
                $"Type: " + ClaimTypeToText(claimToShow.Type) + "\n" +
                $"Description: {claimToShow.Description}\n" +
                $"Amount: {string.Format("{0:0.00}", claimToShow.ClaimAmount)}\n" +
                $"Date of Accident: {claimToShow.DateOfIncident.ToString("MM/dd/yy")}\n" +
                $"Date of Claim: {claimToShow.DateOfClaim.ToString("MM/dd/yy")}\n" +
                $"This claim is " + (claimToShow.IsValid ? "valid." : "invalid.")
                );
            Console.WriteLine("----------------------------------");
        }

        public void DisplayClaimList(Claim claimToShow)
        {
            if (claimToShow == null)
            {
                Console.WriteLine("Claim does not exist.");
                return;
            }
            Console.WriteLine(
                $"{string.Format("{0:0000}", claimToShow.ID)}"+ "|" +
                $"" + ClaimTypeToText(claimToShow.Type).PadRight(6) + "|" +
                $"{claimToShow.Description}".PadRight(25) + "|" +
                $"${string.Format("{0:0.00}", claimToShow.ClaimAmount)}".PadRight(10) + "|" +
                $"{claimToShow.DateOfIncident.ToString("MM/dd/yy")}".PadRight(13) + "|" +
                $"{claimToShow.DateOfClaim.ToString("MM/dd/yy")}".PadRight(10) +"|" +
                $"" + (claimToShow.IsValid ? "Valid." : "Invalid.").PadRight(8)
                );
            Console.WriteLine("".PadRight(82,'-'));
        }

        //Confirm selection
        private bool ConfirmSelection()
        {
            bool isValid;
            do
            {
                string confirm = Console.ReadLine();
                isValid = (confirm.ToLower() == "y") || (confirm.ToLower() == "n");

                switch (confirm.ToLower())
                {
                    case "y":
                        return true;
                    case "n":
                        return false;
                    default:
                        Console.WriteLine("Please use y or n to respond.");
                        break;
                }

            } while (!isValid);
            return false;
        }

        //Convert Enum to String
        private string ClaimTypeToText(ClaimType claimType)
        {
            switch (claimType)
            {
                case ClaimType.Car:
                    return "Car";
                case ClaimType.Home:
                    return "Home";
                case ClaimType.Theft:
                    return "Theft";
                default:
                    return "Invalid Claim Type";
            }
        }

        //Debug
        public void Seed()
        {
            Claim test0 = new Claim(0, ClaimType.Car, "Hokey Pokey", 0.99, DateTime.Parse("05/10/2021"), DateTime.Parse("06/03/2021"));
            Claim test1 = new Claim(1, ClaimType.Home, "Pikachu", 30, DateTime.Parse("05/10/2021"), DateTime.Parse("05/11/2021"));
            Claim test2 = new Claim(2, ClaimType.Car, "10 car pileup", 10000, DateTime.Parse("05/10/2021"), DateTime.Parse("06/20/2021"));
            Claim test3 = new Claim(3, ClaimType.Car, "T-Boned", 5000, DateTime.Parse("05/10/2021"), DateTime.Parse("07/03/2021"));
            Claim test4 = new Claim(4, ClaimType.Theft, "Stolen wiper blades", 19.99, DateTime.Parse("05/10/2021"), DateTime.Parse("06/01/2021"));
            Claim test5 = new Claim(5, ClaimType.Theft, "Radio stolen from vehicle", 200, DateTime.Parse("05/10/2021"), DateTime.Parse("05/12/2021"));
            Claim test6 = new Claim(6, ClaimType.Car, "Cracked windshield", 198.99, DateTime.Parse("05/10/2021"), DateTime.Parse("08/03/2021"));
            Claim test7 = new Claim(7, ClaimType.Car, "broken tail light", 50.49, DateTime.Parse("04/10/2021"), DateTime.Parse("05/03/2021"));
            Claim test8 = new Claim(8, ClaimType.Car, "Popped tire", 25.99, DateTime.Parse("05/10/2021"), DateTime.Parse("06/10/2021"));
            Claim test9 = new Claim(9, ClaimType.Car, "Superficial damage", 50, DateTime.Parse("05/10/2021"), DateTime.Parse("06/12/2021"));

            _claimRepo.AddToQueue(test0);
            _claimRepo.AddToQueue(test1);
            _claimRepo.AddToQueue(test2);
            _claimRepo.AddToQueue(test3);
            _claimRepo.AddToQueue(test4);
            _claimRepo.AddToQueue(test5);
            _claimRepo.AddToQueue(test6);
            _claimRepo.AddToQueue(test7);
            _claimRepo.AddToQueue(test8);
            _claimRepo.AddToQueue(test9);

        }
    }
}
