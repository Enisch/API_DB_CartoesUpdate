using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Domain.Interfaces
{
    public interface IConta
    {
        public Task<bool> PaymentFromDebit(double Value,int id);
        public Task<double> VerifyBalance(int id);

        public Task RaiseBalance(int id,double value);

    }
}
