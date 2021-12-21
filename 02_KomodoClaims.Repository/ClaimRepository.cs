using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_KomodoClaims.Repository
{
    public class ClaimRepository
    {
        private readonly Queue<Claim> _claimsQueue;
        //private int _count;

        public ClaimRepository()
        {
            _claimsQueue = new Queue<Claim>();
        }

        //Create
        public bool AddToQueue(Claim newClaim)
        {
            if(newClaim == null)
            {
                return false;
            }
            int startingCount = _claimsQueue.Count;
            //newClaim.ID = _count++;
            _claimsQueue.Enqueue(newClaim);

            return _claimsQueue.Count>startingCount ? true: false;
        }
        //Read
        public Claim[] ListClaims()
        {
            return _claimsQueue.ToArray();
        }
        public Claim DisplayNextClaim()
        {
            return _claimsQueue.Peek();
        }
        //Delete
        public Claim ProcessClaim()
        {
            return _claimsQueue.Dequeue();
        }
    }
}
