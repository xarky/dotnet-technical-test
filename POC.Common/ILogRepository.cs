using System;

namespace POC.Common
{
    public interface ILogRepository
    {
        void LogDeposit(Int32 customerId, Decimal funds);
        void LogWithdrawal(Int32 customerId, Decimal funds);
        void LogTransfer(Int32 sourceCustomerId, Int32 destinationCustomerId, Decimal funds);
    }
}