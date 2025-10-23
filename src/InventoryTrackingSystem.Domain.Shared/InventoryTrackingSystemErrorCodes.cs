namespace InventoryTrackingSystem;

public static class InventoryTrackingSystemErrorCodes
{
    private const string Prefix = "InventorySystem:";
    public static class General
    {
        public const string ValidationFailed = Prefix + "GENERAL_VALIDATION_FAILED";
        public const string UnknownError = Prefix + "GENERAL_UNKNOWN_ERROR";
        public const string InvalidOperation = Prefix + "GENERAL_INVALID_OPERATION";
        public const string RecordAlreadyExists = Prefix + "GENERAL_RECORD_ALREADY_EXISTS";
        public const string RecordInUse = Prefix + "GENERAL_RECORD_IN_USE";
        public const string AccessDenied = Prefix + "GENERAL_ACCESS_DENIED";
        public const string InvalidInput = Prefix + "GENERAL_INVALID_INPUT";
    }
    public static class Inventory
    {
        public const string NotFound = Prefix + "INVENTORY_NOT_FOUND";
        public const string AlreadyInUse = Prefix + "INVENTORY_ALREADY_IN_USE";
        public const string NotAvailable = Prefix + "INVENTORY_NOT_AVAILABLE";
        public const string CannotDelete = Prefix + "INVENTORY_CANNOT_DELETE";
        public const string InvalidSerialNumber = Prefix + "INVENTORY_INVALID_SERIAL_NUMBER";
        public const string InvalidSite = Prefix + "INVENTORY_INVALID_SITE";
        public const string ReasonRequiredWhenUnavailable = Prefix + "INVENTORY_REASON_REQUIRED";
        public const string CannotUpdateInMovement = Prefix + "INVENTORY_CANNOT_UPDATE_IN_MOVEMENT";
        public const string InsufficientStock = Prefix + "INVENTORY_INSUFFICIENT_STOCK";
    }
    public static class MovementRequest
    {
        public const string NotFound = Prefix + "MOVEMENT_REQUEST_NOT_FOUND";
        public const string InvalidStatus = Prefix + "MOVEMENT_REQUEST_INVALID_STATUS";
        public const string AlreadyProcessed = Prefix + "MOVEMENT_REQUEST_ALREADY_PROCESSED";
        public const string CannotDelete = Prefix + "MOVEMENT_REQUEST_CANNOT_DELETE";
        public const string CannotUpdate = Prefix + "MOVEMENT_REQUEST_CANNOT_UPDATE";
        public const string InvalidFromSite = Prefix + "MOVEMENT_REQUEST_INVALID_FROM_SITE";
        public const string InvalidToSite = Prefix + "MOVEMENT_REQUEST_INVALID_TO_SITE";
        public const string SameSiteTransfer = Prefix + "MOVEMENT_REQUEST_SAME_SITE";
        public const string NoDetails = Prefix + "MOVEMENT_REQUEST_NO_DETAILS";
        public const string AlreadyApproved = Prefix + "MOVEMENT_REQUEST_ALREADY_APPROVED";
        public const string AlreadyRejected = Prefix + "MOVEMENT_REQUEST_ALREADY_REJECTED";
    }
    public static class MovementRequestDetail
    {
        public const string NotFound = Prefix + "MOVEMENT_REQUEST_DETAIL_NOT_FOUND";
        public const string InvalidQuantity = Prefix + "MOVEMENT_REQUEST_DETAIL_INVALID_QUANTITY";
        public const string QuantityExceedsAvailable = Prefix + "MOVEMENT_REQUEST_DETAIL_QUANTITY_EXCEEDS";
        public const string DuplicateSerialNumber = Prefix + "MOVEMENT_REQUEST_DETAIL_DUPLICATE_SERIAL";
        public const string CannotDelete = Prefix + "MOVEMENT_REQUEST_DETAIL_CANNOT_DELETE";
    }
    public static class MovementRequestResponse
    {
        public const string NotFound = Prefix + "MOVEMENT_REQUEST_RESPONSE_NOT_FOUND";
        public const string AlreadyResponded = Prefix + "MOVEMENT_REQUEST_RESPONSE_ALREADY_EXISTS";
        public const string InvalidApprover = Prefix + "MOVEMENT_REQUEST_RESPONSE_INVALID_APPROVER";
        public const string CommentRequired = Prefix + "MOVEMENT_REQUEST_RESPONSE_COMMENT_REQUIRED";
        public const string CannotModify = Prefix + "MOVEMENT_REQUEST_RESPONSE_CANNOT_MODIFY";
    }
    public static class InventoryMovement
    {
        public const string NotFound = Prefix + "INVENTORY_MOVEMENT_NOT_FOUND";
        public const string AlreadyCompleted = Prefix + "INVENTORY_MOVEMENT_ALREADY_COMPLETED";
        public const string InvalidStatus = Prefix + "INVENTORY_MOVEMENT_INVALID_STATUS";
        public const string VehicleRequired = Prefix + "INVENTORY_MOVEMENT_VEHICLE_REQUIRED";
        public const string CannotDelete = Prefix + "INVENTORY_MOVEMENT_CANNOT_DELETE";
        public const string CannotUpdateCompleted = Prefix + "INVENTORY_MOVEMENT_CANNOT_UPDATE_COMPLETED";
        public const string InvalidVehicle = Prefix + "INVENTORY_MOVEMENT_INVALID_VEHICLE";
    }
    public static class Stock
    {
        public const string NotFound = Prefix + "STOCK_NOT_FOUND";
        public const string InsufficientQuantity = Prefix + "STOCK_INSUFFICIENT_QUANTITY";
        public const string NegativeStock = Prefix + "STOCK_NEGATIVE_NOT_ALLOWED";
        public const string CannotDelete = Prefix + "STOCK_CANNOT_DELETE";
        public const string AlreadyExists = Prefix + "STOCK_ALREADY_EXISTS";
        public const string InvalidCalculation = Prefix + "STOCK_INVALID_CALCULATION";
    }
    public static class InventoryHistory
    {
        public const string NotFound = Prefix + "INVENTORY_HISTORY_NOT_FOUND";
        public const string InvalidEventType = Prefix + "INVENTORY_HISTORY_INVALID_EVENT_TYPE";
        public const string CannotModify = Prefix + "INVENTORY_HISTORY_CANNOT_MODIFY";
        public const string CannotDelete = Prefix + "INVENTORY_HISTORY_CANNOT_DELETE";
    }
    public static class Vehicle
    {
        public const string NotFound = Prefix + "VEHICLE_NOT_FOUND";
        public const string InvalidPlateNumber = Prefix + "VEHICLE_INVALID_PLATE_NUMBER";
        public const string PlateNumberExists = Prefix + "VEHICLE_PLATE_NUMBER_EXISTS";
        public const string InvalidType = Prefix + "VEHICLE_INVALID_TYPE";
        public const string InUse = Prefix + "VEHICLE_IN_USE";
        public const string CannotDelete = Prefix + "VEHICLE_CANNOT_DELETE";
    }
    public static class SerialNumber
    {
        public const string NotFound = Prefix + "SERIAL_NUMBER_NOT_FOUND";
        public const string InvalidPrefix = Prefix + "SERIAL_NUMBER_INVALID_PREFIX";
        public const string PrefixAlreadyExists = Prefix + "SERIAL_NUMBER_PREFIX_EXISTS";
        public const string InUse = Prefix + "SERIAL_NUMBER_IN_USE";
        public const string CannotDelete = Prefix + "SERIAL_NUMBER_CANNOT_DELETE";
    }
    public static class Site
    {
        public const string NotFound = Prefix + "SITE_NOT_FOUND";
        public const string InvalidType = Prefix + "SITE_INVALID_TYPE";
        public const string InUse = Prefix + "SITE_IN_USE";
        public const string CannotDelete = Prefix + "SITE_CANNOT_DELETE";
    }
    public static class UnavailabilityReason
    {
        public const string NotFound = Prefix + "UNAVAILABILITY_REASON_NOT_FOUND";
        public const string InvalidReason = Prefix + "UNAVAILABILITY_REASON_INVALID";
        public const string AlreadyExists = Prefix + "UNAVAILABILITY_REASON_ALREADY_EXISTS";
        public const string InUse = Prefix + "UNAVAILABILITY_REASON_IN_USE";
        public const string CannotDelete = Prefix + "UNAVAILABILITY_REASON_CANNOT_DELETE";
    }
}
