using NUnit.Framework;
using System;
using ClassLibraryLab4;

namespace TestProjectLab4
{
    public class Tests
    {
        [TestFixture]
        public class DataAccessTests
        {

            [Test]
            public void GetByID_WhenNegativeIdPassed_ReturnsNull()
            {
                var dataAccess = new DataAccess();

                var retrievedEntity = dataAccess.GetByID(-1);

                Assert.IsNull(retrievedEntity);
            }

            [Test]
            public void GetByID_ReturnsCorrectEntity()
            {
                DataAccess dataAccess = new DataAccess();
                //предварительно убедился, что данный id существует в базе//

                int id = 700;

                var result = dataAccess.GetByID(id);

                Assert.NotNull(result);
                Assert.AreEqual(id, result.ID);
            }

            [Test]
            public void GetByID_WhenIdIsZero_ReturnsNull1111()
            {
               // подготовка базы, удалить из базы все  //
               
                var dataAccess = new DataAccess();
                dataAccess.DeleteAllData();

                // act
                var retrievedEntity = dataAccess.GetByID(888);

                // assert
                Assert.IsNull(retrievedEntity);
            }

            [Test]
            public void GetByName_WhenNameDoesNotExist_ReturnsEmptyList()
            {
                // Arrange
                var dataAccess = new DataAccess();

                // Act
                var retrievedEntities = dataAccess.GetByName("NonExistentName");

                // Assert
                Assert.IsNotNull(retrievedEntities);
                Assert.AreEqual(0, retrievedEntities.Count);
            }

            [Test]
            public void GetByName_WhenNameIsNull_ReturnsEmptyList()
            {
                // Arrange
                var dataAccess = new DataAccess();

                // Act
                var retrievedEntities = dataAccess.GetByName(null);

                // Assert
                Assert.IsNotNull(retrievedEntities);
                Assert.AreEqual(0, retrievedEntities.Count);
            }

            [Test]
            public void Add_WhenEntityIsNull_ThrowsException()
            {
                var dataAccess = new DataAccess();
                DBEntity entity = null;

                Assert.Throws<System.ArgumentNullException>(() => dataAccess.Add(entity));
            }

            [Test]
            public void Update_WhenEntityDoesNotExist_NothingIsUpdated()
            {
                var dataAccess = new DataAccess();

                dataAccess.Update(1, "NewTestMessage");

                var updatedEntity = dataAccess.GetByID(1);
                Assert.IsNull(updatedEntity);
            }

            [Test]
            public void Update_WhenEntityExistsAndMessageIsNull_NoExceptionIsThrown()
            {
                var dataAccess = new DataAccess();
                var entity = new DBEntity { ID = 1, Name = "TestName", Message = "TestMessage" };
                dataAccess.Add(entity);

                Assert.DoesNotThrow(() => dataAccess.Update(1, null));
            }

            [Test]
            public void Delete_WhenEntityExists_DeletesFromDatabase()
            {
                var dataAccess = new DataAccess();
                var entity = new DBEntity { ID = 1, Name = "TestName", Message = "TestMessage" };
                dataAccess.Add(entity);

                dataAccess.Delete(1);

                var deletedEntity = dataAccess.GetByID(1);
                Assert.IsNull(deletedEntity);
            }

            [Test]
            public void Delete_WhenEntityDoesNotExist_NothingIsDeleted()
            {
                var dataAccess = new DataAccess();

                dataAccess.Delete(1);

                var deletedEntity = dataAccess.GetByID(1);
                Assert.IsNull(deletedEntity);
            }


            [Test]
            public void Add_WhenEntityIsNullAndSaved_ThrowsException()
            {
                // Arrange
                var dataAccess = new DataAccess();
                DBEntity entity = null;

                // Act & Assert
                Assert.Throws<System.ArgumentNullException>(() => dataAccess.Add(entity));
            }


        }
    }
}