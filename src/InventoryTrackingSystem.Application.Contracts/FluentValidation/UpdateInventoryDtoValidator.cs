using FluentValidation;
using InventoryTrackingSystem.Dtos.Inventory;

namespace InventoryTrackingSystem.FluentValidation;

public class UpdateInventoryDtoValidator : AbstractValidator<UpdateInventoryDto>
{
    public UpdateInventoryDtoValidator()
    {
        // SerialNumberId zorunlu
        RuleFor(x => x.SerialNumberId)
            .NotEmpty()
            .WithMessage(InventoryTrackingSystemErrorCodes.Inventory.InvalidSerialNumber);
        // CurrentSiteId zorunlu
        RuleFor(x => x.CurrentSiteId)
            .NotEmpty()
            .WithMessage(InventoryTrackingSystemErrorCodes.Inventory.InvalidSite);
        // Müsait değilse sebep zorunlu
        When(x => !x.IsAvailableForRequest, () =>
        {
            RuleFor(x => x.UnavailabilityReasonId)
                .NotEmpty()
                .WithMessage(InventoryTrackingSystemErrorCodes.Inventory.ReasonRequiredWhenUnavailable);
        });
        // Müsaitse sebep null olmalı
        When(x => x.IsAvailableForRequest, () =>
        {
            RuleFor(x => x.UnavailabilityReasonId)
                .Null()
                .WithMessage(InventoryTrackingSystemErrorCodes.General.InvalidOperation);
        });
    }
}