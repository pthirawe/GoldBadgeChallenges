using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_KomodoClaims.Repository
{
    public enum ClaimType { Car, Home, Theft }
    public class Claim
    {
        public Claim()
        {

        }
        public Claim(int ClaimID, ClaimType type, string description, double claimAmount, DateTime incidentDate, DateTime claimDate)
        {
            ID = ClaimID;
            Type = type;
            Description = description;
            ClaimAmount = claimAmount;
            DateOfIncident = incidentDate;
            DateOfClaim = claimDate;
        }
        public int ID { get; set; }
        public ClaimType Type { get; set; }
        public string Description { get; set; }
        public double ClaimAmount { get; set; }
        public DateTime DateOfIncident { get; set; }
        public DateTime DateOfClaim { get; set; }
        public bool IsValid
        {
            get
            {
                return (DateOfClaim - DateOfIncident).TotalDays <= 30;
            }
        }

    }
}
