namespace Ocean2City.Common.CommonData
{
    public static class Constants
    {
        /// <summary>
        /// Added By Column
        /// </summary>
        public const string CreatedBy = "CreatedBy";

        /// <summary>
        /// Added Date Column
        /// </summary>
        public const string CreatedDate = "CreatedDate";

        /// <summary>
        /// The modified by column
        /// </summary>
        public static string ModifiedBy = "ModifiedBy";

        /// <summary>
        /// The modified date column
        /// </summary>
        public static string ModifiedDate = "ModifiedDate";

        /// <summary>
        /// Is Deleted Column
        /// </summary>
        public const string IsDeletedColumn = "IsDeleted";

        /// <summary>
        /// Is Active Column
        /// </summary>
        public const string IsActiveColumn = "IsActive";

    }

    public static class RoleNotification
    {
        public const string Created = "Role Created";
        public const string Updated = "Role Updated";
        public const string Deleted = "Role Deleted";
        public const string Duplicate = "Role with the same name Already Exists!.";
    }

    public static class FileNotification
    {
        public const string FileNotFound = "No Files Found";
    }

    public static class CategoryNotification
    {
        public const string Added = "Category Added Successfully";
        public const string Updated = "Category Updated Successfully";
        public const string CategoryNotProvided = "Category Not Provided";
        public const string NoCategory = "No Category found";
    }

    public static class ItemNotification
    {
        public const string Added = "Item Added Successfully";
        public const string Updated = "Item Updated Successfully";
        public const string ItemNotProvided = "Item Not Provided";
        public const string NoItem = "No Item found";
    }

    public static class CommonErrorMessages
    {
        public const string UnknownError = "Sorry, we have encountered an error.";
        public const string BadRequest = "Invalid Request";
        public const string NoIdentifierProvided = "Provided Identifier is Null";
        public const string NoResultFound = "No Result Found";
    }
}
