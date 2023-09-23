using FluentValidation;
using Tivix.FamilyBudget.Server.Core.Users.Providers;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

namespace Tivix.FamilyBudget.Server.Core.Users.Validators;

/// <summary>
/// Validates that Budget with provided ID belongs to the specific user
/// </summary>
internal class UserBudgetValidator : AbstractValidator<Guid>
{
    public UserBudgetValidator(IUserProvider userProvider, ApplicationContext context)
    {
        RuleFor(p => p).Must(ValidateUser).WithMessage("No access to this resource"); ;

        bool ValidateUser(Guid budgetId)
        {
            var user = userProvider.UserEntity;
            if (user!.BudgetsAccessible == null || !user.BudgetsAccessible.Contains(budgetId))
            {
                return context.Budgets.First(p => p.Id == budgetId).User.Id == userProvider.UserEntity!.Id;
            }

            return true;
        }
    }
}

/// <summary>
/// Validates that Category with provided ID belongs to the specific user
/// </summary>
internal class UserCategoryValidator : AbstractValidator<Guid>
{
    public UserCategoryValidator(IUserProvider userProvider, ApplicationContext context)
    {
        RuleFor(p => p).Must(ValidateUser).WithMessage("No access to this resource"); ;

        bool ValidateUser(Guid categoryId)
        {
            var category = context.Categories.First(p=>p.Id == categoryId);
            return category.Id == userProvider.UserEntity!.Id;
        }
    }
}

/// <summary>
/// Validates that FinancialEntry with provided ID belongs to the specific user
/// </summary>
internal class UserFinancialEntryValidator : AbstractValidator<Guid>
{
    public UserFinancialEntryValidator(IUserProvider userProvider, ApplicationContext context)
    {
        RuleFor(p => p).Must(ValidateUser).WithMessage("No access to this resource"); ;

        bool ValidateUser(Guid financialEntryId)
        {
            var financialEntry = context.FinancialEntries.First(p=>p.Id == financialEntryId);
            return context.Budgets.First(p => p.Id == financialEntry.Category.Id).User.Id == userProvider.UserEntity!.Id;
        }
    }
}

/// <summary>
/// Validates that User Exists
/// </summary>
internal class UserExistsValidator : AbstractValidator<Guid>
{
    public UserExistsValidator(ApplicationContext context)
    {
        RuleFor(p => p).Must(ValidateUser).WithMessage("User does not exist"); ;

        bool ValidateUser(Guid userId)
        {
            return context.Users.First(p => p.Id == userId).Id == userId;
        }
    }
}
