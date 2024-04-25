


using ITM.DAL.MSSQL;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using ITM.Test.Common;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Test.PPT.DAL.MSSQL
{
    public class TestUserDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserDal dal = new UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void User_GetAll_Success()
        {
            var dal = PrepareUserDal("DALInitParams");

            IList<User> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("User\\000.GetDetails.Success")]
        public void User_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.Login, Is.EqualTo("Login a703e85d20db418aa43e017d18d1559e"));
            Assert.That(entity.PwdHash, Is.EqualTo("PwdHash a703e85d20db418aa43e017d18d1559e"));
            Assert.That(entity.Salt, Is.EqualTo("Salt a703e85d20db418aa43e017d18d1559e"));
            Assert.That(entity.FirstName, Is.EqualTo("FirstName a703e85d20db418aa43e017d18d1559e"));
            Assert.That(entity.MiddleName, Is.EqualTo("MiddleName a703e85d20db418aa43e017d18d1559e"));
            Assert.That(entity.LastName, Is.EqualTo("LastName a703e85d20db418aa43e017d18d1559e"));
            Assert.That(entity.FriendlyName, Is.EqualTo("FriendlyName a703e85d20db418aa43e017d18d1559e"));
            Assert.That(entity.CreatedDate, Is.EqualTo(DateTime.Parse("10/24/2021 3:20:17 AM")));
            Assert.That(entity.ModifiedDate, Is.EqualTo(DateTime.Parse("10/24/2021 3:20:17 AM")));
            Assert.That(entity.ModifiedByID, Is.EqualTo(100001));
        }

        [Test]
        public void User_GetDetails_InvalidId()
        {
            var paramID = Int64.MaxValue - 1;
            var dal = PrepareUserDal("DALInitParams");

            User entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("User\\010.Delete.Success")]
        public void User_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void User_Delete_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");
            var paramID = Int64.MaxValue - 1;

            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("User\\020.Insert.Success")]
        public void User_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
            entity.Login = "Login 694c81b1bb4648a081abb084b5a4a1ed";
            entity.PwdHash = "PwdHash 694c81b1bb4648a081abb084b5a4a1ed";
            entity.Salt = "Salt 694c81b1bb4648a081abb084b5a4a1ed";
            entity.FirstName = "FirstName 694c81b1bb4648a081abb084b5a4a1ed";
            entity.MiddleName = "MiddleName 694c81b1bb4648a081abb084b5a4a1ed";
            entity.LastName = "LastName 694c81b1bb4648a081abb084b5a4a1ed";
            entity.FriendlyName = "FriendlyName 694c81b1bb4648a081abb084b5a4a1ed";
            entity.CreatedDate = DateTime.Parse("11/30/2024 11:20:17 PM");
            entity.ModifiedDate = DateTime.Parse("11/30/2024 11:20:17 PM");
            entity.ModifiedByID = 100004;

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.Login, Is.EqualTo("Login 694c81b1bb4648a081abb084b5a4a1ed"));
            Assert.That(entity.PwdHash, Is.EqualTo("PwdHash 694c81b1bb4648a081abb084b5a4a1ed"));
            Assert.That(entity.Salt, Is.EqualTo("Salt 694c81b1bb4648a081abb084b5a4a1ed"));
            Assert.That(entity.FirstName, Is.EqualTo("FirstName 694c81b1bb4648a081abb084b5a4a1ed"));
            Assert.That(entity.MiddleName, Is.EqualTo("MiddleName 694c81b1bb4648a081abb084b5a4a1ed"));
            Assert.That(entity.LastName, Is.EqualTo("LastName 694c81b1bb4648a081abb084b5a4a1ed"));
            Assert.That(entity.FriendlyName, Is.EqualTo("FriendlyName 694c81b1bb4648a081abb084b5a4a1ed"));
            Assert.That(entity.CreatedDate, Is.EqualTo(DateTime.Parse("11/30/2024 11:20:17 PM")));
            Assert.That(entity.ModifiedDate, Is.EqualTo(DateTime.Parse("11/30/2024 11:20:17 PM")));
            Assert.That(entity.ModifiedByID, Is.EqualTo(100004));

        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

            entity.Login = "Login e8a38a804bb54b4d8108341fb6572d91";
            entity.PwdHash = "PwdHash e8a38a804bb54b4d8108341fb6572d91";
            entity.Salt = "Salt e8a38a804bb54b4d8108341fb6572d91";
            entity.FirstName = "FirstName e8a38a804bb54b4d8108341fb6572d91";
            entity.MiddleName = "MiddleName e8a38a804bb54b4d8108341fb6572d91";
            entity.LastName = "LastName e8a38a804bb54b4d8108341fb6572d91";
            entity.FriendlyName = "FriendlyName e8a38a804bb54b4d8108341fb6572d91";
            entity.CreatedDate = DateTime.Parse("3/1/2025 6:54:17 PM");
            entity.ModifiedDate = DateTime.Parse("3/1/2025 6:54:17 PM");
            entity.ModifiedByID = 100009;

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.Login, Is.EqualTo("Login e8a38a804bb54b4d8108341fb6572d91"));
            Assert.That(entity.PwdHash, Is.EqualTo("PwdHash e8a38a804bb54b4d8108341fb6572d91"));
            Assert.That(entity.Salt, Is.EqualTo("Salt e8a38a804bb54b4d8108341fb6572d91"));
            Assert.That(entity.FirstName, Is.EqualTo("FirstName e8a38a804bb54b4d8108341fb6572d91"));
            Assert.That(entity.MiddleName, Is.EqualTo("MiddleName e8a38a804bb54b4d8108341fb6572d91"));
            Assert.That(entity.LastName, Is.EqualTo("LastName e8a38a804bb54b4d8108341fb6572d91"));
            Assert.That(entity.FriendlyName, Is.EqualTo("FriendlyName e8a38a804bb54b4d8108341fb6572d91"));
            Assert.That(entity.CreatedDate, Is.EqualTo(DateTime.Parse("3/1/2025 6:54:17 PM")));
            Assert.That(entity.ModifiedDate, Is.EqualTo(DateTime.Parse("3/1/2025 6:54:17 PM")));
            Assert.That(entity.ModifiedByID, Is.EqualTo(100009));

        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
            entity.Login = "Login e8a38a804bb54b4d8108341fb6572d91";
            entity.PwdHash = "PwdHash e8a38a804bb54b4d8108341fb6572d91";
            entity.Salt = "Salt e8a38a804bb54b4d8108341fb6572d91";
            entity.FirstName = "FirstName e8a38a804bb54b4d8108341fb6572d91";
            entity.MiddleName = "MiddleName e8a38a804bb54b4d8108341fb6572d91";
            entity.LastName = "LastName e8a38a804bb54b4d8108341fb6572d91";
            entity.FriendlyName = "FriendlyName e8a38a804bb54b4d8108341fb6572d91";
            entity.CreatedDate = DateTime.Parse("3/1/2025 6:54:17 PM");
            entity.ModifiedDate = DateTime.Parse("3/1/2025 6:54:17 PM");
            entity.ModifiedByID = 100009;

            try
            {
                entity = dal.Update(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch (Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }


        protected IUserDal PrepareUserDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserDal dal = new UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
