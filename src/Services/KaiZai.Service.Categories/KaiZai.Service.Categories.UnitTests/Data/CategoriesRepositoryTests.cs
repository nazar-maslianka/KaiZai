using KaiZai.Service.Categories.API.Data.Repositories;

namespace KaiZai.Service.Categories.UnitTests.Data;

public sealed class CategoriesRepositoryTests : IClassFixture<DbFixture>
{
    private readonly DbFixture _dbFixture;
    private readonly CategoriesRepository _categoriesRepository;

    public CategoriesRepositoryTests(DbFixture dbFixture)
    {
        _dbFixture = dbFixture;
        _categoriesRepository = new CategoriesRepository(mongoDatabase: _dbFixture.database);
    }
    
    [Fact]
    public async Task Get_categories_for_user_success()
    {
        //Arrange
        var fakeUserId  = new Guid(ConfigurationValues.FakeUserId);

        //Act
        await _dbFixture.Seed();
        var fakeCategories = await _categoriesRepository.GetAllAsync(c => c.UserId.Equals(fakeUserId));

        //Assert
        Assert.NotEmpty(fakeCategories);
    }

    [Fact]
    public async Task Get_category_by_id_for_user_success()
    {
        //Arrange
        var fakeUserId  = new Guid(ConfigurationValues.FakeUserId);
        var fakeExpenseCategoryId  = new Guid(ConfigurationValues.FakeExpenseCategory);

        //Act
        await _dbFixture.Seed();
        var fakeCategory = await _categoriesRepository.GetOneAsync(x => x.Id.Equals(fakeExpenseCategoryId) && x.UserId.Equals(fakeUserId));

        //Assert
        Assert.NotNull(fakeCategory);
        Assert.Equal(fakeCategory.Id, fakeExpenseCategoryId);
    }
}
