using Ejemplo01.Data;
using Ejemplo01.Models;

const string X_ORIGIN_ACCOUNT = "10";
const string X_DESTINATION_ACCOUNT = "20";

using(var dbContext = new BankContext())
{
    //Iniciar una transaccion
    using(var transaction = dbContext.Database.BeginTransaction())
    {
        try
        {
            //Transferir fondos de una cuenta a otra
            decimal quantityToTransfer = 200M;

            //Leer saldo de la cuenta de origen
            var originAccount = dbContext.AccountTransactions
                .Where(a => a.AccountNumber == X_ORIGIN_ACCOUNT)
                .GroupBy(a => a.AccountNumber)
                .Select(g => new { Balance = g.Sum(a => a.Credit) - g.Sum(a => a.Debit) })
                .FirstOrDefault();

            if (originAccount == null || originAccount.Balance < quantityToTransfer)
            {
                throw new Exception($"Fondos insuficientes en la cuenta {X_ORIGIN_ACCOUNT} TRANSACCION ABORTADA");
            }

            //Retirar fondos de la cuenta de origen
            var debitTransfer = new AccountTransaction
            {
                AccountNumber = X_DESTINATION_ACCOUNT,
                Debit = quantityToTransfer,
                Credit = 0M
            };

            dbContext.AccountTransactions.Add(debitTransfer);
            //Depositar fondo a la cuenta de destino
            var creditTransaction = new AccountTransaction
            {
                AccountNumber = X_DESTINATION_ACCOUNT,
                Debit = 0M,
                Credit = quantityToTransfer
            };
            dbContext.AccountTransactions.Add(creditTransaction);

            //Guardar los cambios en la base de datos
            dbContext.SaveChanges();

            //Provocar una excepcion de conexion
            throw new Exception("Simulacion de error de conexion a la base de datos");

            //Confirmar la transaccion
            transaction.Commit();
            Console.WriteLine("Fondo transferidos con exito");
        }
        catch (Exception ex)
        {

           //Revertir la transaccion en caso de error
           transaction.Rollback();
            Console.WriteLine($"Ha ocurrido un error," +
               $"los fondos no se han transferido : {ex.Message}" );
        }

    }
}
