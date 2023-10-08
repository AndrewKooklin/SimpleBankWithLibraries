using SimpleBank.Data;
using SimpleBank.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Help
{
    public class GetDataFromDB
    {
        private ObservableCollection<UserOperation> userOperations;
        string stringQuery = "";
        SQLiteCommand SqliteCmd = new SQLiteCommand();
        private int personId;
        private int? totalSalaryAccount;
        private int? totalDepositAccount;

        public ObservableCollection<Person> GEtAllPersonsFromDB()
        {
            ObservableCollection<Person> _persons = new ObservableCollection<Person>();

            try
            {
                SQLiteConnection connection = new SQLiteConnection(App.connectionString);
                connection.Open();

                stringQuery = "SELECT PersonId, LastName, FirstName, FathersName, " +
                                "Phone, PassportNumber, " +
                                "TotalSalaryAccount, TotalDepositAccount FROM Persons";

                SqliteCmd.Connection = connection;
                SqliteCmd.CommandText = stringQuery;
                SQLiteDataReader dataReader = SqliteCmd.ExecuteReader();

                if (!dataReader.HasRows)
                {
                    throw new ReadDataFromBaseException("Нет данных в таблице");
                }

                while (dataReader.Read())
                {
                    object personIdObj = dataReader["PersonId"];
                    object lastNameObj = dataReader["LastName"];
                    object firstNameObj = dataReader["FirstName"];
                    object fathersNameObj = dataReader["FathersName"];
                    object phoneObj = dataReader["Phone"];
                    object passportNumberObj = dataReader["PassportNumber"];
                    object totalSalaryAccountObj = dataReader["TotalSalaryAccount"];
                    object totalDepositAccountObj = dataReader["TotalDepositAccount"];
                    if (String.IsNullOrWhiteSpace(personIdObj.ToString()))
                    {
                        personId = 0;
                    }
                    else personId = System.Int32.Parse(personIdObj.ToString());
                    if (String.IsNullOrWhiteSpace(totalSalaryAccountObj.ToString()))
                    {
                        totalSalaryAccount = null;
                    }
                    else totalSalaryAccount = System.Int32.Parse(totalSalaryAccountObj.ToString());
                    if (String.IsNullOrWhiteSpace(totalDepositAccountObj.ToString()))
                    {
                        totalDepositAccount = null;
                    }
                    else totalDepositAccount = System.Int32.Parse(totalDepositAccountObj.ToString());

                    Person person = new Person(personId, lastNameObj.ToString(), firstNameObj.ToString(),
                                               fathersNameObj.ToString(), phoneObj.ToString(),
                                               passportNumberObj.ToString(), totalSalaryAccount,
                                               totalDepositAccount);

                    _persons.Add(person);
                }

                dataReader.Close();
                connection.Close();

                App.Persons = _persons;

                return _persons;
            }
            catch (ReadDataFromBaseException e)
            {
                e.ShowMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return _persons;
        }

        public ObservableCollection<UserOperation> GetUserOperationsFromDB()
        {
            userOperations = new ObservableCollection<UserOperation>();

            int? totalSum;

            try
            {
                SQLiteConnection connection = new SQLiteConnection(App.connectionString);
                connection.Open();

                stringQuery = "SELECT Role, DataOperation, Operation, TotalSum FROM UserOperations";

                SqliteCmd.Connection = connection;
                SqliteCmd.CommandText = stringQuery;
                SQLiteDataReader dataReader = SqliteCmd.ExecuteReader();

                if (!dataReader.HasRows)
                {
                    throw new ReadDataFromBaseException("Нет данных в таблице");
                }

                while (dataReader.Read())
                {
                    object role = dataReader["Role"];
                    object dataOperation = dataReader["DataOperation"];
                    object operation = dataReader["Operation"];
                    object totalSumRead = dataReader["TotalSum"];
                    if(totalSumRead.ToString().Equals("0") || totalSumRead == null)
                    {
                        totalSum = null;
                    }
                    else totalSum = System.Int32.Parse(totalSumRead.ToString());

                    UserOperation userOperation = new UserOperation(role.ToString(), dataOperation.ToString(),
                                                                    operation.ToString(), totalSum);

                    userOperations.Add(userOperation);
                }

                dataReader.Close();
                connection.Close();

                return userOperations;
            }
            catch (ReadDataFromBaseException e)
            {
                e.ShowMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return userOperations;
        }
    }
}
