using FluentValidation;
using InventoryTrackingSystem.Dtos.Inventory;
namespace InventoryTrackingSystem.FluentValidation;
public class ChangeAvailabilityStatusDtoValidator : AbstractValidator<ChangeAvailabilityStatusDto>
{
    public ChangeAvailabilityStatusDtoValidator()
    {
        // InventoryId zorunlu
        RuleFor(x => x.InventoryId)
            .NotEmpty()
            .WithMessage(InventoryTrackingSystemErrorCodes.Inventory.NotFound);
        // Müsait değilse sebep zorunlu
        When(x => !x.IsAvailable, () =>
        {
            RuleFor(x => x.UnavailabilityReasonId)
                .NotEmpty()
                .WithMessage(InventoryTrackingSystemErrorCodes.Inventory.ReasonRequiredWhenUnavailable);
        });
        // Müsaitse sebep null olmalı
        When(x => x.IsAvailable, () =>
        {
            RuleFor(x => x.UnavailabilityReasonId)
                .Null()
                .WithMessage(InventoryTrackingSystemErrorCodes.General.InvalidOperation);
        });
    }
}