using KaiZai.Service.Categories.API.Data.Entities;
using KaiZai.Service.Categories.API.Data.Enums;
using KaiZai.Service.Categories.API.Data.Repositories;

namespace KaiZai.Service.Categories.UnitTests.Data;

public sealed class CategoriesRepositoryTests : IClassFixture<DbFixture>
{
    private readonly DbFixture _dbFixture;
    private readonly CategoriesRepository _categoriesRepository;

    public CategoriesRepositoryTests(DbFixture dbFixture)
    {
        _dbFixture = dbFixture;
        _dbFixture.Seed();
        _categoriesRepository = new CategoriesRepository(mongoDatabase: _dbFixture.database);
    }
    
    [Fact]
    public async Task Get_categories_for_user_success()
    {
        //Arrange
        var testUserId  = new Guid(ConfigurationValues.TestUserId);
        //Act
        var testCategories = await _categoriesRepository.GetAllAsync(c => c.UserId.Equals(testUserId));
        //Assert
        Assert.NotEmpty(testCategories);
    }

    [Fact]
    public async Task Get_category_by_id_for_user_success()
    {
        //Arrange
        var testUserId  = new Guid(ConfigurationValues.TestUserId);
        var testCategoryId  = new Guid(ConfigurationValues.TestCategory);
        //Act
        var testCategory = await _categoriesRepository.GetOneAsync(x => x.Id.Equals(testCategoryId) && x.UserId.Equals(testUserId));
        //Assert
        Assert.NotNull(testCategory);
        Assert.Equal(testCategory.Id, testCategoryId);
        Assert.Equal(testCategory.UserId, testUserId);
    }

    [Fact]
    public async Task Create_category_for_user_success()
    {
        //Arrange
        var testUserId  = new Guid(ConfigurationValues.TestUserId);
        var testCategory = new Category
        {
            UserId = testUserId,
            Name = "Test_Groceries",
            CategoryType = CategoryType.Expense
        };
        //Act
        await _categoriesRepository.CreateAsync(entity: testCategory);
        //Assert
        Assert.NotEqual(testCategory.Id, Guid.Empty);
    }

    [Fact]
    public async Task Update_category_for_user_success()
    {
        //Arrange
        var testUserId  = new Guid(ConfigurationValues.TestUserId);
        var testCategory = new Category
        {
            UserId = testUserId,
            Name = "Test_Groceries",
            CategoryType = CategoryType.Expense
        };
        //Act
        await _categoriesRepository.CreateAsync(entity: testCategory);
        testCategory.Name = "Test_Groceries1";
        var updatedCategoryResult = await _categoriesRepository.UpdateAsync(entity: testCategory);
        //Assert
        Assert.NotNull(updatedCategoryResult);
        Assert.Equal(updatedCategoryResult.IsAcknowledged, true);
        Assert.Equal(updatedCategoryResult.ModifiedCount, 1);
    }
    
    [Fact]
    public async Task Update_fake_category_for_user_not_updated()
    {
        //Arrange
        var testUserId  = new Guid(ConfigurationValues.TestUserId);
        var testCategory = new Category
        {
            UserId = testUserId,
            Name = "Test_Groceries",
            CategoryType = CategoryType.Expense
        };
        //Act
        var updatedCategoryResult = await _categoriesRepository.UpdateAsync(entity: testCategory);
        //Assert
        Assert.NotNull(updatedCategoryResult);
        Assert.Equal(updatedCategoryResult.IsAcknowledged, true);
        Assert.Equal(updatedCategoryResult.ModifiedCount, 0);
    }

    [Fact]
    public async Task Delete_category_by_id_success()
    {
        //Arrange
        var testUserId  = new Guid(ConfigurationValues.TestUserId);
        var testCategory = new Category
        {
            UserId = testUserId,
            Name = "Test_Groceries",
            CategoryType = CategoryType.Expense
        };
        //Act
        await _categoriesRepository.CreateAsync(entity: testCategory);
        var removedCategoryResult = await _categoriesRepository.RemoveAsync(testCategory.Id);
        //Assert
        Assert.NotNull(removedCategoryResult);
        Assert.Equal(removedCategoryResult.IsAcknowledged, true);
        Assert.Equal(removedCategoryResult.DeletedCount, 1);
    }

    [Fact]
    public async Task Delete_fake_category_by_id_not_deleted()
    {
        //Arrange
        var fakeCategoryId = new Guid(ConfigurationValues.TestFakeCategoryIdForDelete);
        //Act
        var removedCategoryResult = await _categoriesRepository.RemoveAsync(fakeCategoryId);
        //Assert
        Assert.NotNull(removedCategoryResult);
        Assert.Equal(removedCategoryResult.IsAcknowledged, true);
        Assert.Equal(removedCategoryResult.DeletedCount, 0);
    }
}
