using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Domain.Interfaces
{
    public  interface ICreditCard
    {
        //Only these methods for now;
        public Task<bool> PaymentUsingCredit(double Value,Usuario usuario);
        public Task<DateOnly> ConsultInvoice(Usuario usuario);
    }
}
