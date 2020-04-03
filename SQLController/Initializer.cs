using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLConnection;

namespace SQLController {
    public static class Initializer {
        #region Global Variables

        static SQL _sql = new SQL();

        #endregion

        #region Initalize Database

        public static void InitalizeDatabase() {
            // Call method CreateDatabase
            _sql.CreateDatabase();
            // Call method CreateDatabaseTable
            CreateDatabaseTables();
            // Call method SeedDatabaseTables
            SeedDatabaseTables();
        }

        /// <summary>
        /// Creates the database tables
        /// </summary>
        private static void CreateDatabaseTables() {
            CreateMovieTable();
            CreateCustomerTable();
            CreateRentalTable();
            CreateRentalItemTable();
        }

        /// <summary>
        /// Creates movie table
        /// </summary>
        private static void CreateMovieTable() {
            // Movie Schema
            string schema =
                "MovieID int IDENTITY(1,1) PRIMARY KEY, " +
                "MovieName VARCHAR(60)";
            // Call CreateDatabaseTable
            _sql.CreateDatabaseTable("Movie", schema);
        }

        /// <summary>
        /// Creates customer table
        /// </summary>
        private static void CreateCustomerTable() {
            // Customer Schema
            string schema =
                "CustomerID int IDENTITY(1,1) PRIMARY KEY, " +
                "CustomerName VARCHAR(120), " +
                "CustomerPhone VARCHAR(10)";
            // Call CreateDatabaseTable
            _sql.CreateDatabaseTable("Customer", schema);
        }

        /// <summary>
        /// Creates rental table
        /// </summary>
        private static void CreateRentalTable() {
            // Rental Schema
            string schema =
                "RentalID int IDENTITY(1,1) PRIMARY KEY, " +
                "CustomerID int NOT NULL, " +
                "DateRented DATE NOT NULL, " +
                "DateReturned DATE NULL,";
            // Call CreateDatabaseTable
            _sql.CreateDatabaseTable("Rental", schema);
        }

        /// <summary>
        /// Creates rental items table
        /// </summary>
        private static void CreateRentalItemTable() {
            // Rental Item Schema
            string schema =
                "RentalItemID int IDENTITY(1,1) PRIMARY KEY, " +
                "RentalID int NOT NULL, " +
                "MovieID int NOT NULL";
            // Call CreateDatabaseTable
            _sql.CreateDatabaseTable("RentalItem", schema);
        }

        #endregion

        #region Create Database Schema

        public static void CreateDatabaseSchema() {

        }

        #endregion

        #region Seed Database Tables

        private static void SeedDatabaseTables() {
            SeedMovieTable();
            SeedCustomerTable();
            SeedRentalTable();
            SeedRentalItemTable();
        }

        private static void SeedMovieTable() {
            List<string> columnValues = new List<string> {
                // MovieID, MovieName
                "1, 'The Avengers'",
                "2, 'Star Wars'",
                "3, 'The Matrix'"
            };

            string columnNames = "MovieID, MovieName";

            // Loop through the List
            foreach (string value in columnValues) {
                _sql.InsertRecord("Movie", columnNames, value);
            }
        }

        private static void SeedCustomerTable() {
            List<string> columnValues = new List<string> {
                // CustomerID, CustomerName, CustomerPhone
                "1, 'The Man', '0417703977'",
                "2, 'The Woman', '0478367472'",
                "3, 'The Child', '0435454472'"
            };

            string columnNames = "CustomerID, CustomerName, CustomerPhone";

            // Loop through the List
            foreach (string value in columnValues) {
                _sql.InsertRecord("Customer", columnNames, value);
            }
        }

        private static void SeedRentalTable() {
            List<string> columnValues = new List<string> {
                // RentalID, CustomerID, DateRented, DateReturned, ReturnedCheck
                $"1, 2, '01-17-2017', null",
                $"2, 3, '06-30-2017', null",
                $"3, 1, '06-06-2017', '06-07-2017'"
            };

            string columnNames = "RentalID, CustomerID, DateRented, DateReturned";

            // Loop through the List
            foreach (string value in columnValues) {
                _sql.InsertRecord("Rental", columnNames, value);
            }
        }

        private static void SeedRentalItemTable() {
            List<string> columnValues = new List<string> {
                // RentalItemID, RentalID, MovieID
                "1, 1, 2",
                "2, 1, 1",
                "3, 3, 3"
            };

            string columnNames = "RentalItemID, RentalID, MovieID";

            // Loop through the List
            foreach (string value in columnValues) {
                _sql.InsertRecord("RentalItem", columnNames, value);
            }
        }

        #endregion
    }
}
