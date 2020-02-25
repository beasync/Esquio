﻿using Esquio.UI.Api.Infrastructure.Data.Entities;
using FluentValidation;

namespace Esquio.UI.Api.Scenarios.ApiKeys.Add
{
    public class AddApiKeyRequestValidator
        : AbstractValidator<AddApiKeyRequest>
    {
        public AddApiKeyRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(200)
                .Matches(ApiConstants.Constraints.NamesRegularExpression);

            RuleFor(x => x.ActAs)
                .IsEnumName(typeof(ApplicationRole), caseSensitive: false);
        }
    }
}
