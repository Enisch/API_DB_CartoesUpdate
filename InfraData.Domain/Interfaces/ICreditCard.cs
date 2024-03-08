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
        public Task<bool> PaymentUsingCredit(double Value,int Id);
        public Task<DateOnly> ConsultInvoice(int Id);
        public Task<double> RaiseCreditLimit(int id);
        public Task<double> GetCreditLimit(int id);
    }
}
