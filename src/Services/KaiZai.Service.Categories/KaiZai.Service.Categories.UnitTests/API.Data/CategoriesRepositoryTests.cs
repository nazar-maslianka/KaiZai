using KaiZai.Service.Categories.API.Data.Repositories;

namespace KaiZai.Service.Categories.UnitTests.API.Data;

public sealed class CategoryRepositoryTests : IClassFixture<DbFixture>
{
    private readonly DbFixture _dbFixture;
    private readonly CategoryRepository _categoryRepository;

    public CategoryRepositoryTests(DbFixture dbFixture)
    {
        _dbFixture = dbFixture;
        _dbFixture.Seed();
        _categoryRepository = new CategoryRepository(mongoDatabase: _dbFixture.database);
    }
    
    [Fact]
    public async Task Get_categories_for_profile_success()
    {
        //Arrange
        var testProfileId  = new Guid(ConfigurationValues.TestProfileId);
        //Act
        var testCategories = await _categoryRepository.GetAllAsync(c => c.ProfileId.Equals(testProfileId));
        //Assert
        Assert.NotEmpty(testCategories);
    }

    [Fact]
    public async Task Get_category_by_id_for_profile_success()
    {
        //Arrange
        var testProfileId  = new Guid(ConfigurationValues.TestProfileId);
        var testCategoryId  = new Guid(ConfigurationValues.TestCategory);
        //Act
        var testCategory = await _categoryRepository.GetOneAsync(x => x.Id.Equals(testCategoryId) && x.ProfileId.Equals(testProfileId));
        //Assert
        Assert.NotNull(testCategory);
        Assert.Equal(testCategory.Id, testCategoryId);
        Assert.Equal(testCategory.ProfileId, testProfileId);
    }

    [Fact]
    public async Task Create_category_for_profile_success()
    {
        //Arrange
        var testProfileId  = new Guid(ConfigurationValues.TestProfileId);
        var testCategory = new Category
        {
            ProfileId = testProfileId,
            Name = "Test_Groceries",
            CategoryType = CategoryType.Expense
        };
        //Act
        await _categoryRepository.CreateAsync(entity: testCategory);
        //Assert
        Assert.NotEqual(testCategory.Id, Guid.Empty);
    }

    [Fact]
    public async Task Update_category_for_profile_success()
    {
        //Arrange
        var testProfileId  = new Guid(ConfigurationValues.TestProfileId);
        var testCategory = new Category
        {
            ProfileId = testProfileId,
            Name = "Test_Groceries",
            CategoryType = CategoryType.Expense
        };
        //Act
        await _categoryRepository.CreateAsync(entity: testCategory);
        testCategory.Name = "Test_Groceries1";
        var updatedCategoryResult = await _categoryRepository.UpdateAsync(entity: testCategory);
        //Assert
        Assert.NotNull(updatedCategoryResult);
        Assert.Equal(updatedCategoryResult.IsAcknowledged, true);
        Assert.Equal(updatedCategoryResult.ModifiedCount, 1);
    }
    
    [Fact]
    public async Task Update_fake_category_for_profile_not_updated()
    {
        //Arrange
        var testProfileId  = new Guid(ConfigurationValues.TestProfileId);
        var testCategory = new Category
        {
            ProfileId = testProfileId,
            Name = "Test_Groceries",
            CategoryType = CategoryType.Expense
        };
        //Act
        var updatedCategoryResult = await _categoryRepository.UpdateAsync(entity: testCategory);
        //Assert
        Assert.NotNull(updatedCategoryResult);
        Assert.Equal(updatedCategoryResult.IsAcknowledged, true);
        Assert.Equal(updatedCategoryResult.ModifiedCount, 0);
    }

    [Fact]
    public async Task Delete_category_by_id_success()
    {
        //Arrange
        var testProfileId  = new Guid(ConfigurationValues.TestProfileId);
        var testCategory = new Category
        {
            ProfileId = testProfileId,
            Name = "Test_Groceries",
            CategoryType = CategoryType.Expense
        };
        //Act
        await _categoryRepository.CreateAsync(entity: testCategory);
        var removedCategoryResult = await _categoryRepository.RemoveAsync(testCategory.Id);
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
        var removedCategoryResult = await _categoryRepository.RemoveAsync(fakeCategoryId);
        //Assert
        Assert.NotNull(removedCategoryResult);
        Assert.Equal(removedCategoryResult.IsAcknowledged, true);
        Assert.Equal(removedCategoryResult.DeletedCount, 0);
    }
}
