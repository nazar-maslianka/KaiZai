using KaiZai.Service.Categories.Contracts;
using KaiZai.Service.Expenses.BAL.DTOs;
using KaiZai.Service.Expenses.DAL.Models;

namespace KaiZai.Service.Expenses.BAL.Core;

public static class Mappings
{
      public static Category ToCategory(this CategoryDTO categoryDTO)
    {
        if (categoryDTO == null)
        {
            throw new ArgumentNullException(nameof(categoryDTO));
        }

        return new Category
        {
            Id = categoryDTO.Id,
            Name = categoryDTO.Name,
            CategoryType = categoryDTO.CategoryType
        };
    }

    public static Category ToCategory(this CategoryCreated categoryCreated)
    {
        if (categoryCreated == null)
        {
            throw new ArgumentNullException(nameof(categoryCreated));
        }

        return new Category
        {
            Id = categoryCreated.Id,
            Name = categoryCreated.Name,
            ProfileId = categoryCreated.ProfileId,
            CategoryType = categoryCreated.CategoryType
        };
    }

    public static Category ToCategory(this CategoryUpdated categoryUpdated)
    {
        if (categoryUpdated == null)
        {
            throw new ArgumentNullException(nameof(categoryUpdated));
        }

        return new Category
        {
            Id = categoryUpdated.Id,
            ProfileId = categoryUpdated.ProfileId,
            Name = categoryUpdated.Name,
            CategoryType = categoryUpdated.CategoryType
        };
    }

    public static ExpenseDTO ToExpenseDTO(this Expense expense, Category category)
    {
        if (expense == null)
        {
            throw new ArgumentNullException(nameof(expense));
        }

        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        return new ExpenseDTO
        (
            expense.Id,
            expense.ProfileId,
            new CategoryDTO
            (
                category.Id,
                category.Name,
                category.CategoryType
            ),
            expense.ExpenseDate,
            expense.Description,
            expense.Amount
        );
    }

    public static ExpenseShortDTO ToExpenseShortDTO(this Expense expense, Category category)
    {
        if (expense == null)
        {
            throw new ArgumentNullException(nameof(expense));
        }

        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        return new ExpenseShortDTO
        (
            expense.Id,
            new CategoryShortDTO
            (
                category.Id,
                category.Name
            ),
            expense.ExpenseDate,
            expense.Amount
        );
    }

    public static Expense ToExpense(this AddUpdateExpenseDTO addUpdateExpenseDTO, Guid profileId, Guid? expenseId = null)
    {
        if (addUpdateExpenseDTO == null)
        {
            throw new ArgumentNullException(nameof(addUpdateExpenseDTO));
        }

        if (profileId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(profileId));
        }

        var expense = new Expense
        {
            ProfileId = profileId,
            Amount = addUpdateExpenseDTO.Amount,
            CategoryId = addUpdateExpenseDTO.CategoryId,
            ExpenseDate = addUpdateExpenseDTO.ExpenseDate,
            Description = addUpdateExpenseDTO.Description
        };

        if (expenseId.HasValue && expenseId.Value != Guid.Empty)
        {
            expense.Id = expenseId.Value;
        }

        return expense;
    }
}