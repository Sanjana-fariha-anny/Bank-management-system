using DataAccessLayer_Banking_Management_System.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer_Banking_Management_System.Operation
{
    public class OProduct
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-K7F1EBS\MSSQLSERVER01;Initial Catalog=Banking Management System Info;Integrated Security=True");
        public int insertAccount(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("if exists(select* from Customer where customerNID='" + eProduct.AccountNo + "') Begin insert into SignUp(firstname,lastname,gender,language,accountType,dateOfBirth,AccountNo,mobileNumber,password) values('" + eProduct.firstName + "','" + eProduct.lastName + "','" + eProduct.gender + "','" + eProduct.language + "','" + eProduct.accountType + "','" + eProduct.dateOfBirth + "','" + eProduct.AccountNo + "','" + eProduct.mobileNumber + "','" + eProduct.password + "') end", connection);    
            int number = command.ExecuteNonQuery();
            connection.Close();
            return number;
        }
        public int insertNewEmployee(EProduct eProduct)
        {
            int number = -1;
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Employee(accountName,employeeID,salary) values('" + eProduct.accountName + "','" + eProduct.employeeID + "','" + eProduct.salary + "')", connection);

            try
            {
                number = command.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                //Violation of PRIMARY KEY.MessageBox.Show("Please enter the all inputs ");
            }
            connection.Close();
            return number;
        }
        public int RemoveExistingEmployee(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Employee WHERE employeeID='" + eProduct.employeeID + "';", connection);
            int number = command.ExecuteNonQuery();
            connection.Close();
            return number;
        }

        public int logIn(EProduct eProduct)
        {
            SqlCommand command = new SqlCommand("select * from SignUp where AccountNo='" + eProduct.userID + "' and password ='" + eProduct.password + "'", connection);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            connection.Open();
            command.ExecuteNonQuery();
            int number = dt.Rows.Count;
            connection.Close();
            return number;
        }
        public int insertNewCustomer(EProduct eProduct)
        {
            int number = -1;
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Customer(customerName,customerNID) values('" + eProduct.customerName + "','" + eProduct.customerNID + "')", connection);
            try
            {
                number = command.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                //Violation of PRIMARY KEY. MessageBox.Show("Please enter the all inputs ");
            }
            connection.Close();
            return number;
        }
        public int RemoveExistingCustomer(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE customerNID='" + eProduct.customerNID + "';", connection);
            int number = command.ExecuteNonQuery();
            connection.Close();
            return number;
        }
        public int insertNewSavingsAccount(EProduct eProduct)
        {
            int number = -1;
            connection.Open();
            SqlCommand command = new SqlCommand("if  exists(select* from Customer where customerName='" + eProduct.accountName + "') Begin insert into SavingsAccount(accountName, balance, interestRate) values('" + eProduct.accountName + "','" + eProduct.balance + "','" + eProduct.interestRate + "') end", connection);
            try
            {
                number = command.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                //Violation of PRIMARY KEY.MessageBox.Show("Please enter the all inputs ");
            }
            connection.Close();
            return number;
        }
        public int insertNewFixedAccount(EProduct eProduct)
        {
            int number = -1;
            connection.Open();
            SqlCommand command = new SqlCommand("if  exists(select* from Customer where customerName='" + eProduct.accountName + "') Begin insert into FixedAccount(accountName,balance,tenureYear) values('" + eProduct.accountName + "','" + eProduct.balance + "','" + eProduct.tenureYear + "') end", connection);

            try
            {
                number = command.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                //Violation of PRIMARY KEY.MessageBox.Show("Please enter the all inputs ");
            }
            connection.Close();
            return number;
        }
        public int RemoveSavingsAccount(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("if  exists(select* from Customer where customerNID='" + eProduct.accountNumber + "') Begin DELETE FROM SavingsAccount where accountName=(select customerName from Customer where customerNID='" + eProduct.accountNumber + "') end", connection);
            int number = command.ExecuteNonQuery();
            connection.Close();
            return number;
        }
        public int RemoveFixedAccount(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("if  exists(select* from Customer where customerNID='" + eProduct.accountNumber + "') Begin DELETE FROM FixedAccount where accountName=(select customerName from Customer where customerNID='" + eProduct.accountNumber + "') end", connection);
            int number = command.ExecuteNonQuery();
            connection.Close();
            return number;
        }
        public int deposit(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("update SavingsAccount set balance+='" + eProduct.amount + "'   where accountName =(select customerName from Customer where customerNID='" + eProduct.AccountNo + "')", connection);
            int number = command.ExecuteNonQuery();
            connection.Close();
            return number;
        }
        public int withdraw(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("if('" + eProduct.balance + "'>='" + eProduct.amount + "')  Begin update SavingsAccount set balance-='" + eProduct.amount + "'   where accountName =(select customerName from Customer where customerNID='" + eProduct.AccountNo + "') end", connection);
            int number = command.ExecuteNonQuery();
            connection.Close();
            return number;
        }
        public int transfer(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("if exists(select * from  SavingsAccount where accountName =(select customerName from Customer where customerNID='" + eProduct.transferAccountNumber + "') and '" + eProduct.balance + "'>='" + eProduct.amount + "') Begin update SavingsAccount set balance-='" + eProduct.amount + "'  where accountName =(select customerName from Customer where customerNID='" + eProduct.AccountNo + "') update SavingsAccount set balance+='" + eProduct.amount + "'  where accountName=(select customerName from Customer where customerNID='" + eProduct.transferAccountNumber + "') end", connection);
            int number = command.ExecuteNonQuery();
            connection.Close();
            return number;
        }
        public int checkAdmin(EProduct eProduct)
        {
            SqlCommand command = new SqlCommand("if exists(select * from  SignUp where 'Admin'=(select accountType from SignUp where AccountNo='" + eProduct.userID + "')) Begin select * from SignUp where AccountNo='" + eProduct.userID + "' and password ='" + eProduct.password + "' end ", connection);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            connection.Open();
            command.ExecuteNonQuery();
            int number = dt.Rows.Count;
            connection.Close();
            return number;
        }
        public int checkLogInDetails(EProduct eProduct)
        {
            int userID;
            connection.Open();
            using (SqlCommand command = new SqlCommand("select userID from logInDetails", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Data is accessible through the DataReader object here.
                    reader.Read();
                    userID = reader.GetInt32(0);
                }
            }
            connection.Close();
            return userID;
        }
        public void storeLogInDetails(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into logInDetails (userID) values ('" + eProduct.userID + "')", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void deleteLogInDetails(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM logInDetails", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public int checkBalance(EProduct eProduct)
        {
            int balance;
            connection.Open();
            using (SqlCommand command = new SqlCommand("select balance from SavingsAccount where accountName =(select customerName from Customer where customerNID='" + eProduct.AccountNo + "')", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Data is accessible through the DataReader object here.
                    reader.Read();
                    balance = reader.GetInt32(0);
                }
            }
            connection.Close();
            return balance;
        }
        public void storeDepositTransactions(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Transactions(transactions,amount,[From (AccountNo) :],balance,dateTime) values(' Deposited ','" + eProduct.amount + "','" + eProduct.AccountNo + "','" + eProduct.balance + "','" + eProduct.dateTime + "') ", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void storeWithdrawTransactions(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Transactions(transactions,amount,[From (AccountNo) :],balance,dateTime) values(' Withdrawn ','" + eProduct.amount + "','" + eProduct.AccountNo + "','" + eProduct.balance + "','" + eProduct.dateTime + "') ", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void storeTransferTransactions(EProduct eProduct)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Transactions(transactions,amount,[From (AccountNo) :],balance,dateTime,[To (AccountNo) :]) values(' transferred ','" + eProduct.amount + "','" + eProduct.AccountNo + "','" + eProduct.balance + "','" + eProduct.dateTime + "','" + eProduct.transferAccountNumber + "') ", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}
