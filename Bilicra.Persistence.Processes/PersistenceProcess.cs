using Bilicra.API.Domain.Models;
using Bilicra.Infrastructure.Domain.Interfaces;
using Bilicra.Persistence.Domain.Interfaces;
using Bilicra.Persistence.Domain.Models;
using Bilicra.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Bilicra.Persistence.Processes
{
    public class PersistenceProcess : IPersistenceProcess
    {
        private readonly ILoggingProcess logging;
        public PersistenceProcess(ILoggingProcess _loggingProcess)
        {
            logging = _loggingProcess;
        }

        private SqlConnection connection;
        public void EstablishConnection(string server, string database, string username, string password)
        {
            try
            {
                if (PersistenceService.ValidateConnectionParameter(server))
                    throw new Exception("Server is invalid!");

                if (PersistenceService.ValidateConnectionParameter(database))
                    throw new Exception("Database is invalid!");

                if (PersistenceService.ValidateConnectionParameter(username))
                    throw new Exception("Username is invalid!");

                if (PersistenceService.ValidateConnectionParameter(password))
                    throw new Exception("Password is invalid!");

                connection = PersistenceService.Connect(new ConnectionModel
                {
                    ServerName = server,
                    DatabaseName = database,
                    UserName = username,
                    Password = password
                });
            }
            catch (Exception ex)
            {
                logging.WriteFatalMessage(ex.Message, ex.InnerException, MethodBase.GetCurrentMethod(), "");
                throw;
            }
        }

        private SqlCommand command;
        public void CreateCommand(string commandText, params object[] sqlParameters)
        {
            try
            {
                command = PersistenceService.CreateSqlCommand(connection, commandText, sqlParameters);
            }
            catch (Exception ex)
            {
                logging.WriteFatalMessage(ex.Message, ex.InnerException, MethodBase.GetCurrentMethod(), "");
                throw;
            }
        }

        public List<ProductCatolog> GetProductCatalogs()
        {
            EstablishConnection("localhost", "dbBilicra", "sa", "sapwd");

            CreateCommand("select * from ProductCatalogs");

            return PersistenceService.GetList<ProductCatolog>(command);
        }

        public ProductCatolog GetProductCatalog(int Id)
        {
            EstablishConnection("localhost", "dbBilicra", "sa", "sapwd");

            CreateCommand("select * from ProductCatalogs where Id=@p1", Id);

            return PersistenceService.Get<ProductCatolog>(command);
        }

        public ProductCatolog GetProductCatalog(string Code)
        {
            EstablishConnection("localhost", "dbBilicra", "sa", "sapwd");

            CreateCommand("select * from ProductCatalogs where Code=@p1", Code);

            return PersistenceService.Get<ProductCatolog>(command);
        }

        public int SaveProductCatalog(ProductCatolog productCatolog)
        {
            EstablishConnection("localhost", "dbBilicra", "sa", "sapwd");

            if (productCatolog.Id > 0)
            {
                CreateCommand("update ProductCatalogs set Code=@p1,Name=@p2,Photo=@p3,Price=@p4,LastUpdated=@p5 where Id=@p6",
                    productCatolog.Code, productCatolog.Name, productCatolog.Photo, productCatolog.Price, productCatolog.LastUpdated, productCatolog.Id);
            }
            else
            {
                CreateCommand("insert into ProductCatalogs (Code,Name,Photo,Price,LastUpdated) values (@p1,@p2,@p3,@p4,@p5)",
                    productCatolog.Code, productCatolog.Name, productCatolog.Photo ?? "", productCatolog.Price, productCatolog.LastUpdated);
            }

            return PersistenceService.SqlExecute(command);
        }

        public int DeleteProductCatalog(int Id)
        {
            EstablishConnection("localhost", "dbBilicra", "sa", "sapwd");

            CreateCommand("delete from ProductCatalogs where Id=@p1", Id);

            return PersistenceService.SqlExecute(command);
        }
    }
}
